using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SwapInput : MonoBehaviour
{
    private enum InputState
    {
        Keyboard,
        Controller
    }

    private InputState _state;
    [SerializeField] Text _buttonText;
    [SerializeField] Player _player;

    private KeyboardMoveInput _keyBoardMoveInput;
    private KeyboardInteractInput _keyBoardInteractInput;
    private ControllerMoveInput _controllerMoveInput;
    private ControllerInteractInput _controllerInteractInput;
    void Start()
    {
        _keyBoardMoveInput = GetComponent<KeyboardMoveInput>();
        _keyBoardInteractInput = GetComponent<KeyboardInteractInput>();
        _controllerMoveInput = GetComponent<ControllerMoveInput>();
        _controllerInteractInput = GetComponent<ControllerInteractInput>();
        _state = InputState.Keyboard;
    }

    void Update()
    {

    }

    public void ChangeInput()
    {
        if (GameplayManager.Instance.State == GameplayManager.GameState.Overworld && _state == InputState.Keyboard)
        {
            IPlayerInteractInput input = _player.GetComponent<IPlayerInteractInput>();
            Destroy((MonoBehaviour)input);
            _player.AddComponent<ControllerMoveInput>();
        }
        else if (GameplayManager.Instance.State == GameplayManager.GameState.Overworld && _state == InputState.Controller)
        {
            IPlayerInteractInput input = _player.GetComponent<IPlayerInteractInput>();
            Destroy((MonoBehaviour)input);
            _player.AddComponent<KeyboardMoveInput>();
        }
        //else if (GameplayManager.Instance.State == GameplayManager.GameState.Battle && _state == InputState.Keyboard)
        //{
        //    List<PlayerBattle> battlePlayers = new List<PlayerBattle>();
        //    battlePlayers = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.InstanceID).OfType<PlayerBattle>().ToList();
        //    foreach (PlayerBattle player in battlePlayers)
        //    {
        //        IPlayerInteractInput input = player.GetComponent<IPlayerInteractInput>();
        //        Destroy((MonoBehaviour)input);
        //        player.AddComponent<ControllerInteractInput>();
        //    }
        //    print("swap input controller");
        //    _state = InputState.Controller;
        //    _buttonText.text = "Controller Input";
        //    GameplayManager.Instance.SwapInput();
        //}
        //else if (GameplayManager.Instance.State == GameplayManager.GameState.Battle && _state == InputState.Controller)
        //{
        //    List<PlayerBattle> battlePlayers = new List<PlayerBattle>();
        //    battlePlayers = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.InstanceID).OfType<PlayerBattle>().ToList();
        //    foreach (PlayerBattle player in battlePlayers)
        //    {
        //        IPlayerInteractInput input = player.GetComponent<IPlayerInteractInput>();
        //        Destroy((MonoBehaviour)input);
        //        player.AddComponent<KeyboardInteractInput>();
        //    }
        //    print("swap input keyboard");
        //    _state = InputState.Keyboard;
        //    _buttonText.text = "Keyboard Input";
        //    GameplayManager.Instance.SwapInput();
        //}
    }

}
