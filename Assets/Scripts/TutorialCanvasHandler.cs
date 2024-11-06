using UnityEngine;
using UnityEngine.UI;

public class TutorialCanvasHandler : MonoBehaviour
{
    [SerializeField] private Image[] images = new Image[3]; 
    [SerializeField] private Image displayImage; 
    [SerializeField] private Canvas tutorialCanvas;
    //[SerializeField] private bool showOnce = true;

    private int currentIndex = 0;

    private void Start()
    {
        //if (showOnce && PlayerPrefs.GetInt("TutorialShown", 0) == 1)
        //{
        //    tutorialCanvas.gameObject.SetActive(false);
        //    return;
        //}

        //PlayerPrefs.SetInt("TutorialShown", 1);
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
        tutorialCanvas.gameObject.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
