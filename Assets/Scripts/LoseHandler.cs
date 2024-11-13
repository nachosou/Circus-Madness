using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseHandler : MonoBehaviour
{
    [SerializeField] private LevelController levelController;
    private Canvas loseCanvas;

    public bool hasPlayerDied;

    private void Start()
    {
        loseCanvas = GetComponent<Canvas>();
        loseCanvas.enabled = false;
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
        loseCanvas.enabled = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;       
    }

    public void MenuButton()
    {
        NavigationManager.Instance?.UnloadScene(levelController.thisLevelName);
        NavigationManager.Instance?.LoadScene("MainMenu");
        hasPlayerDied = false;
    }

    public void ResetLevel()
    {
        NavigationManager.Instance?.UnloadScene(levelController.thisLevelName);
        NavigationManager.Instance?.LoadScene(levelController.thisLevelName, true);

        Scene activeScene = SceneManager.GetSceneByName(levelController.thisLevelName);
        SceneManager.SetActiveScene(activeScene);
        hasPlayerDied = false;
    }
}
