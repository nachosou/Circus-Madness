using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private GameObject optionsCanvas;
    [SerializeField] private GameObject gameplayCanvas;
    [SerializeField] private LevelController levelController;
    [SerializeField] private InputReader inputReader;

    [SerializeField] private string SceneName = "MainMenu";

    private bool isPauseActive;

    private void Start()
    {
        pauseCanvas.SetActive(false);
        optionsCanvas.SetActive(false);
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
        pauseCanvas.SetActive(true);
        gameplayCanvas.SetActive(true);  
        Cursor.lockState = CursorLockMode.None;  
        Cursor.visible = true;        
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;    
        isPauseActive = false;        
        pauseCanvas.SetActive(false);
        gameplayCanvas.SetActive(true); 
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false;       
    }

    public void MenuButton()
    {
        isPauseActive = false;
        pauseCanvas.SetActive(false);
        Time.timeScale = 1.0f;
        NavigationManager.Instance.UnloadScene(levelController.thisLevelName);
        NavigationManager.Instance.LoadScene(SceneName);
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

        ResumeGame();
    }

    public void OpenOptions()
    {
        pauseCanvas.SetActive(false);
        optionsCanvas.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsCanvas.SetActive(false);
        pauseCanvas.SetActive(true);
    }
}
