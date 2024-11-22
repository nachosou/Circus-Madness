using UnityEngine;
using UnityEngine.SceneManagement;

public class WinHandler : MonoBehaviour
{
    [SerializeField] private LevelController levelController;
    [SerializeField] private GameObject winCanvas;

    public bool hasPlayerWon;

    [SerializeField] private string mainMenuScene = "MainMenu";

    private void Start()
    {
        hasPlayerWon = false;
        winCanvas.SetActive(false);     
    }

    private void Update()
    {
        if (hasPlayerWon)
        {
            ActivateWinCanvas();
        }
    }

    public void ActivateWinCanvas()
    {
        Time.timeScale = 0.0f;
        winCanvas.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void GoToNextLevel()
    {
        NavigationManager.Instance?.UnloadScene(levelController.thisLevelName);
        NavigationManager.Instance?.LoadScene(levelController.nextLevelName, true);
        Time.timeScale = 1.0f;
        hasPlayerWon = false;
    }

    public void RestartLevel()
    {
        NavigationManager.Instance?.UnloadScene(levelController.thisLevelName);
        NavigationManager.Instance?.LoadScene(levelController.thisLevelName, true);
        hasPlayerWon = false;
    }    

    public void GoToMenu()
    {
        NavigationManager.Instance?.UnloadScene(levelController.thisLevelName);
        NavigationManager.Instance?.LoadScene(mainMenuScene);
        hasPlayerWon = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
