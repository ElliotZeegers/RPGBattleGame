using UnityEngine;

public class FollowObject : MonoBehaviour
{
    //Het object om te volgen
    [SerializeField] private GameObject _followObject;

    private void Update()
    {
        //Verplaats positie naar het object om te volgen
        transform.position = new Vector3(_followObject.transform.position.x, _followObject.transform.position.y, this.transform.position.z);
    }
}
