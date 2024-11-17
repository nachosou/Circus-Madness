using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationManager : MonoBehaviour
{
    [SerializeField] private string SceneName = "MainMenu";
    public static NavigationManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadScene(SceneName);
    }

    public void LoadScene(string sceneName, bool isActive = false)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName, isActive));
    }

    public void UnloadScene(string sceneName)
    {
        StartCoroutine(UnloadSceneCoroutine(sceneName));
    }

    private IEnumerator LoadSceneCoroutine(string sceneName, bool isActive)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        if(isActive) 
        {
            Scene activeScene = SceneManager.GetSceneByName(sceneName);

            SceneManager.SetActiveScene(activeScene);   
        }
    }

    private IEnumerator UnloadSceneCoroutine(string sceneName)
    {
        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(sceneName);

        while (!asyncUnload.isDone)
        {
            yield return null;
        }
    }
}
