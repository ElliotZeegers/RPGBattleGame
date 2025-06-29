using UnityEngine;

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
        //Vraag de beweging input op en geef mee aan _moveable.Move()
        _moveable.Move(_input.GetMovementInput(), _moveSpeed);
    }

    //Zet het object aan of uit op basis van p
    public void Pause(bool p)
    {
        this.gameObject.SetActive(p);
    }

    //Update de input component, wordt gebruikt voor als er een andere input component op komt
    public void ChangeInput()
    {
        _input = GetComponent<IPlayerMoveInput>();
    }
}
