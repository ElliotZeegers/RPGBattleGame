using UnityEngine;

public class ChaseState : MonoBehaviour
{
    public void Chase(IMoveable pMoveable, Vector2 pChasePos)
    {
        Vector2 difference = pChasePos - (Vector2)transform.position;
        difference.Normalize();
        Vector2 direction = difference;
        pMoveable.Move(difference, 8f);

    }
}
