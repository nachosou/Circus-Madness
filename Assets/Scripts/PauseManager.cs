using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private Canvas pauseCanvas;
    [SerializeField] private Canvas optionsCanvas;
    [SerializeField] private Canvas gameplayCanvas;
    [SerializeField] private LevelController levelController;
    [SerializeField] private InputReader inputReader;

    private bool isPauseActive;

    private void Start()
    {
        pauseCanvas.enabled = false;
        optionsCanvas.enabled = false;
        isPauseActive = false;
    }

    private void OnEnable()
    {
        inputReader.OnPause += AttemptPause;
    }

    private void OnDisable()
    {
        inputReader.OnPause -= AttemptPause;
    }

    private void AttemptPause()
    {
        if (isPauseActive)
            ResumeGame();
        else
            PauseGame();
    }

    private void PauseGame()
    {
        Time.timeScale = 0.0f;      
        isPauseActive = true;      
        pauseCanvas.enabled = true;
        gameplayCanvas.enabled = false;  
        Cursor.lockState = CursorLockMode.None;  
        Cursor.visible = true;        
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;    
        isPauseActive = false;        
        pauseCanvas.enabled = false;
        gameplayCanvas.enabled = true; 
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false;       
    }

    public void MenuButton()
    {
        isPauseActive = false;
        pauseCanvas.enabled = false;
        Time.timeScale = 1.0f;
        NavigationManager.Instance.UnloadScene(levelController.thisLevelName);
        NavigationManager.Instance.LoadScene("MainMenu");
    }   

    public void ResetLevel()
    {
        if (!NavigationManager.Instance)
        {
            Debug.LogError($"{nameof(NavigationManager)} has no instance! Maybe you didn't play from Root?");
            return;
        }
        NavigationManager.Instance?.UnloadScene(levelController.thisLevelName);
        NavigationManager.Instance?.LoadScene(levelController.thisLevelName, true);

        Scene activeScene = SceneManager.GetSceneByName(levelController.thisLevelName);
        SceneManager.SetActiveScene(activeScene);

        ResumeGame();
    }

    public void OpenOptions()
    {
        pauseCanvas.enabled = false;
        optionsCanvas.enabled = true;
    }

    public void CloseOptions()
    {
        optionsCanvas.enabled = false;
        pauseCanvas.enabled = true;
    }
}
