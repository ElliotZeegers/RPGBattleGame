using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }

    List<BattleEntity> _battleEntities = new List<BattleEntity>();
    List<EnemyBattle> _battleEnemies = new List<EnemyBattle>();
    List<PlayerBattle> _battlePlayers = new List<PlayerBattle>();
    public List<BattleEntity> BattleEntities { get { return _battleEntities; } }
    public List<EnemyBattle> BattleEnemies { get { return _battleEnemies; } }
    public List<PlayerBattle> BattlePlayers { get { return _battlePlayers; } }

    public delegate void TurnEndHandler();
    public event TurnEndHandler OnEndTurn;

    [SerializeField] private BattleUIManager _battleUIManager;
    private int _turnCounter = 1;
    public int TurnCounter { get { return _turnCounter; } }

    private List<BattleEntity> _turnList;
    private int _currentTurnIndex = 0;
    BattleEntity _currentEntity;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        _battleEntities = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.InstanceID).OfType<BattleEntity>().ToList();

        for (int i = 0; i < _battleEntities.Count(); i++)
        {
            if (_battleEntities[i].GetType() == typeof(EnemyBattle))
            {
                _battleEnemies.Add((EnemyBattle)_battleEntities[i]);
            }
            else if (_battleEntities[i].GetType() == typeof(PlayerBattle))
            {
                _battlePlayers.Add((PlayerBattle)_battleEntities[i]);
            }
        }
    }

    void Start()
    {
        _battleUIManager.UpdateTurnUI();
        SetupTurnList();
        OnEndTurn += HandleTurnEnded;
        OnEndTurn += _battleUIManager.UpdateTurnUI;
        StartNextTurn();
    }

    void Update()
    {

    }

    void SetupTurnList()
    {
        _turnList = _battleEntities.OrderByDescending(e => e.Speed).ToList();

    }

    void StartNextTurn()
    {

        if (_currentTurnIndex >= _turnList.Count())
        {
            _currentTurnIndex = 0;
        }
        else if ( _currentTurnIndex < 0)
        {
            _currentTurnIndex = _turnList.Count() - 1;
        }

        _currentEntity = _turnList[_currentTurnIndex];

        if (_currentEntity.Hp <= 0)
        {
            _currentTurnIndex++;
            StartNextTurn();
            return;
        }

        _currentEntity.TurnBehaviour();
    }

    public void EndTurn()
    {
        OnEndTurn?.Invoke();
    }

    void HandleTurnEnded()
    {
        _currentEntity.EntityEndTurn();
        _currentEntity.IsMyTurn = false;
        _currentTurnIndex++;
        _turnCounter++;

        CheckForDeath();
        CheckForWin();

        StartNextTurn();
    }

    private void CheckForDeath()
    {
        List<BattleEntity> removeEntities = new List<BattleEntity>();
        for (int i = 0; i < _battleEntities.Count(); i++)
        {
            if (_battleEntities[i].Hp <= 0)
            {
                print("ik ben af " + _battleEntities[i].Name);
                Destroy(_battleEntities[i].gameObject);
                removeEntities.Add(_battleEntities[i]);

            }
        }

        foreach (BattleEntity entity in removeEntities)
        {
            int removedIndex = _turnList.IndexOf(entity);
            _turnList.Remove(entity);
            _battleEntities.Remove(entity);

            if (_currentTurnIndex > removedIndex)
            {
                _currentTurnIndex--;
            }

            if (entity.GetType() == typeof(PlayerBattle))
            {
                PlayerBattle playerBattle = (PlayerBattle)entity;
                //playerBattle.UnSubscribe();
                _battlePlayers.Remove((PlayerBattle)entity);
            }
            else if (entity.GetType() == typeof(EnemyBattle))
            {
                _battleEnemies.Remove((EnemyBattle)entity);
            }
        }

    }
    private void CheckForWin()
    {
        if (_battleEnemies.Count() == 0)
        {
            StartCoroutine(EndBattle("Player Wins"));
            GameplayManager.Instance.EndBattle();
            foreach(BattleEntity battleEntity in _battleEntities)
            {
                battleEntity.enabled = false;
            }
            //foreach(PlayerBattle playerBattle in _battlePlayers)
            //{
            //    playerBattle.UnSubscribe();
            //}
            this.enabled = false;
            return;
        }
        if (_battlePlayers.Count() == 0)
        {
            StartCoroutine(EndBattle("Enemies Win"));
            foreach (BattleEntity battleEntity in _battleEntities)
            {
                battleEntity.enabled = false;
            }
            SceneSwitcher.Instance.OnSwitchScene("LoseScene");
            this.enabled = false;

            return;
        }
    }

    public IEnumerator EndBattle(string pDisplayText)
    {
        yield return new WaitForSeconds(0.5f);
        _battleUIManager.UpdateWinnerUI(pDisplayText);
        yield return new WaitForSeconds(1f);

    }
}
