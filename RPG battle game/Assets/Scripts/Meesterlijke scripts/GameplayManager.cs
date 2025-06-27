using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance { get; private set; }

    public enum GameState
    {
        Overworld,
        Battle
    }

    private int _enemiesdefeatedCount;
    [SerializeField] private Text _enemiesdefeatedCounterText;
    private BattleHandler _startBattleScript;

    GameState _state = GameState.Overworld;

    //public event Action OnSwapInput;

    public GameState State { get { return _state; } }

    private IPausable[] _pausableObjects;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;

        _startBattleScript = GetComponent<BattleHandler>();
    }

    public void Pause(GameObject pPlayerGroup, GameObject pEnemyGroup, GameObject pStartedBattleEnemy)
    {
        _pausableObjects = UnityEngine.Object.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IPausable>().ToArray();
        foreach (IPausable p in _pausableObjects)
        {
            p.Pause(false);
        }
        _state = GameState.Battle;
        _startBattleScript.EnterBattle(pPlayerGroup, pEnemyGroup, pStartedBattleEnemy);
    }

    public void EndBattle()
    {
        _startBattleScript.ExitBattle();
        _enemiesdefeatedCount++;
        _enemiesdefeatedCounterText.text = "Enemies defeated: " + _enemiesdefeatedCount;
        _state = GameState.Overworld;
        CheckForWin();
    }

    public void CheckForWin()
    {
        if (_enemiesdefeatedCount >= 3)
        {
            SceneSwitcher.Instance.OnSwitchScene("WinScene");
        }
    }

    public void UnPause()
    {
        foreach (IPausable p in _pausableObjects)
        {
            p.Pause(true);
        }
    }

    //public void SwapInput()
    //{
    //    OnSwapInput?.Invoke();
    //}
}
