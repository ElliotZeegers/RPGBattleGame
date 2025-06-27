using UnityEngine;

public class OptionBlock : MonoBehaviour
{
    public enum BlockTypes
    {
        Run,
        Attack,
        Skip
    }

    [SerializeField] private int _priority;
    [SerializeField] private BlockTypes _blockType;
    private PlayerBattle _player;
    public int Priority { get { return _priority; } }
    private void Awake()
    {
        _player = GetComponentInParent<PlayerBattle>();
    }

    public void Activate()
    {
        switch (_blockType)
        {
            case BlockTypes.Run:
                int runChance = Random.Range(0, 5);
                if (runChance == 0)
                {
                    BattleManager.Instance.StartCoroutine(BattleManager.Instance.EndBattle("You escaped"));
                    GameplayManager.Instance.EndBattle();
                }
                else
                {
                    BattleManager.Instance.EndTurn();
                }
                break;
            case BlockTypes.Attack:
                _player.TurnState = PlayerBattle.PlayerTurnState.IsSelectingTarget;
                break;
            case BlockTypes.Skip:
                BattleManager.Instance.EndTurn();
                break;
        }
    }
}
