using System;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private Canvas pauseCanvas;
    [SerializeField] private Canvas optionsCanvas;
    [SerializeField] LevelController levelController;

    private KeyCode pauseKey;
    private bool isPauseActive;

    private void Start()
    {
        pauseCanvas.enabled = false;
        optionsCanvas.enabled = false;
        isPauseActive = false;
        pauseKey = KeyCode.Escape;
    }

    private void Update()
    {
        InputCheck();
    }

    private void InputCheck()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            if (isPauseActive)
                ResumeGame(); 
            else
                PauseGame();   
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0.0f;      
        isPauseActive = true;      
        pauseCanvas.enabled = true;  
        Cursor.lockState = CursorLockMode.None;  
        Cursor.visible = true;        
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;    
        isPauseActive = false;        
        pauseCanvas.enabled = false; 
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false;       
    }

    public void MenuButton()
    {
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
        NavigationManager.Instance?.LoadScene(levelController.thisLevelName);

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
