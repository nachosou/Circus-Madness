using UnityEngine;

public class PreselectCanvasButton : MonoBehaviour
{
    [SerializeField] private GamepadUIController preselectButton;
    [SerializeField] private GameObject button;

    private void OnEnable()
    {
        preselectButton.SetPreselectedButton(button);
        Debug.Log($"AAAA {button.name}");
    }
}
