using System;
using System.Collections;
using UnityEngine;

public class Grappler : MonoBehaviour
{
    [SerializeField] Transform playerTransform;

    public Transform cam;
    public Transform grapplerTip;
    public LayerMask grappleable;
    public LineRenderer lineRenderer;

    public float maxGrappleDistance;
    public float grappleDelay;
    public float grapplingSpeed;
    [SerializeField] private AnimationCurve lerpCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private Vector3 grapplePoint;
    float lerpLength;
    private float startTime;

    public float grapplingCoolDown;
    private float grapplingCoolDownTimer;

    public KeyCode grappleKey = KeyCode.Mouse1;

    private bool grappling;

    private void Start()
    {
        startTime = Time.time;
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
            grapplePoint = hitPoint.point;

            lerpLength = Vector3.Distance(grapplerTip.position, grapplePoint);

            Invoke(nameof(ExecuteGrapple), grappleDelay);
        }
        else
        {
            grapplePoint = cam.position + cam.forward * maxGrappleDistance;

            Invoke(nameof(StopGrapple), grappleDelay);
        }

        Invoke(nameof(StopGrapple), grappleDelay);

        lineRenderer.enabled = true;
        lineRenderer.SetPosition(1, grapplePoint);
    }

    private void ExecuteGrapple()
    {
        float distCovered = (Time.time - startTime) * grapplingSpeed;

        float fractionOfJourney = distCovered / lerpLength;

        playerTransform.position = Vector3.Lerp(grapplerTip.position, grapplePoint, fractionOfJourney);
        StartCoroutine(Lerp(5, HandleLerpPlayer));
        StartCoroutine(EasedLerp(5, EaseInOut, HandleLerpPlayer));
        StartCoroutine(CurvedLerp(5, lerpCurve, HandleLerpPlayer));

        void HandleLerpPlayer(float lerp)
        {
            playerTransform.position = Vector3.Lerp(grapplerTip.position, grapplePoint, fractionOfJourney);
        }

        float EaseInOut(float t)
        {
            return -(Mathf.Cos(Mathf.PI * t) - 1) / 2;
        }
    }


    private void StopGrapple()
    {
        grappling = false;

        grapplingCoolDownTimer = grapplingCoolDown;

        lineRenderer.enabled = false;
    }

    private static IEnumerator Lerp(float duration, Action<float> callback)
    {
        float t = 0;
        do
        {
            var lerp = t / duration;
            //run logic with lerp
            callback(lerp);
            yield return null;
            t += Time.deltaTime;
        } while (t < duration);
    }
    private static IEnumerator EasedLerp(float duration,Ease ease, Action<float> callback)
    {
        float t = 0;
        do
        {
            var lerp = t / duration;
            //run logic with lerp
            var easedLerp = ease(lerp);
            callback(lerp);
            yield return null;
            t += Time.deltaTime;
        } while (t < duration);
    }
    private static IEnumerator CurvedLerp(float duration, AnimationCurve curve, Action<float> callback)
    {
        float t = 0;
        do
        {
            var lerp = t / duration;
            //run logic with lerp
            var curvedLerp = curve.Evaluate(lerp);
            callback(lerp);
            yield return null;
            t += Time.deltaTime;
        } while (t < duration);
    }
}
public delegate float Ease(float t);