using UnityEngine;

public class OptionBlock : MonoBehaviour
{
    //Acties die een OptionBlock kan uitvoeren
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
                //Kans van 1 op 5 om te ontsnappen
                int runChance = Random.Range(0, 5);
                if (runChance == 0)
                {
                    // Start het beëindigen van de battle
                    BattleManager.Instance.StartCoroutine(BattleManager.Instance.EndBattle("Je bent ontsnapt"));
                    //Zet alle BattleEntities uit
                    foreach (BattleEntity battleEntity in BattleManager.Instance.BattleEntities)
                    {
                        battleEntity.enabled = false;
                    }
                    //Meldt aan GameplayManager dat de battle is geëindigd via run
                    GameplayManager.Instance.EndBattleRun();
                    //Zet BattleManager uit
                    BattleManager.Instance.DisableManager();
                }
                else
                {
                    //Als ontsnappen mislukt, beëindig de beurt
                    BattleManager.Instance.EndTurn();
                }
                break;
            case BlockTypes.Attack:
                //Zet de state van de speler zodat hij een target moet kiezen
                _player.TurnState = PlayerBattle.PlayerTurnState.IsSelectingTarget;
                break;
            case BlockTypes.Skip:
                //Beurt overslaan
                BattleManager.Instance.EndTurn();
                break;
        }
    }
}
