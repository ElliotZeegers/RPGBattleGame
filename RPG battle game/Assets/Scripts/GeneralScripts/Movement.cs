using UnityEngine;

public class Movement : MonoBehaviour, IMoveable
{
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 pMoveInput, float pSpeed)
    {
        //Verplaatst het object op basis van moveInput en pSpeed
        _rb.AddForce(pMoveInput * pSpeed, ForceMode2D.Impulse);
    }
}
