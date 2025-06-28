using UnityEngine;

public class PlayerBattle : BattleEntity
{
    public enum PlayerTurnState
    {
        None,
        IsSelectingBlock,
        IsSelectingTarget
    }

    [SerializeField] private PlayerTurnState _turnState;

    private IPlayerInteractInput _playerInput;
    private PlayerChoice _playerChoice;

    public PlayerTurnState TurnState { get { return _turnState; } set { _turnState = value; } }

    protected override void Awake()
    {
        base.Awake();
        _playerInput = GetComponent<IPlayerInteractInput>();
        _playerChoice = GetComponentInChildren<PlayerChoice>();
        _playerChoice.gameObject.SetActive(false);
        _targetingScript.enabled = false;
    }

    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        Turn();
    }

    public override void TurnBehaviour()
    {
        base.TurnBehaviour();
        _turnState = PlayerTurnState.IsSelectingBlock;
    }

    public void Turn()
    {
        switch (_turnState)
        {
            case PlayerTurnState.None:
                break;
            case PlayerTurnState.IsSelectingBlock:
                SelectingBlock();
                break;
            case PlayerTurnState.IsSelectingTarget:
                Target();
                break;
        }
    }

    public void SelectingBlock()
    {
        _playerChoice.gameObject.SetActive(true);
        if (_playerInput.Confirm())
        {
            _playerChoice.gameObject.SetActive(false);
            _playerChoice.ActivateBlock();
        }
    }

    public void Target()
    {
        _targetingScript.enabled = true;
        _targetingScript.SelectEnemy(_playerInput);
        if (_playerInput.Confirm())
        {
            BattleManager.Instance.BattleEnemies[_targetingScript.SelectedTarget].Takedamage(_damage, 1f, 1f);
            BattleManager.Instance.EndTurn();
        }
        if (_playerInput.Return())
        {
            _targetingScript.UnTarget();
            _turnState = PlayerTurnState.IsSelectingBlock;
            _targetingScript.enabled = false;
        }
    }

    public override void EntityEndTurn()
    {
        base.EntityEndTurn();
        _turnState = PlayerTurnState.None;
        _targetingScript.enabled = false;
    }

    public void ChangeInput()
    {
        _playerInput = GetComponent<IPlayerInteractInput>();
        _playerChoice.ChangeInput();
    }
}
