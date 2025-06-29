using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IPausable
{
    private IPlayerMoveInput _input;
    private IMoveable _moveable;
    [SerializeField] private float _moveSpeed = 10f;

    private void Awake()
    {
        _input = GetComponent<IPlayerMoveInput>();
        _moveable = GetComponent<IMoveable>();
    }

    void FixedUpdate()
    {
        _moveable.Move(_input.GetMovementInput(), _moveSpeed);
    }

    public void Pause(bool p)
    {
        print(p);
        this.gameObject.SetActive(p);
    }

    public void ChangeInput()
    {
        _input = GetComponent<IPlayerMoveInput>();
    }
}
