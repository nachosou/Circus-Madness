using UnityEngine;
using UnityEngine.UI;

public class TutorialCanvasHandler : MonoBehaviour
{
    [SerializeField] private Image[] images = new Image[3]; 
    [SerializeField] private Image displayImage; 
    [SerializeField] private GameObject tutorialCanvas;

    private int currentIndex = 0;

    private void Start()
    {
        UpdateDisplayedImage();
        Time.timeScale = 0f; 
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void GoToNextImage()
    {
        if (currentIndex < 2)
        {
            currentIndex++;
            UpdateDisplayedImage();
        }
        else
        {
            HideMenu();
        }
    }

    public void GoToPrevImage()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            UpdateDisplayedImage();
        }
    }

    private void UpdateDisplayedImage()
    {
        displayImage.sprite = images[currentIndex].sprite;
    }

    public void HideMenu()
    {
        tutorialCanvas.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
