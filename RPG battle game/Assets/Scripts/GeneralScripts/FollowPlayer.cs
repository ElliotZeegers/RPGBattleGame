using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    //De player die gevolgd moet worden
    [SerializeField] private GameObject _player;

    private void Update()
    {
        //Verander huidige positie naar die van _player, maar niet op de Z as
        transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, this.transform.position.z);
    }
}
