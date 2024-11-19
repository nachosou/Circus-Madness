using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject creditsCanvas;

    [SerializeField] private string mainMenuScene = "MainMenu";
    [SerializeField] private string tutorialScene = "Tutorial";

    public void PlayGame()
    {
        NavigationManager.Instance?.UnloadScene(mainMenuScene);  
        NavigationManager.Instance?.LoadScene(tutorialScene, true);

        Scene activeScene = SceneManager.GetSceneByName(tutorialScene);
        SceneManager.SetActiveScene(activeScene);
        RenderSettings.fog = true;
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
