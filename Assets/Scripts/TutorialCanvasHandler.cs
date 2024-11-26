using UnityEngine;
using UnityEngine.UI;

public class TutorialCanvasHandler : MonoBehaviour
{
    static public int imagesCuantity = 3;

    [SerializeField] private Image[] images = new Image[imagesCuantity]; 
    [SerializeField] private Image displayImage; 
    [SerializeField] private GameObject tutorialCanvas;

    public float lastImageIndex = 2;
    public float firstImageIndex = 0;

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
        if (currentIndex < lastImageIndex)
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
        if (currentIndex > firstImageIndex)
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
