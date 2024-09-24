using UnityEngine;

public class LevelController : MonoBehaviour
{
    public string thisLevelName;
    public string nextLevelName;

    private KeyCode nextLevelButton = KeyCode.N;

    private void Update()
    {
        AdvanceLevelIfPressedButton();
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
}
