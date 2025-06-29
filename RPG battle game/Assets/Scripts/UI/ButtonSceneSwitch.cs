using UnityEngine;
using UnityEngine.UI;

public class ButtonSceneSwitch : MonoBehaviour
{
    //Referentie naar de knop waarop geklikt moet worden
    [SerializeField] private Button _button;
    //Naam van de scene waarnaar moet worden geswitcht bij het klikken
    [SerializeField] private string _sceneName = "MainMenu";

    void Start()
    {
        //Voeg een listener toe aan de knop die de scene verandert wanneer erop geklikt wordt
        _button.onClick.AddListener(() =>
        {
            //Controleer of de SceneSwitcher instantie bestaat voordat er wordt geswitcht
            if (SceneSwitcher.Instance != null)
            {
                SceneSwitcher.Instance.OnSwitchScene(_sceneName);
            }
        });
    }
}
