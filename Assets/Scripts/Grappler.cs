using System.Collections;
using UnityEngine;

public class Grappler : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    public Transform cam;
    public Transform grapplerTip;
    public LayerMask grappleable;
    public LineRenderer lineRenderer;
    public GameObject dot;

    public float offSet;
    public float maxGrappleDistance;
    public float grappleDelay;
    public float grapplingSpeed;
    public float lerpDuration;

    private Vector3 grapplePoint;
    private float startTime;

    public float grapplingCoolDown;
    private float grapplingCoolDownTimer;

    public KeyCode grappleKey = KeyCode.Mouse1;

    private bool isGrappling;

    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyUp(grappleKey) && !isGrappling)
        {
            StartGrapple();
        }

        if (grapplingCoolDownTimer > 0)
        {
            grapplingCoolDownTimer -= Time.deltaTime;
        }

        dot.transform.position = cam.position + cam.forward * maxGrappleDistance;
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
            Transform grappleMagnetism = hitPoint.collider.GetComponent<GrapplerMagnetism>().GetMagnetismPoint();

            if (grappleMagnetism != null)
            {
                grapplePoint = grappleMagnetism.position;

                Debug.Log("AAAAA");
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

        lineRenderer.enabled = true;
        lineRenderer.SetPosition(1, grapplePoint);
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
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<PlayerMovement>().enabled = false;

        while (timer < lerpDuration)
        {
            timer += Time.deltaTime;
            playerTransform.position = Vector3.Lerp(startPosition, grapplePoint, timer / lerpDuration);

            yield return null;
        }
        GetComponent<PlayerMovement>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
        isGrappling = false;

        lineRenderer.enabled = false;
    }
}
