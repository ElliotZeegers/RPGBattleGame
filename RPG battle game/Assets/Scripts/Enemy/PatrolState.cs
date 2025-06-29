using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PatrolState : MonoBehaviour
{
    //Lijst met locaties waar de enemy heen moet lopen
    [SerializeField] private List<Transform> _checkPointsTransform = new List<Transform>();
    private List<Vector2> _checkPoints = new List<Vector2>();
    //Houdt bij op welk punt de enemy nu is
    private int _pointNumber = 0;

    void Start()
    {
        //Zet _checkPointsTransform om naar Vector 2s
        _checkPoints = _checkPointsTransform.Select(t => (Vector2)t.position).ToList();
    }

    public void PatrolLogic(IMoveable pMoveable)
    {
        //Loop naar punt
        GoToPoint(pMoveable);
        ChoosePoint();
    }

    public void ChoosePoint()
    {
        //Kiest volgende positie als het laatse punt is bereikt ga dan weer naar het eerste punt anders ga naar het volgende punt
        if (Mathf.Round(transform.position.x) == Mathf.Round(_checkPoints[_checkPoints.Count - 1].x) && Mathf.Round(transform.position.y) == Mathf.Round(_checkPoints[_checkPoints.Count - 1].y))
        {
            _pointNumber = 0;
        }
        else if (Mathf.Round(transform.position.x) == Mathf.Round(_checkPoints[_pointNumber].x) && Mathf.Round(transform.position.y) == Mathf.Round(_checkPoints[_pointNumber].y))
        {
            _pointNumber++;
        }
    }

    public void GoToPoint(IMoveable pMoveable)
    {
        //Bereken het verschil tussen de targetpositie en huidige positie
        Vector2 difference = _checkPoints[_pointNumber] - (Vector2)transform.position;
        //Normaliseer om alleen de richting over te houden
        difference.Normalize();
        Vector2 direction = difference;
        //Beweeg het object in de richting van het doel met snelheid 8
        pMoveable.Move(difference, 8f);
    }
}
