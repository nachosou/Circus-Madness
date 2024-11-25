using UnityEngine;

public class WallRunning : MonoBehaviour
{
    public LayerMask RunnableWall;
    public LayerMask Ground;

    public float wallrunForce;
    public float maxWallrunTime;
    public bool isPlayerWallRunning;

    public float walljumpUpForce;
    public float walljumpForwardForce;
    public float walljumpSideForce;

    public float wallCheckDistance;
    public float minJumpHeight;
    private RaycastHit leftWallhit;
    private RaycastHit rightWallhit;
    public bool isRunningInLeftWall;
    public bool isRunningInRightWall;

    [SerializeField] private Transform orientation;
    private float inputDir;
    [SerializeField] private InputReader inputReader;

    private Rigidbody rb;

    private const int wallRunMultiplyer = 10;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        isPlayerWallRunning = false;
    }

    private void OnEnable()
    {
        inputReader.OnMove += AttemptWallRide;
        inputReader.OnJump += AttemptWallJump;
    }

    private void OnDisable()
    {
        inputReader.OnMove -= AttemptWallRide;
        inputReader.OnJump -= AttemptWallJump;
    }

    private void Update()
    {
        CheckWall();
        StateMachine();
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
        isRunningInRightWall = Physics.Raycast(transform.position, orientation.right, out rightWallhit, wallCheckDistance, RunnableWall);
        isRunningInLeftWall = Physics.Raycast(transform.position, -orientation.right, out leftWallhit, wallCheckDistance, RunnableWall);
    }

    private bool HighEnough()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight, Ground);
    }

    private void StateMachine()
    {
        if ((isRunningInLeftWall || isRunningInRightWall) && inputDir > 0 && HighEnough())
        {
            if (!isPlayerWallRunning) 
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
    }

    private void StopWallrun()
    {
        isPlayerWallRunning = false;
        rb.useGravity = true;
    }

    private void WallrunMovement()
    {
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
        Vector3 wallNormal = isRunningInRightWall ? rightWallhit.normal : leftWallhit.normal;
        Vector3 wallForward = Vector3.Cross(wallNormal, transform.up).normalized;

        if ((orientation.forward - wallForward).magnitude > (orientation.forward - -wallForward).magnitude)
        {
            wallForward = -wallForward;
        }

        Vector3 forwardForce = wallForward * walljumpForwardForce;
        Vector3 sideForce = (isRunningInRightWall ? -orientation.right : orientation.right) * walljumpSideForce;
        sideForce.y = 0f;

        Vector3 forceToApply = (transform.up * walljumpUpForce + forwardForce + sideForce).normalized * walljumpForwardForce;

        Debug.DrawRay(transform.position, forwardForce, Color.red, 10f);
        Debug.DrawRay(transform.position, sideForce, Color.green, 10f);
        Debug.DrawRay(transform.position, forceToApply, Color.blue, 10f);
        rb.velocity = Vector3.zero;

        rb.AddForce(forceToApply, ForceMode.VelocityChange);
    }
}
