using System.Linq;
using UnityEngine;

public class TargetEntity : MonoBehaviour
{
    //Index van de huidige geselecteerde vijand
    private int _currentSelectedTarget = 0;
    [SerializeField] private Material _targetMaterial;
    [SerializeField] private Material _defaultMaterial;

    //Property zodat andere scripts de waarde van _currentSelectedTarget kunnen ophalen
    public int SelectedTarget { get { return _currentSelectedTarget; } }

    public void SelectEnemy(IPlayerInteractInput _input)
    {
        //Verandert de geselecteerde target op basis van input
        _currentSelectedTarget += _input.SelectOption();

        //Zorgt ervoor dat _currentSelectedTarget binnen de grenzen van BattleEnemies blijft. Als _currentSelectedTarget te hoog is, wordt hij op 0 gezet, als hij te laag is wordt hij op het laatste element gezet.
        if (_currentSelectedTarget >= BattleManager.Instance.BattleEnemies.Count())
        {
            _currentSelectedTarget = 0;
        }
        else if (_currentSelectedTarget < 0)
        {
            _currentSelectedTarget = BattleManager.Instance.BattleEnemies.Count() - 1;
        }

        //Zorgt ervoor dat het visueel duidelijk is wie er word getarget
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

    //Laat een vijand een willekeurige speler targeten
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
