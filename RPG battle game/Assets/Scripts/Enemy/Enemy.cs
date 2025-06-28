using UnityEngine;

public class Enemy : MonoBehaviour, IPausable
{
    [SerializeField] private GameObject _playerGroup;
    [SerializeField] private GameObject _enemyGroup;
    private PlayerProtection _protection;
    void Start()
    {
        _protection = GetComponent<PlayerProtection>();
    }

    void Update()
    {
        _protection.CheckForProtection();
    }

    public void Pause(bool p)
    {
        _protection.HasProtection = p;

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