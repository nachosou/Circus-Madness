using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    private float sensX;
    private float sensY;

    public float mouseSensX;
    public float mouseSensY;

    public float gamepadSensX;
    public float gamepadSensY;

    public Transform orientation;

    [SerializeField] WallRunning wallRunningScript;

    private float xRotation;
    private float yRotation;

    public float tiltAngle;
    public float tiltSpeed;
    private float currentTilt;
    public float rotationClamp = 90;

    private Vector2 mouse;

    [SerializeField] private InputReader inputReader;

    private void Awake()
    {
        inputReader ??= FindAnyObjectByType<InputReader>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        sensX = mouseSensX;
        sensY = mouseSensY;
    }

    private void OnEnable()
    {
        inputReader.OnMoveCamera += AttemptCameraMove;
        inputReader.OnInputSourceChange += ChangeSensDependingOnInputSource;
    }

    private void OnDisable()
    {
        inputReader.OnMoveCamera -= AttemptCameraMove;
        inputReader.OnInputSourceChange -= ChangeSensDependingOnInputSource;
    }

    private void Update()
    {
        yRotation += mouse.x;
        xRotation -= mouse.y;

        xRotation = Mathf.Clamp(xRotation, -rotationClamp, rotationClamp);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, WallRunTilt());
        orientation.rotation = Quaternion.Euler(0, yRotation, WallRunTilt());
    }

    private void ChangeSensDependingOnInputSource(bool isController)
    {
        Debug.Log(isController);

        if (isController)
        {
            sensX = gamepadSensX;
            sensY = gamepadSensY;       
        }
        else
        {
            sensX = mouseSensX;
            sensY = mouseSensY;
        }
    }

    private void AttemptCameraMove(Vector2 dir)
    {
        mouse.x = dir.x * Time.deltaTime * sensX;
        mouse.y = dir.y * Time.deltaTime * sensY;
    }

    private float WallRunTilt()
    {
        if (wallRunningScript.isPlayerWallRunning)
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
