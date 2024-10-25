using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;

    [SerializeField] WallRunning wallRunningScript;

    float xRotation;
    float yRotation;

    public float tiltAngle;
    public float tiltSpeed;
    private float currentTilt;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, WallRunTilt());
        orientation.rotation = Quaternion.Euler(0, yRotation, WallRunTilt());
    }

    private float WallRunTilt()
    {
        if (wallRunningScript.wallRunning)
        {
            if (wallRunningScript.isRunningInLeftWall)
            {
                currentTilt = Mathf.MoveTowards(currentTilt, -tiltAngle, Time.deltaTime * tiltSpeed);
            }
            else if (wallRunningScript.isRunningInRightWall)
            {
                currentTilt = Mathf.MoveTowards(currentTilt, tiltAngle, Time.deltaTime * tiltSpeed);
            }
        }
        else
        {
            currentTilt = Mathf.MoveTowards(currentTilt, 0, Time.deltaTime * tiltSpeed);
        }

        float targetRotation = currentTilt;
        return targetRotation;
    }
}
