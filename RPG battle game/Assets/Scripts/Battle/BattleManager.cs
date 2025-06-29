using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class BattleManager : MonoBehaviour
{
    //Zorgt ervoor dat de BattleManager een singleton is zodat andere scripts erbij kunnen als dat nodig is
    public static BattleManager Instance { get; private set; }

    //Lists van de entities, spelers en enemies
    private List<BattleEntity> _battleEntities = new List<BattleEntity>();
    private List<EnemyBattle> _battleEnemies = new List<EnemyBattle>();
    private List<PlayerBattle> _battlePlayers = new List<PlayerBattle>();

    //Een event dat wordt geactiveerd aan het einde van elke beurt
    public delegate void TurnEndHandler();
    public event TurnEndHandler OnEndTurn;

    //Regelt de UI van de battle (niet van de entities zelf)
    [SerializeField] private BattleUIManager _battleUIManager;
    //Houdt bij hoeveel beurten er zijn geweest
    private int _turnUICounter = 1;

    //Houdt bij wie er aan de beurt is en de volgorde van de beurten
    private List<BattleEntity> _turnList;
    private int _currentTurnIndex = 0;
    private BattleEntity _currentEntity;

    public int TurnCounter { get { return _turnUICounter; } }

    //Properties van de lists hiervoor zodat anderen erbij kunnen als dat nodig is
    public List<BattleEntity> BattleEntities { get { return _battleEntities; } }
    public List<EnemyBattle> BattleEnemies { get { return _battleEnemies; } }
    public List<PlayerBattle> BattlePlayers { get { return _battlePlayers; } }

    void Awake()
    {
        //Checkt of er al een instantie van BattleManager bestaat en of dat niet deze is
        if (Instance != null && Instance != this)
        {
            //Vernietigt de hele battle
            Destroy(gameObject.transform.root.gameObject);
            return;
        }
        //Stelt deze instantie in als de singleton
        Instance = this;

        //Zoekt alle BattleEntity scripts in de scene, zet ze in een lijst en sorteert ze op InstanceID
        _battleEntities = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.InstanceID).OfType<BattleEntity>().ToList();

        //Loop die ervoor zorgt dat alle enemies in hun eigen lijst belanden en alle spelers ook
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
        //Subscribed HandleTurnEnded aan OnEndTurn
        OnEndTurn += HandleTurnEnded;
        //Subscribed _battleUIManager.UpdateTurnUI aan OnEndTurn
        OnEndTurn += _battleUIManager.UpdateTurnUI;
        StartNextTurn();
    }

    void SetupTurnList()
    {
        //Sorteert de entities op basis van speed van hoog naar laag en stopt ze in de list _turnList
        _turnList = _battleEntities.OrderByDescending(e => e.Speed).ToList();
    }

    void StartNextTurn()
    {

        //Zorgt ervoor dat _currentTurnIndex binnen de grenzen van _turnList blijft. Als de index te hoog is, wordt hij op 0 gezet, als hij te laag is wordt hij op het laatste element gezet.
        if (_currentTurnIndex >= _turnList.Count())
        {
            _currentTurnIndex = 0;
        }
        else if (_currentTurnIndex < 0)
        {
            _currentTurnIndex = _turnList.Count() - 1;
        }

        //Slaat de huidige entity die aan de beurt is op in _currentEntity
        _currentEntity = _turnList[_currentTurnIndex];

        //Start de TurnBehaviour van een entity maar wacht eerst 1 frame om bugs met input te voorkomen
        StartCoroutine(DelayedTurnBehaviour());
    }

    public void EndTurn()
    {
        //Roept alle methods aan die gesubscribed zijn aan OnEndTurn, maar alleen als er minimaal 1 subscriber is
        OnEndTurn?.Invoke();
    }

    void HandleTurnEnded()
    {
        //Laat de huidige entity zijn beurt beëindigen
        _currentEntity.EntityEndTurn();
        //Zorgt dat de volgende entity aan de beurt is
        _currentTurnIndex++;
        _turnUICounter++;
        //Zorgt dat er gecheckt wordt of er entities dood zijn
        CheckForDeath();
        //Checkt of de spelers of enemies al hebben gewonnen
        CheckForWin();
        //Start de volgende beurt
        StartNextTurn();
    }

    private void CheckForDeath()
    {
        //Een list om entities in op te slaan die weg moeten
        List<BattleEntity> removeEntities = new List<BattleEntity>();
        //Loopt door _battleEntities list om de entities weg te halen die dood zijn
        for (int i = 0; i < _battleEntities.Count(); i++)
        {
            if (_battleEntities[i].Hp <= 0)
            {
                Destroy(_battleEntities[i].gameObject);
                removeEntities.Add(_battleEntities[i]);

            }
        }

        //Loopt door alle entities die weg moeten zodat ze allenmaal uit de juiste lijsten worden gehaald
        foreach (BattleEntity entity in removeEntities)
        {
            //Haalt de plek op van de entity in _tunrList
            int removedIndex = _turnList.IndexOf(entity);
            _turnList.Remove(entity);
            _battleEntities.Remove(entity);

            //Deze check zorgt ervoor dat er geen beurten per ongeluk worden overgeslagen
            if (_currentTurnIndex > removedIndex)
            {
                _currentTurnIndex--;
            }

            //Haalt de enity uit de juiste lijsten
            if (entity.GetType() == typeof(PlayerBattle))
            {
                _battlePlayers.Remove((PlayerBattle)entity);
            }
            else if (entity.GetType() == typeof(EnemyBattle))
            {
                _battleEnemies.Remove((EnemyBattle)entity);
            }
        }

    }

    private IEnumerator DelayedTurnBehaviour()
    {
        //Wacht 1 frame
        yield return null;
        //Roept _currentEntity.TurnBehaviour() aan
        _currentEntity.TurnBehaviour();
    }

    private void CheckForWin()
    {
        //Checkt of er 0 enemies zijn als dat zo is rond die de battle af met een winst voor de speler
        if (_battleEnemies.Count() == 0)
        {
            StartCoroutine(EndBattle("Player Wint"));
            GameplayManager.Instance.EndBattleWin();
            foreach (BattleEntity battleEntity in _battleEntities)
            {
                battleEntity.enabled = false;
            }

            DisableManager();
            return;
        }
        //Checkt of er 0 players zijn als dat zo is rond die de battle af met een verlies voor de speler
        if (_battlePlayers.Count() == 0)
        {
            StartCoroutine(EndBattle("Enemies Winnen"));
            foreach (BattleEntity battleEntity in _battleEntities)
            {
                battleEntity.enabled = false;
            }
            DisableManager();
            SceneSwitcher.Instance.OnSwitchScene("LoseScene");

            return;
        }
    }

    public void DisableManager()
    {
        this.enabled = false;
    }


    public IEnumerator EndBattle(string pDisplayText)
    {
        yield return new WaitForSeconds(0.5f);
        _battleUIManager.UpdateWinnerUI(pDisplayText);
        yield return new WaitForSeconds(1f);
    }
}
