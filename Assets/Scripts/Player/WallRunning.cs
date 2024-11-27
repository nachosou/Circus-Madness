using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class WallRunning : MonoBehaviour
{
    public LayerMask RunnableWall;
    public LayerMask Ground;

    public float wallrunForce;
    public float maxWallrunTime;
    public bool isPlayerWallRunning;

    public float walljumpUpForce;
    public float walljumpSideForce;

    public float wallCheckDistance;
    public float minJumpHeight;
    private RaycastHit leftWallhit;
    private RaycastHit rightWallhit;
    public bool isRunningInLeftWall;
    public bool isRunningInRightWall;
    private bool hasPlayerWallJump;

    [SerializeField] private Transform orientation;
    private float inputDir;
    [SerializeField] private InputReader inputReader;

    private Rigidbody rb;
    public float maximumUpForce;
    public float maximumSideForce;
    public float maximumForwardForce;
    private const int wallRunMultiplyer = 10;

    public float coolDownTime;
    private bool isOnCoolDown;
    private Coroutine coolDownCoroutine;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        isPlayerWallRunning = false;
    }

    private void OnEnable()
    {
        inputReader.OnMove += AttemptWallRide;
        inputReader.OnJump += AttemptWallJump;
        isOnCoolDown = false;
    }

    private void OnDisable()
    {
        inputReader.OnMove -= AttemptWallRide;
        inputReader.OnJump -= AttemptWallJump;
    }

    private void Update()
    {
        CheckWall();
        WallRunStates();
    }

    private void FixedUpdate()
    {
        if (isPlayerWallRunning)
        {
            WallrunMovement();
        }
    }

    private void CheckWall()
    {
        if (!isOnCoolDown)
        {
            isRunningInRightWall = Physics.Raycast(transform.position, orientation.right, out rightWallhit, wallCheckDistance, RunnableWall);
            isRunningInLeftWall = Physics.Raycast(transform.position, -orientation.right, out leftWallhit, wallCheckDistance, RunnableWall);
        }
    }

    private bool HighEnough()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight, Ground);
    }

    private void WallRunStates()
    {
        if ((isRunningInLeftWall || isRunningInRightWall) && inputDir > 0 && HighEnough())
        {
            if (!isPlayerWallRunning && !hasPlayerWallJump) 
            { 
                StartWallrun();
            }
        }
        else
        {
            if (isPlayerWallRunning) 
            {
                StopWallrun();
            }
        }
    }

    private void AttemptWallRide(Vector2 value)
    {
        inputDir = value.y;
    }

    private void AttemptWallJump()
    {
        if ((isRunningInLeftWall || isRunningInRightWall) && inputDir > 0 && HighEnough())
        {
            WallJump();
        }
    }

    private void StartWallrun()
    {
        isPlayerWallRunning = true;
        rb.useGravity = false;
        hasPlayerWallJump = false;
    }

    private void StopWallrun()
    {
        isPlayerWallRunning = false;
        rb.useGravity = true;
        isOnCoolDown = true;
        hasPlayerWallJump = false;

        if (coolDownCoroutine != null)
        {
            StopCoroutine(coolDownCoroutine);
        }

        coolDownCoroutine = StartCoroutine(WaitCoolDown());
    }

    private void WallrunMovement()
    {
        if (hasPlayerWallJump)
            return;

        rb.velocity = new Vector3 (rb.velocity.x, 0f, rb.velocity.z);

        Vector3 wallNormal = isRunningInRightWall ? rightWallhit.normal : leftWallhit.normal;

        Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);

        if ((orientation.forward - wallForward).magnitude > (orientation.forward - -wallForward).magnitude)
        {
            wallForward = -wallForward;
        }

        rb.AddForce((wallForward * wallrunForce * wallRunMultiplyer) * Time.fixedDeltaTime, ForceMode.Force);
    }

    private void WallJump()
    {
        hasPlayerWallJump = true;

        Vector3 sideForce = (isRunningInRightWall ? -orientation.right : orientation.right) * walljumpSideForce;
        sideForce.y = 0f;

        Vector3 forceToApply = (transform.up * walljumpUpForce) + sideForce;

        forceToApply.x = Mathf.Clamp(forceToApply.x, walljumpSideForce, maximumSideForce);
        forceToApply.y = Mathf.Clamp(forceToApply.y, walljumpUpForce, maximumUpForce);

        rb.AddForce(forceToApply, ForceMode.Impulse);
    }

    IEnumerator WaitCoolDown() 
    { 
        yield return new WaitForSeconds(coolDownTime);
        isOnCoolDown = false;
    }
}
