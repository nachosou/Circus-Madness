using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public string thisLevelName;
    public string nextLevelName;

    [SerializeField] HealthSystem playerHealth;

    private KeyCode nextLevelButton = KeyCode.N;

    private void Update()
    {
        AdvanceLevelIfPressedButton();
        ResetLevelIfPlayerDies();
    }

    private void AdvanceLevelIfPressedButton()
    {
        if (Input.GetKey(nextLevelButton))
        {
            AdvanceToNextLevel();
        }
    }

    public void AdvanceToNextLevel()
    {
        if (!NavigationManager.Instance)
        {
            Debug.LogError($"{nameof(NavigationManager)} has no instance! Maybe you didn't play from Root?");
            return;
        }

        NavigationManager.Instance?.UnloadScene(thisLevelName);
        NavigationManager.Instance?.LoadScene(nextLevelName);
    }

    private void ResetLevelIfPlayerDies()
    {
        if (playerHealth.health <= 0)
        {
            playerHealth.health = 200;
            NavigationManager.Instance?.UnloadScene(thisLevelName);
            NavigationManager.Instance?.LoadScene(thisLevelName);
        }
    }
}
