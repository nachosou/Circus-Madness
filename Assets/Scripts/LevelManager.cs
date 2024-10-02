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
        PlayerDies();
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
        NavigationManager.Instance.UnloadScene(thisLevelName);
        NavigationManager.Instance.LoadScene(nextLevelName);
    }

    private void PlayerDies()
    {
        if (playerHealth.health <= 0)
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        }
    }
}
