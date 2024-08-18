using System.Collections;
using UnityEngine;

public class Grappler : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    public Transform cam;
    public Transform grapplerTip;
    public LayerMask grappleable;
    public LineRenderer lineRenderer;

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

    private bool grappling;

    // Añadir una referencia al Rigidbody del jugador
    private Rigidbody playerRigidbody;

    private void Start()
    {
        // Asignar el Rigidbody del jugador
        playerRigidbody = playerTransform.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(grappleKey))
        {
            StartGrapple();
        }

        if (grapplingCoolDownTimer > 0)
        {
            grapplingCoolDownTimer -= Time.deltaTime;
        }
    }

    private void LateUpdate()
    {
        if (grappling)
        {
            lineRenderer.SetPosition(0, grapplerTip.position);
        }
    }

    private void StartGrapple()
    {
        if (grapplingCoolDownTimer > 0) return;

        grappling = true;

        RaycastHit hitPoint;
        if (Physics.Raycast(cam.position, cam.forward, out hitPoint, maxGrappleDistance, grappleable))
        {
            Vector3 surfaceNormal = hitPoint.normal;
            grapplePoint = hitPoint.point - surfaceNormal * offSet;

            // Desactivar el control de físicas del jugador durante el grappling
            playerRigidbody.isKinematic = true;

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
        grappling = false;
        grapplingCoolDownTimer = grapplingCoolDown;
        lineRenderer.enabled = false;

        // Reactivar el control de físicas del jugador
        playerRigidbody.isKinematic = false;
    }

    private IEnumerator grappleCoroutine()
    {
        float timer = 0f;
        float startTime = Time.time;
        Vector3 startPosition = playerTransform.position;

        while (timer < lerpDuration)
        {
            timer = Time.time - startTime;
            playerTransform.position = Vector3.Lerp(startPosition, grapplePoint, timer / lerpDuration);

            yield return null;
        }

        // Asegurar que la posición final sea exacta
        playerTransform.position = grapplePoint;

        // Reactivar el control del jugador y detener la velocidad del Rigidbody
        playerRigidbody.isKinematic = false;
        playerRigidbody.velocity = Vector3.zero;

        // Detener el grappling visualmente
        StopGrapple();
    }
}
