using UnityEngine;

public class CheckGameStartInput : MonoBehaviour
{
    [SerializeField] InputReader inputReader;

    [SerializeField] private string startGameScene = "StartGame";
    [SerializeField] private string mainMenuScene = "MainMenu";

    private void OnEnable()
    {
        inputReader.OnStartGame += AttemptStartGame;
        inputReader.OnExitGame += AttemptExitGame;
    }

    private void OnDisable()
    {
        inputReader.OnStartGame -= AttemptStartGame;
        inputReader.OnExitGame -= AttemptExitGame;
    }

    private void AttemptStartGame()
    {
        NavigationManager.Instance?.UnloadScene(startGameScene);
        NavigationManager.Instance?.LoadScene(mainMenuScene);
    }

    private void AttemptExitGame()
    {
        Application.Quit();
    }
}
