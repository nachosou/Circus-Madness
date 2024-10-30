using UnityEngine;

public class CheckGameStartInput : MonoBehaviour
{
    private KeyCode esc;
    private KeyCode enter;

    private void Start()
    {
        esc = KeyCode.Escape;
        enter = KeyCode.E;
    }

    private void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if (Input.GetKey(esc)) 
        { 
            Application.Quit();
        }

        if (Input.GetKey(enter))
        {
            NavigationManager.Instance.UnloadScene("StartGame");
            NavigationManager.Instance.LoadScene("MainMenu");
        }
    }
}
