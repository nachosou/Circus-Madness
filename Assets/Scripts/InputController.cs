using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
     [SerializeField] private PlayerMovement playerMovement;
     [SerializeField] private PlayerCam cam;
     [SerializeField] private Grappler grappler;
     [SerializeField] private GunSystem gun;

    private void OnEnable()
    {
       
    }

    public void SetPlayerDirection(InputAction.CallbackContext context)
    {
        playerMovement.SetDirection(context.ReadValue<Vector2>());
    }
}
