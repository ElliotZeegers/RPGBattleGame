using UnityEngine;

public class FollowEntity : MonoBehaviour
{
    [SerializeField] private GameObject _followObject;
    private Vector2 _offset;


    void Start()
    {
        _followObject = GetComponentInParent<BattleEntity>().gameObject;
        _offset = new Vector2(0, -1.5f);
    }

    void Update()
    {
        Vector2 entityPos = _followObject.transform.position;
        entityPos += _offset;
        transform.position = entityPos;
    }
}
