using UnityEngine;

public class FollowEntity : MonoBehaviour
{
    //Het object te volgen
    [SerializeField] private GameObject _followObject;
    private Vector2 _offset;


    void Start()
    {
        _followObject = GetComponentInParent<BattleEntity>().gameObject;
        _offset = new Vector2(0, -1.5f);
    }

    void Update()
    {
        //Zorgt dat het object _followObject volgt met de juiste offset
        Vector2 entityPos = _followObject.transform.position;
        entityPos += _offset;
        transform.position = entityPos;
    }
}
