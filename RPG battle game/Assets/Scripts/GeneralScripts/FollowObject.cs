using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField] private GameObject _followObject;

    private void Update()
    {
        transform.position = new Vector3(_followObject.transform.position.x, _followObject.transform.position.y, this.transform.position.z);
    }
}
