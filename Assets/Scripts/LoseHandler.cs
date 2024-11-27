using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseHandler : MonoBehaviour
{
    [SerializeField] private LevelController levelController;
    [SerializeField] private GameObject loseCanvas;
    [SerializeField] private DeathEventSO deathEventData;

    [SerializeField] private string mainMenuScene = "MainMenu";

    private void Start()
    {
        loseCanvas.SetActive(false);
    }

    private void OnEnable()
    {
        deathEventData.OnPlayerDeath += ActivateLoseCanvas;
    }

    private void OnDisable()
    {
        deathEventData.OnPlayerDeath -= ActivateLoseCanvas;
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
    }

    public void ResetLevel()
    {       
        NavigationManager.Instance?.UnloadScene(levelController.thisLevelName);
        NavigationManager.Instance?.LoadScene(levelController.thisLevelName, true);
    }
}
