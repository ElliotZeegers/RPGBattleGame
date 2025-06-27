using UnityEngine;

public class Enemy : MonoBehaviour, IPausable
{
    private IProtectable _protection;
    [SerializeField] private GameObject _playerGroup;
    [SerializeField] private GameObject _enemyGroup;
    void Start()
    {
    }

    void Update()
    {

    }

    public void Pause(bool p)
    {
        if (p == false)
        {

        }
        //this.gameObject.SetActive(p);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            GameplayManager.Instance.Pause(_playerGroup, _enemyGroup, this.transform.root.gameObject);
        }
    }
}   