using UnityEngine;

public class Movement : MonoBehaviour, IMoveable
{
    Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 pMoveInput, float pSpeed)
    {
        _rb.AddForce(pMoveInput * pSpeed, ForceMode2D.Impulse);
        //_rb.linearVelocity += pMoveInput * pSpeed;
    }
}
