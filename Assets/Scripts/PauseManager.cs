using System;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private Canvas pauseCanvas;

    private KeyCode pauseKey;
    private bool isPauseActive;

    private void Start()
    {
        pauseCanvas.enabled = false;
        pauseKey = KeyCode.Escape;
    }

    private void ChangePauseBool()
    { 
        isPauseActive = !isPauseActive;
    }

    private void ActivatePause()
    {
        if (Input.GetKey(pauseKey))
        {
            Time.timeScale = 0.0f;
            ChangePauseBool();
        }
    }
}
