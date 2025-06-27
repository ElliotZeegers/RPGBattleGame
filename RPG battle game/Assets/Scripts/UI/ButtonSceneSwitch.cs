using UnityEngine;
using UnityEngine.UI;

public class ButtonSceneSwitch : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private string _sceneName = "MainMenu";

    void Start()
    {
        _button.onClick.AddListener(() =>
        {
            if (SceneSwitcher.Instance != null)
            {
                SceneSwitcher.Instance.OnSwitchScene(_sceneName);
            }
        });
    }
}
