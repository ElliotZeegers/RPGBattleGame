using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    //Singleton instance van GameplayManager, zodat je overal makkelijk erbij kunt
    public static GameplayManager Instance { get; private set; }

    //Mogelijke game states
    public enum GameState
    {
        Overworld,
        Battle
    }

    [SerializeField] private Text _enemiesdefeatedCounterText;
    private int _enemiesdefeatedCount;
    private BattleHandler _battleHandler;
    private SwapInput _swapInputScript;
    private bool _battleStarting = false;

    GameState _state = GameState.Overworld;

    private IPausable[] _pausableObjects;

    //Een property zodat andere script kunnen weten in welke state de game zit
    public GameState State { get { return _state; } }


    private void Awake()
    {
        //Als er al een instance is, vernietig deze nieuwe
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;

        _battleHandler = GetComponent<BattleHandler>();
        //Haalt het component op in child object
        _swapInputScript = GetComponentInChildren<SwapInput>();
    }

    //Pauzeer de overworld en start battle, maar alleen als er geen battle loopt
    public void Pause(GameObject pPlayerGroup, GameObject pEnemyGroup, GameObject pStartedBattleEnemy)
    {
        if (_battleStarting || _state == GameState.Battle)
        {
            return;
        }
        //Slaat alle objects met IPauseable op in de list _pausableObjects
        _pausableObjects = Object.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IPausable>().ToArray();
        //Run in ieder script in _pausableObjects Pause() en geef false mee
        foreach (IPausable p in _pausableObjects)
        {
            p.Pause(false);
        }
        _state = GameState.Battle;
        //Start de battle
        _battleHandler.EnterBattle(pPlayerGroup, pEnemyGroup, pStartedBattleEnemy);
    }

    //Hervat de overworld na battle
    public void UnPause()
    {
        //Run in ieder script in _pausableObjects Pause() en geef true mee
        foreach (IPausable p in _pausableObjects)
        {
            p.Pause(true);
        }
        //Zorgt ervoor dat de input weer op keyboard wordt gezet om bugs te voorkomen
        _swapInputScript.OnChangeGameState(true);
    }

    public void StartBattle()
    {
        //Zorgt ervoor dat de input weer op keyboard wordt gezet om bugs te voorkomen
        _swapInputScript.OnChangeGameState(false);
    }

    public void EndBattleWin()
    {
        _battleHandler.ExitBattleWin();
        //Telt 1 op bij _enemiesdefeatedCount
        _enemiesdefeatedCount++;
        //Update de tekst
        _enemiesdefeatedCounterText.text = "Enemies defeated: " + _enemiesdefeatedCount;
        //Zet _state terug naar Overworld
        _state = GameState.Overworld;
        _battleStarting = false;
        //Checkt of je al hebt gewonnen
        CheckForWin();
    }

    public void EndBattleRun()
    {
        _battleHandler.ExitBattleRun();
        _state = GameState.Overworld;
        _battleStarting = false;
    }

    public void CheckForWin()
    {
        //als je 3 of meer enemies hebt verslagen dan ga je naar de WinScene
        if (_enemiesdefeatedCount >= 3)
        {
            SceneSwitcher.Instance.OnSwitchScene("WinScene");
        }
    }

}
