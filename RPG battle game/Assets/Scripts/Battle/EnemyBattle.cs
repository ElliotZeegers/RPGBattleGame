using System.Collections;
using UnityEngine;

public class EnemyBattle : BattleEntity
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    public override void TurnBehaviour()
    {
        base.TurnBehaviour();
        //Simuleert een beurt
        StartCoroutine(SimulateTurn());
    }

    private IEnumerator SimulateTurn()
    {
        //Wacht 0.5 seconden
        yield return new WaitForSeconds(0.5f);
        //Selecteert een speler om aan te vallen
        _targetingScript.SelectPlayer();
        yield return new WaitForSeconds(1f);
        //Deelt de damage uit aan de gekozen speler
        BattleManager.Instance.BattlePlayers[_targetingScript.SelectedTarget].Takedamage(_damage, 1f, 1f);
        _entityRenderer.material = _defaultMaterial;
        BattleManager.Instance.EndTurn();
    }

    public override void EntityEndTurn()
    {
        base.EntityEndTurn();
    }
}
