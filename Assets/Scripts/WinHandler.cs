using UnityEngine;
using UnityEngine.SceneManagement;

public class WinHandler : MonoBehaviour
{
    [SerializeField] private LevelController levelController;
    private Canvas winCanvas;

    public bool hasPlayerWon;

    [SerializeField] private string mainMenuScene = "MainMenu";

    private void Start()
    {
        winCanvas = GetComponent<Canvas>();
        hasPlayerWon = false;
        winCanvas.enabled = false;     
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
        winCanvas.enabled = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void GoToNextLevel()
    {
        NavigationManager.Instance.UnloadScene(levelController.thisLevelName);
        NavigationManager.Instance.LoadScene(levelController.nextLevelName, true);
        hasPlayerWon = false;

        Scene activeScene = SceneManager.GetSceneByName(levelController.nextLevelName);
        SceneManager.SetActiveScene(activeScene);
    }

    public void RestartLevel()
    {
        NavigationManager.Instance.UnloadScene(levelController.thisLevelName);
        NavigationManager.Instance.LoadScene(levelController.thisLevelName, true);
        hasPlayerWon = false;

        Scene activeScene = SceneManager.GetSceneByName(levelController.thisLevelName);
        SceneManager.SetActiveScene(activeScene);
    }    

    public void GoToMenu()
    {
        NavigationManager.Instance.UnloadScene(levelController.thisLevelName);
        NavigationManager.Instance.LoadScene(mainMenuScene);
        hasPlayerWon = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
