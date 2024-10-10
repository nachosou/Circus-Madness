using UnityEngine;

public class WallRunning : MonoBehaviour
{
    public LayerMask RunnableWall;
    public LayerMask Ground;

    public float wallrunForce;
    public float maxWallrunTime;
    private bool wallRunning;

    public float walljumpUpForce;
    public float walljumpSideForce;
    public float walljumpForwardForce; 

    private float verticalInput;
    public KeyCode jumpKey = KeyCode.Space;

    public float wallCheckDistance;
    public float minJumpHeight;
    private RaycastHit leftWallhit;
    private RaycastHit rightWallhit;
    private bool isRunningInLeftWall;
    private bool isRunningInRightWall;


    [SerializeField] Transform orientation;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        wallRunning = false;
    }

    private void Update()
    {
        CheckWall();
        StateMachine();
    }

    private void FixedUpdate()
    {
        if (wallRunning)
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
        verticalInput = Input.GetAxisRaw("Vertical");

        if ((isRunningInLeftWall || isRunningInRightWall) && verticalInput > 0 && HighEnough())
        {
            if (!wallRunning) 
            { 
                StartWallrun();               
            }

            if (Input.GetKeyDown(jumpKey))
            {
                WallJump();
            }
        }
        else
        {
            if (wallRunning) 
            {
                StopWallrun();
            }
        }
    }

    private void StartWallrun()
    {
        wallRunning = true;
        rb.useGravity = false;
    }

    private void StopWallrun()
    {
        wallRunning = false;
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

        rb.AddForce((wallForward * wallrunForce) * Time.fixedDeltaTime, ForceMode.Force);
    }

    private void WallJump()
    {
        Vector3 wallNormal = isRunningInRightWall ? rightWallhit.normal : leftWallhit.normal;

        Vector3 forwardForce = orientation.forward * walljumpForwardForce;

        Vector3 forceToApply = transform.up * walljumpUpForce + forwardForce;

        rb.velocity = Vector3.zero;

        rb.AddForce(forceToApply, ForceMode.Impulse);
    }
}
