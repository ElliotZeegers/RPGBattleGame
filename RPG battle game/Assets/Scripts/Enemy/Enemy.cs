using UnityEngine;

public class Enemy : MonoBehaviour, IPausable
{
    //Enemy en Player group voor voor het starten van een battle
    [SerializeField] private GameObject _playerGroup;
    [SerializeField] private GameObject _enemyGroup;
    //PlayerProtection zorgt ervoor dat de enemies de speler niet kunnen raken direct na een battle
    private PlayerProtection _protection;
    void Start()
    {
        _protection = GetComponent<PlayerProtection>();
    }

    void Update()
    {
        //Checkt of hij protection heeft
        _protection.CheckForProtection();
    }

    public void Pause(bool p)
    {
        //Zorgt ervoor dat als het spel "geunpaused" wordt dat HasProtection dan op true komt te staan
        _protection.HasProtection = p;

    }

    //Check voor of de speler de trigger heeft geraakt
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            //Start een battle
            GameplayManager.Instance.Pause(_playerGroup, _enemyGroup, this.transform.root.gameObject);
        }
    }
}