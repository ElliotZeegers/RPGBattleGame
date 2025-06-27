using System.Collections.Generic;
using UnityEngine;

public class PatrolState : MonoBehaviour
{
    [SerializeField] List<Transform> _checkPointsTransform = new List<Transform>();
    List<Vector2> _checkPoints = new List<Vector2>();
    int _pointNumber = 0;

    void Start()
    {
        foreach (Transform t in _checkPointsTransform)
        {
            _checkPoints.Add(new Vector2(t.transform.position.x, t.transform.position.y));
        }
    }

    public void PatrolLogic(IMoveable pMoveable)
    {
        GoToPoint(pMoveable);
        ChoosePoint();
    }

    public void ChoosePoint()
    {
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
        Vector2 difference = _checkPoints[_pointNumber] - (Vector2)transform.position;
        difference.Normalize();
        Vector2 direction = difference;
        pMoveable.Move(difference, 8f);
    }
}
