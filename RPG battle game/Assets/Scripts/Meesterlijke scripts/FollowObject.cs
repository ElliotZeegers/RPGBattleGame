using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField] private GameObject _followObject;
    private Vector2 _offset;

    void Start()
    {
        _offset = new Vector2(1, 1.5f);
    }

    void Update()
    {
        Vector2 followObjectPos = _followObject.transform.position;
        followObjectPos += _offset;
        this.transform.position = followObjectPos;
    }
}
