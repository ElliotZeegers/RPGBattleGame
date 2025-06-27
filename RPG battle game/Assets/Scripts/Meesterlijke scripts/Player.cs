using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IPausable
{
    private IPlayerMoveInput _input;
    private IMoveable _moveable;

    private void Awake()
    {
        _input = GetComponent<IPlayerMoveInput>();
        _moveable = GetComponent<IMoveable>();
    }

    void Start()
    {
        //GameplayManager.Instance.OnSwapInput += ChangeInput;
    }

    void FixedUpdate()
    {
        _moveable.Move(_input.GetMovementInput(), 10f);
    }

    public void Pause(bool p)
    {
        this.gameObject.SetActive(p);
    }

    //public void ChangeInput()
    //{
    //    _input = GetComponent<IPlayerMoveInput>();
    //}
}
