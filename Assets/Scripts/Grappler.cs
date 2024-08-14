using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappler : MonoBehaviour
{
    public Transform cam;
    public Transform grapplerTip;
    public LayerMask grappleable;
    public LineRenderer lineRenderer;

    public float maxGrappleDistance;
    public float grappleDelay;

    private Vector3 grapplePoint;

    public float grapplingCoolDown;
    private float grapplingCoolDownTimer;

    public KeyCode grappleKey = KeyCode.Mouse1;

    private bool grappling;

    private void Start()
    {
        
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

    }

    private void StopGrapple()
    {
        grappling = false;

        grapplingCoolDownTimer = grapplingCoolDown;

        lineRenderer.enabled = false;
    }


}
