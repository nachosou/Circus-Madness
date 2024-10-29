using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject creditsCanvas;

    public void PlayGame()
    {
        NavigationManager.Instance.UnloadScene("MainMenu");  
        NavigationManager.Instance.LoadScene("Tutorial");
    }

    public void OpenCredits()
    {
        mainMenuCanvas.SetActive(false);
        creditsCanvas.SetActive(true);
    }

    public void CloseCredits()
    {
        creditsCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
