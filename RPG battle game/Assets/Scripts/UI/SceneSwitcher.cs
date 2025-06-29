using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    //Singleton instantie van de SceneSwitcher
    public static SceneSwitcher Instance { get; private set; }

    //Naam van de scene die geladen moet worden
    [SerializeField] string _sceneName;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        //Zorgt ervoor dat dit object niet vernietigd wordt bij het laden van een nieuwe scene
        DontDestroyOnLoad(gameObject);
    }

    //Method om van scene te switchen
    public void OnSwitchScene(string pSceneName)
    {
        _sceneName = pSceneName;
        SceneManager.LoadScene(_sceneName);
    }

    //Method om de game af te sluiten
    public void OnClickExit()
    {
        Application.Quit();
    }
}
