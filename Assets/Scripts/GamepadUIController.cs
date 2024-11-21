using UnityEngine;
using UnityEngine.EventSystems;

public class GamepadUIController : MonoBehaviour
{
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private InputReader inputReader;

    private GameObject selectedButton;

    private void OnEnable()
    {
        inputReader.OnNavigation += ResetPreselectedButton;
    }

    private void OnDisable()
    {
        inputReader.OnNavigation -= ResetPreselectedButton;
    }

    public void SetPreselectedButton(GameObject button)
    {
        if (eventSystem != null)
        {
            eventSystem.SetSelectedGameObject(button);

            selectedButton = button;
        }
    }

    public void ResetPreselectedButton()
    {
        if (eventSystem != null)
        {
            eventSystem.SetSelectedGameObject(selectedButton);
        }
    }
}

