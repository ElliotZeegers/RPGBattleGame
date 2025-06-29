using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattlePosManager : MonoBehaviour
{
    private PosPriority[] _spawns;
    private BattleEntity[] _entities;
    private List<EnemyBattle> _enemyList = new List<EnemyBattle>();
    private List<PlayerBattle> _playerList = new List<PlayerBattle>();
    private List<PosPriority> _enemySpawnsList = new List<PosPriority>();
    private List<PosPriority> _playerSpawnsList = new List<PosPriority>();

    void Start()
    {
        //Zet de list BattleEntities om naar een array en slaat dit op in _entities
        _entities = BattleManager.Instance.BattleEntities.ToArray();
        //Haalt alle objecten op met PosPriority en slaat ze op in de array _spawns
        _spawns = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.InstanceID).OfType<PosPriority>().ToArray();


        //Scheidt de entities in vijanden en spelers en vult de juiste lijsten
        for (int i = 0; i < _entities.Length; i++)
        {
            if (_entities[i] is EnemyBattle)
            {
                _enemyList.Add((EnemyBattle)_entities[i]);
            }
            else if (_entities[i] is PlayerBattle)
            {
                _playerList.Add((PlayerBattle)_entities[i]);
            }
        }

        //Scheidt de spawnposities op basis van het PosType
        for (int i = 0; i < _spawns.Length; i++)
        {
            if (_spawns[i].PosType == PosPriority.PosTypes.Enemy)
            {
                _enemySpawnsList.Add(_spawns[i]);
            }
            else if (_spawns[i].PosType == PosPriority.PosTypes.Player)
            {
                _playerSpawnsList.Add(_spawns[i]);
            }
        }

        //Sorteert de spawnlijsten op basis van hun priority (hoog naar laag)
        _enemySpawnsList = _enemySpawnsList.OrderByDescending(p => p.Priority).ToList();
        _playerSpawnsList = _playerSpawnsList.OrderByDescending(p => p.Priority).ToList();
        // Sorteert de entiteitlijsten op battle priority (hoog naar laag)
        _enemyList = _enemyList.OrderByDescending(e => e.BattlePriority).ToList();
        _playerList = _playerList.OrderByDescending(e => e.BattlePriority).ToList();

        //Zet de vijanden op de juiste spawnposities
        for (int i = 0; i < Mathf.Min(_enemyList.Count(), _enemySpawnsList.Count()); i++)
        {
            _enemyList[i].transform.position = _enemySpawnsList[i].transform.position;
        }

        //Zet de spelers op de juiste spawnposities
        for (int i = 0; i < Mathf.Min(_playerList.Count(), _playerSpawnsList.Count()); i++)
        {
            _playerList[i].transform.position = _playerSpawnsList[i].transform.position;
        }
    }
}
