using UnityEngine;

public class PlayerBattle : BattleEntity
{
    //States waarin de speler zich tijdens zijn beurt kan bevinden
    public enum PlayerTurnState
    {
        None,
        IsSelectingBlock,
        IsSelectingTarget
    }

    [SerializeField] private PlayerTurnState _turnState;

    private IPlayerInteractInput _playerInput;
    private PlayerChoice _playerChoice;

    //Property voor TurnState zodat andere scripts het aan kunnen passen of de waarde kunnen lezen
    public PlayerTurnState TurnState { get { return _turnState; } set { _turnState = value; } }

    protected override void Awake()
    {
        base.Awake();
        _playerInput = GetComponent<IPlayerInteractInput>();
        _playerChoice = GetComponentInChildren<PlayerChoice>();
        //Zorgt dat de keuze menu en targeting in eerste instantie uit staan
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

    //Wordt aangeroepen als het de beurt is van deze speler
    public override void TurnBehaviour()
    {
        base.TurnBehaviour();
        _turnState = PlayerTurnState.IsSelectingBlock;
    }

    //Handelt gedrag af op basis van de huidige turn state
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

    //Zorgt dat de speler een actie kan kiezen
    public void SelectingBlock()
    {
        _playerChoice.gameObject.SetActive(true);
        
        //Als speler een keuze bevestigt, verberg de blokken (keuze menu) en activeer gekozen block
        if (_playerInput.Confirm())
        {
            _playerChoice.gameObject.SetActive(false);
            _playerChoice.ActivateBlock();
        }
    }

    public void Target()
    {
        _targetingScript.enabled = true;
        //Speler kan een enemy selecteren
        _targetingScript.SelectEnemy(_playerInput);
        //Als de keuze is bevestigt geeft hij de damage aan de gekozen target
        if (_playerInput.Confirm())
        {
            BattleManager.Instance.BattleEnemies[_targetingScript.SelectedTarget].Takedamage(_damage, 1f, 1f);
            BattleManager.Instance.EndTurn();
        }
        //Als de speler op terug drukt ga de speler terug naar het keuze menu
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
    //Zorgt ervoor dat de input opnieuw opgehaald wordt
    public void ChangeInput()
    {
        _playerInput = GetComponent<IPlayerInteractInput>();
        _playerChoice.ChangeInput();
    }
}
