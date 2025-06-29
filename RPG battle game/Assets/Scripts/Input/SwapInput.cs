using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SwapInput : MonoBehaviour
{
    //Enum om bij te houden welke input actief is
    private enum InputState
    {
        Keyboard,
        Controller
    }

    private InputState _state;
    [SerializeField] private Player _player;
    //Referenties naar input scripts voor keyboard en controller beweging en interactie om te checken
    private KeyboardMoveInput _keyboardMoveInput;
    private ControllerMoveInput _controllerMoveInput;
    private KeyboardInteractInput _keyboardInteractInput;
    private ControllerInteractInput _controllerInteractInput;


    void Start()
    {
        _keyboardMoveInput = GetComponent<KeyboardMoveInput>();
        _controllerMoveInput = GetComponent<ControllerMoveInput>();
        _keyboardInteractInput = GetComponent<KeyboardInteractInput>();
        _controllerInteractInput = GetComponent<ControllerInteractInput>();
        //Start met keyboard input
        _state = InputState.Keyboard;
    }

    void Update()
    {
        ChangeInput();
    }

    //Wissel input afhankelijk van game state en huidig input type
    public void ChangeInput()
    {
        if (GameplayManager.Instance.State == GameplayManager.GameState.Overworld && _state == InputState.Keyboard)
        {
            ChangeToOverworldController();
        }
        else if (GameplayManager.Instance.State == GameplayManager.GameState.Overworld && _state == InputState.Controller)
        {
            ChangeToOverworldKeyboard();
        }
        else if (GameplayManager.Instance.State == GameplayManager.GameState.Battle && _state == InputState.Keyboard)
        {
            ChangeToBattleController();
        }
        else if (GameplayManager.Instance.State == GameplayManager.GameState.Battle && _state == InputState.Controller)
        {
            ChangeToBattleKeyboard();
        }
    }

    //Wissel van keyboard naar controller input in de overworld als controller input gedetecteerd wordt
    private void ChangeToOverworldController()
    {
        if (_controllerMoveInput.GetMovementInput() != new Vector2(0, 0))
        {
            IPlayerMoveInput input = _player.GetComponent<IPlayerMoveInput>();
            Destroy((MonoBehaviour)input);
            _player.gameObject.AddComponent<ControllerMoveInput>();
            StartCoroutine(DelayedInputUpdateOverworld());
            _state = InputState.Controller;
        }

    }

    //Wissel van controller naar keyboard input in de overworld als keyboard input gedetecteerd wordt
    private void ChangeToOverworldKeyboard()
    {
        if (_keyboardMoveInput.GetMovementInput() != new Vector2(0, 0))
        {
            IPlayerMoveInput input = _player.GetComponent<IPlayerMoveInput>();
            Destroy((MonoBehaviour)input);
            _player.gameObject.AddComponent<KeyboardMoveInput>();
            StartCoroutine(DelayedInputUpdateOverworld());
            _state = InputState.Keyboard;
        }
    }

    //Wissel van keyboard naar controller input in de battle als controller interactie gedetecteerd wordt
    private void ChangeToBattleController()
    {
        if (_controllerInteractInput.SelectOption() != 0 || _controllerInteractInput.Confirm() == true || _controllerInteractInput.Return() == true)
        {
            List<PlayerBattle> battlePlayers = new List<PlayerBattle>();
            battlePlayers = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.InstanceID).OfType<PlayerBattle>().ToList();
            foreach (PlayerBattle player in battlePlayers)
            {
                IPlayerInteractInput input = player.GetComponent<IPlayerInteractInput>();
                Destroy((MonoBehaviour)input);
                player.AddComponent<ControllerInteractInput>();
                StartCoroutine(DelayedInputUpdateBattle(player));
            }
            _state = InputState.Controller;
        }
    }

    //Wissel van controller naar keyboard input in de battle als keyboard interactie gedetecteerd wordt
    private void ChangeToBattleKeyboard()
    {
        if (_keyboardInteractInput.SelectOption() != 0 || _keyboardInteractInput.Confirm() == true || _keyboardInteractInput.Return() == true)
        {
            List<PlayerBattle> battlePlayers = new List<PlayerBattle>();
            battlePlayers = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.InstanceID).OfType<PlayerBattle>().ToList();
            foreach (PlayerBattle player in battlePlayers)
            {
                IPlayerInteractInput input = player.GetComponent<IPlayerInteractInput>();
                Destroy((MonoBehaviour)input);
                player.AddComponent<KeyboardInteractInput>();
                StartCoroutine(DelayedInputUpdateBattle(player));
            }
            _state = InputState.Keyboard;
        }
    }

    //Reset de input state naar keyboard bij een gamestate verandering
    public void OnChangeGameState()
    {
        _state = InputState.Keyboard;
    }

    //Hier gebruik ik een coroutine zodat het een frame wacht met zeggen dat er naar het nieuwe input component gezocht moet worden, als het niet 1 frame wacht werkt het niet
    private IEnumerator DelayedInputUpdateOverworld()
    {
        yield return null;
        _player.ChangeInput();
    }

    //Hier gebruik ik een coroutine zodat het een frame wacht met zeggen dat er naar het nieuwe input component gezocht moet worden, als het niet 1 frame wacht werkt het niet
    private IEnumerator DelayedInputUpdateBattle(PlayerBattle pPlayer)
    {
        yield return null;
        pPlayer.ChangeInput();
    }
}
