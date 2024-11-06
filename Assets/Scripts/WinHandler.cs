using UnityEngine;

public class WinHandler : MonoBehaviour
{
    [SerializeField] private LevelController levelController;
    private Canvas winCanvas;

    public bool hasPlayerWon;

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
        NavigationManager.Instance.LoadScene(levelController.nextLevelName);
        hasPlayerWon = false;
    }

    public void RestartLevel()
    {
        NavigationManager.Instance.UnloadScene(levelController.thisLevelName);
        NavigationManager.Instance.LoadScene(levelController.thisLevelName);
        hasPlayerWon = false;
    }    

    public void GoToMenu()
    {
        NavigationManager.Instance.UnloadScene(levelController.thisLevelName);
        NavigationManager.Instance.LoadScene("MainMenu");
        hasPlayerWon = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
