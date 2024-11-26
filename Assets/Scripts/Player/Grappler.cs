using System.Collections;
using UnityEngine;

public class Grappler : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] Rigidbody playerRB;
    [SerializeField] PlayerMovement playerMovement;
    public Transform cam;
    public Transform grapplerTip;
    public LayerMask grappleable;
    public LineRenderer lineRenderer;

    public float offSet;
    public float maxGrappleDistance;
    public float grappleDelay;
    public float grapplingSpeed;
    public float lerpDuration;
    public float grappleLaunchForce;
    private int lineRendererIndex = 1;

    private Vector3 grapplePoint;
    private float startTime;

    public float grapplingCoolDown;
    private float grapplingCoolDownTimer;

    private bool isGrappling;

    PlayerAnimationHandler playerAnimationHandler;
    [SerializeField] InputReader inputReader;

    private void Start()
    {
        playerAnimationHandler = GetComponent<PlayerAnimationHandler>();
        lineRenderer.enabled = false;
    }

    private void OnEnable()
    {
        inputReader.OnGrappling += AttemptGrappling;
    }

    private void OnDisable()
    {
        inputReader.OnGrappling -= AttemptGrappling;
    }

    private void Update()
    {
        if (grapplingCoolDownTimer > 0)
        {
            grapplingCoolDownTimer -= Time.deltaTime;
        }

        playerAnimationHandler.SetGrapplingBoolAnimation(isGrappling);
    }

    private void AttemptGrappling()
    {
        if (!isGrappling)
        {           
            StartGrapple();
        }
    }

    private void LateUpdate()
    {
        if (isGrappling)
        {
            lineRenderer.SetPosition(0, grapplerTip.position);
        }
    }

    private void StartGrapple()
    {
        if (grapplingCoolDownTimer > 0) return;
        
        isGrappling = true;

        RaycastHit hitPoint;
        if (Physics.Raycast(cam.position, cam.forward, out hitPoint, maxGrappleDistance, grappleable))
        {
            GrapplerMagnetism grappleMagnetism = hitPoint.collider.GetComponent<GrapplerMagnetism>();

            if (grappleMagnetism != null)
            {
                grapplePoint = grappleMagnetism.GetMagnetismPoint(hitPoint.point);
            }
            else
            {
                Vector3 surfaceNormal = hitPoint.normal;
                grapplePoint = hitPoint.point - surfaceNormal * offSet;
            }

            Invoke(nameof(ExecuteGrapple), grappleDelay);
        }
        else
        {
            grapplePoint = cam.position + cam.forward * maxGrappleDistance;

            Invoke(nameof(StopGrapple), grappleDelay);
        }

        if (lineRenderer != null)
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(lineRendererIndex, grapplePoint);
        }       
    }

    private void ExecuteGrapple()
    {
        StartCoroutine(grappleCoroutine());
    }

    private void StopGrapple()
    {
        isGrappling = false;
        grapplingCoolDownTimer = grapplingCoolDown;
        lineRenderer.enabled = false;
    }

    private IEnumerator grappleCoroutine()
    {
        float timer = 0f;
        Vector3 startPosition = playerTransform.position;

        isGrappling = true;
        playerRB.isKinematic = true;
        playerMovement.enabled = false;

        while (timer < lerpDuration)
        {
            timer += Time.deltaTime;
            playerTransform.position = Vector3.Lerp(startPosition, grapplePoint, timer / lerpDuration);

            yield return null;
        }

        playerMovement.enabled = true;
        playerRB.isKinematic = false;

        Vector3 launchDirection = (grapplePoint - startPosition).normalized;
        playerRB.AddForce(launchDirection * grappleLaunchForce, ForceMode.VelocityChange);

        isGrappling = false;
        lineRenderer.enabled = false;
    }
}
