using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseHandler : MonoBehaviour
{
    [SerializeField] private LevelController levelController;
    [SerializeField] private GameObject loseCanvas;

    [SerializeField] private string mainMenuScene = "MainMenu";

    public bool hasPlayerDied;

    private void Start()
    {
        loseCanvas.SetActive(false);
        hasPlayerDied = false;
    }

    private void Update()
    {
        if (hasPlayerDied) 
        {
            ActivateLoseCanvas();
        }
    }

    private void ActivateLoseCanvas()
    {
        Time.timeScale = 0.0f;
        loseCanvas.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;       
    }

    public void MenuButton()
    {
        NavigationManager.Instance?.UnloadScene(levelController.thisLevelName);
        NavigationManager.Instance?.LoadScene(mainMenuScene);
        hasPlayerDied = false;
    }

    public void ResetLevel()
    {       
        NavigationManager.Instance?.UnloadScene(levelController.thisLevelName);
        NavigationManager.Instance?.LoadScene(levelController.thisLevelName, true);
    }
}
