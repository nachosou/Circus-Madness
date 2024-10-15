using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] GameObject mainMenuCanvas;
    [SerializeField] GameObject creditsCanvas;

    public void PlayGame()
    {
        NavigationManager.Instance.UnloadScene("MainMenu");  
        NavigationManager.Instance.LoadScene("Lvl1");
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
