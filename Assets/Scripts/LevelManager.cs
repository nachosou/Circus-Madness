using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public string thisLevelName;
    public string nextLevelName;

    [SerializeField] private HealthSystem playerHealth;
    [SerializeField] private DeathEventSO deathEventData;

    private void Update()
    {
        ResetLevelIfPlayerDies();
    }

    public void AdvanceToNextLevel()
    {
        if (!NavigationManager.Instance)
        {
            Debug.LogError($"{nameof(NavigationManager)} has no instance! Maybe you didn't play from Root?");
            return;
        }

        NavigationManager.Instance?.UnloadScene(thisLevelName);
        NavigationManager.Instance?.LoadScene(nextLevelName, true);

        Scene activeScene = SceneManager.GetSceneByName(nextLevelName);
        SceneManager.SetActiveScene(activeScene);
    }

    private void ResetLevelIfPlayerDies()
    {
        if (playerHealth.health <= 0)
        {
            deathEventData.OnPlayerDeath?.Invoke();
        }
    }
}
