using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public static SceneSwitcher Instance { get; private set; }

    [SerializeField] string _sceneName;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void OnSwitchScene(string pSceneName)
    {
        _sceneName = pSceneName;
        SceneManager.LoadScene(_sceneName);
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}
