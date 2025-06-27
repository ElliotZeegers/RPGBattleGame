using System.Collections;
using UnityEngine;

public class EnemyBattle : BattleEntity
{
    protected override void Awake()
    {
        base.Awake();
        _damageable = GetComponent<IDamageable>();
    }

    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        
    }

    public override void TurnBehaviour()
    {
        base.TurnBehaviour();
        StartCoroutine(SimulateTurn());
    }

    IEnumerator SimulateTurn()
    {
        yield return new WaitForSeconds(0.5f);
        _targetingScript.SelectPlayer();
        yield return new WaitForSeconds(1f);
        BattleManager.Instance.BattlePlayers[_targetingScript.SelectedTarget].Takedamage(_damage, 1f, 1f);
        _entityRenderer.material = _defaultMaterial;
        BattleManager.Instance.EndTurn();
    }

    public override void EntityEndTurn()
    {
        base.EntityEndTurn();
    }
}
