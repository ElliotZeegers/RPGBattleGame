using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    private void Update()
    {
        transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, this.transform.position.z);
    }
}
