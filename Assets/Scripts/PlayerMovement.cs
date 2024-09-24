using UnityEngine;

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

    private Vector3 startPos;

    private KeyCode jumpKey = KeyCode.Space;
    private KeyCode reset = KeyCode.O;

    public float playerHeight;
    public LayerMask Ground;
    public LayerMask Grappleable;
    bool grounded;
    bool inGrappleable;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
        rb.useGravity = false;
        startPos = transform.position;
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, Ground);
        inGrappleable = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, Grappleable);

        MyInput();

        if (Input.GetKey(reset)) 
        { 
            transform.position = startPos;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
        rb.AddForce(customGravity * Time.fixedDeltaTime, ForceMode.Acceleration);

        if (grounded || inGrappleable)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(jumpKey) && readyToJump && grounded || Input.GetKey(jumpKey) && readyToJump && inGrappleable) 
        { 
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if(grounded || inGrappleable)
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        else 
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
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
