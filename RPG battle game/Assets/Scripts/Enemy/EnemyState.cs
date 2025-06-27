using UnityEngine;

public class EnemyState : MonoBehaviour
{
    private enum States
    {
        Idle,
        Patrolling,
        Chasing
    }

    private IMoveable _moveable;
    private ChaseState _chaseState;
    private PatrolState _patrolState;

    [SerializeField] private States _currentState;
    private Vector2 _chasePos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _moveable = GetComponent<IMoveable>();
        _chaseState = GetComponent<ChaseState>();
        _patrolState = GetComponent<PatrolState>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (_currentState)
        {
            case States.Idle:
                break;
            case States.Patrolling:
                _patrolState.PatrolLogic(_moveable);
                break;
            case States.Chasing:
                _chaseState.Chase(_moveable, _chasePos);
                break;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject hitObject = collision.gameObject;
        if (hitObject.GetComponent<Player>() != null)
        {
            _chasePos = hitObject.transform.position;
            _currentState = States.Chasing;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject hitObject = collision.gameObject;
        if (hitObject.GetComponent<Player>() != null)
        {
            if (_patrolState == null)
            {
                _currentState = States.Idle;
            }
            else
            {
                _currentState = States.Patrolling;
            }
        }
    }
}
