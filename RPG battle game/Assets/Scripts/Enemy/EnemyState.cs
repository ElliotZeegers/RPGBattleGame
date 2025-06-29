using UnityEngine;

public class EnemyState : MonoBehaviour
{
    //Mogelijke states voor de enemy
    private enum States
    {
        Idle,
        Patrolling,
        Chasing
    }

    private IMoveable _moveable;
    private ChaseState _chaseState;
    private PatrolState _patrolState;

    //Huidige staat van de enemy
    [SerializeField] private States _currentState;
    private Vector2 _chasePos;

    void Start()
    {
        _moveable = GetComponent<IMoveable>();
        _chaseState = GetComponent<ChaseState>();
        _patrolState = GetComponent<PatrolState>();
    }

    void FixedUpdate()
    {
        switch (_currentState)
        {
            case States.Idle:
                break;
            case States.Patrolling:
                //Loopt rond tussen locaties
                _patrolState.PatrolLogic(_moveable);
                break;
            case States.Chasing:
                //Ga achter speler aan
                _chaseState.Chase(_moveable, _chasePos);
                break;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Als het een speler is ga achter hem aan
        GameObject hitObject = collision.gameObject;
        if (hitObject.GetComponent<Player>() != null)
        {
            _chasePos = hitObject.transform.position;
            _currentState = States.Chasing;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Als de speler de trigger verlaat dan checkt het hier of hij een patrolState heeft als dat zo is gaat hij rond lopen tussen locaties en anders blijft hij stil staan
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
