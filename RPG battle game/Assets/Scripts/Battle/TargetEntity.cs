using System.Linq;
using UnityEngine;

public class TargetEntity : MonoBehaviour
{
    private int _currentSelectedTarget = 0;
    [SerializeField] private Material _targetMaterial;
    [SerializeField] private Material _defaultMaterial;

    public int SelectedTarget { get { return _currentSelectedTarget; } }

    private void Awake()
    {

    }

    public void SelectEnemy(IPlayerInteractInput _input)
    {
        _currentSelectedTarget += _input.SelectOption();

        if (_currentSelectedTarget >= BattleManager.Instance.BattleEnemies.Count())
        {
            _currentSelectedTarget = 0;
        }
        else if (_currentSelectedTarget < 0)
        {
            _currentSelectedTarget = BattleManager.Instance.BattleEnemies.Count() - 1;
        }

        for (int i = 0; i < BattleManager.Instance.BattleEnemies.Count(); i++)
        {
            if (i == _currentSelectedTarget)
            {
                BattleManager.Instance.BattleEnemies[i].EntityRenderer.material = _targetMaterial;
            }
            else
            {
                BattleManager.Instance.BattleEnemies[i].EntityRenderer.material = _defaultMaterial;
            }
        }
    }

    public void SelectPlayer()
    {
        _currentSelectedTarget = Random.Range(0, BattleManager.Instance.BattlePlayers.Count());
        BattleManager.Instance.BattlePlayers[_currentSelectedTarget].EntityRenderer.material = _targetMaterial;
    }

    public void UnTarget()
    {
        BattleManager.Instance.BattleEnemies[_currentSelectedTarget].EntityRenderer.material = _defaultMaterial;
    }
}
