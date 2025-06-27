using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattlePosManager : MonoBehaviour
{
    private PosPriority[] _spwans;
    private BattleEntity[] _entities;
    private List<EnemyBattle> _enemyList = new List<EnemyBattle>();
    private List<PlayerBattle> _playerList = new List<PlayerBattle>();
    private List<PosPriority> _enemySpawnsList = new List<PosPriority>();
    private List<PosPriority> _playerSpawnsList = new List<PosPriority>();

    void Start()
    {
        _entities = BattleManager.Instance.BattleEntities.ToArray();
        _spwans = UnityEngine.Object.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.InstanceID).OfType<PosPriority>().ToArray();

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

        for (int i = 0; i < _spwans.Length; i++)
        {
            if (_spwans[i].PosType == PosPriority.PosTypes.Enemy)
            {
                _enemySpawnsList.Add(_spwans[i]);
            }
            else if (_spwans[i].PosType == PosPriority.PosTypes.Player)
            {
                _playerSpawnsList.Add(_spwans[i]);
            }
        }


        _enemySpawnsList = _enemySpawnsList.OrderByDescending(p => p.Priority).ToList();
        _playerSpawnsList = _playerSpawnsList.OrderByDescending(p => p.Priority).ToList();

        _enemyList = _enemyList.OrderByDescending(e => e.BattlePriority).ToList();
        _playerList = _playerList.OrderByDescending(e => e.BattlePriority).ToList();


        for (int i = 0; i < Mathf.Min(_enemyList.Count(), _enemySpawnsList.Count()); i++)
        {
            _enemyList[i].transform.position = _enemySpawnsList[i].transform.position;
        }

        for (int i = 0; i < Mathf.Min(_playerList.Count(), _playerSpawnsList.Count()); i++)
        {
            _playerList[i].transform.position = _playerSpawnsList[i].transform.position;
        }
    }
}
