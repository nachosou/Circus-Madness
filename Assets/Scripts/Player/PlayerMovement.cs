using UnityEngine;
using AK.Wwise;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;

    public float groundDrag;
    public float airDrag;
    public Vector3 customGravity;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    private float coyoteTime = 0.2f; 
    private float coyoteTimeCounter;

    private Vector3 startPos;

    public float playerHeight;
    public LayerMask Ground;
    public LayerMask Grappleable;
    bool IsPlayerOnGround;
    bool inGrappleable;
    bool IsPlayerMoving;

    public Transform orientation;
    private Vector2 inputDir;
    Vector3 moveDirection;
    Rigidbody rb;

    [SerializeField] InputReader inputReader;
    public AK.Wwise.Event wwiseEvent;

    public float stepInterval = 0.1f;
    private float stepTimer;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
        rb.useGravity = false;
        startPos = transform.position;
    }

    private void OnEnable()
    {
        inputReader.OnJump += AttemptJump;
        inputReader.OnMove += AttemptMove;
        
    }

    private void OnDisable()
    {
        inputReader.OnJump -= AttemptJump;
        inputReader.OnMove -= AttemptMove;
    }

    private void Update()
    {
        IsPlayerOnGround = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, Ground);
        inGrappleable = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, Grappleable);

        if (IsPlayerOnGround || inGrappleable)
        {
            coyoteTimeCounter = coyoteTime; 
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        IsPlayerMoving = inputDir.magnitude > 0.1f;

        if (IsPlayerMoving && IsPlayerOnGround)
        {
            stepTimer += Time.deltaTime;

            if (stepTimer >= stepInterval)
            {
                wwiseEvent.Post(gameObject);
                stepTimer = 0f; 
            }
        }
        else
        {
            stepTimer = 0f;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
        rb.AddForce(customGravity * Time.fixedDeltaTime, ForceMode.Acceleration);

        if (IsPlayerOnGround || inGrappleable)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
    }

    private void AttemptMove(Vector2 value)
    {
        inputDir = value;
    }

    private void AttemptJump()
    {
        if (readyToJump && (IsPlayerOnGround || inGrappleable || coyoteTimeCounter > 0f))
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private Vector3 GetGroundNormal()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.1f, Ground))
        {
            return hit.normal;  
        }

        return Vector3.up;
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * inputDir.y + orientation.right * inputDir.x;

        Vector3 groundNormal = GetGroundNormal();
        Vector3 adjustedDirection = Vector3.ProjectOnPlane(moveDirection, groundNormal).normalized;

        float multiplier = (IsPlayerOnGround || inGrappleable) ? 10f : 10f * airMultiplier;
        rb.AddForce(adjustedDirection * moveSpeed * multiplier, ForceMode.Force);
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}
