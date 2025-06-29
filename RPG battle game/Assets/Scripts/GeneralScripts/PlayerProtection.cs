using UnityEngine;

public class PlayerProtection : MonoBehaviour
{
    //Referentie naar de collider van de enemy die een battle activeert
    [SerializeField] private Collider2D _collider;
    [SerializeField] private float _protectedTime = 3f;
    private float _protectionTimer;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private Material _flickerEffect;
    [SerializeField] private bool _hasProtection = false;

    //Een property met een stukje code ingebouwt als het wordt geset hij kijkt of de waarde true of false is zodat het gelijk word bijgewerkt en niet hoeft te wachten op de volgende update tick dit voorkomt dat het soms bugged en dat je gelijk weer een battle in gaat omdat er een enemy tegen je aan staat.
    public bool HasProtection { get { return _hasProtection; }
        set
        {
            _hasProtection = value;
            if (value)
            {
                _collider.enabled = false;
                _spriteRenderer.material = _flickerEffect;
                _protectionTimer = _protectedTime;
            }
            else
            {
                _collider.enabled = true;
                _spriteRenderer.material = _defaultMaterial;
            }
        }
    }

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _protectionTimer = _protectedTime;
    }

    public void CheckForProtection()
    {
        //Check of bescherming actief is
        if (_hasProtection == true)
        {
            //Zet collider uit en pas flicker effect toe
            _collider.enabled = false;
            _spriteRenderer.material = _flickerEffect;
            //Laat timer naar beneden lopen
            _protectionTimer -= Time.deltaTime;
            //Als de timer 0 is zet de bescherming uit
            if (_protectionTimer <= 0)
            {
                _protectionTimer = _protectedTime;
                _spriteRenderer.material = _defaultMaterial;
                _collider.enabled = true;
                _hasProtection = false;
            }
        }
    }
}
