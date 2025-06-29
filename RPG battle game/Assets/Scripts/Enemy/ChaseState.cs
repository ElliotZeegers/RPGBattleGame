using UnityEngine;

public class ChaseState : MonoBehaviour
{
    public void Chase(IMoveable pMoveable, Vector2 pChasePos)
    {
        //Bereken het verschil tussen de targetpositie en huidige positie
        Vector2 difference = pChasePos - (Vector2)transform.position;
        //Normaliseer om alleen de richting over te houden
        difference.Normalize();
        Vector2 direction = difference;
        //Beweeg het object in de richting van het doel met snelheid 8
        pMoveable.Move(difference, 8f);

    }
}
