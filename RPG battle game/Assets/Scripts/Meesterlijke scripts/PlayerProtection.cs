using UnityEngine;

public class PlayerProtection : MonoBehaviour
{
    [SerializeField] private Collider2D _collider;
    [SerializeField] private float _protectedTime = 3f;
    private float _protectionTimer;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private Material _flickerEffect;
    private bool _hasProtection = false;

    public bool HasProtection { get { return _hasProtection; } set { _hasProtection = value; } }

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _protectionTimer = _protectedTime;
    }

    public void CheckForProtection()
    {
        if (_hasProtection == true)
        {
            _collider.enabled = false;
            _spriteRenderer.material = _flickerEffect;
            _protectionTimer -= Time.deltaTime;
            if (_protectionTimer <= 0)
            {
                _collider.enabled = true;
                _spriteRenderer.material = _defaultMaterial;
                _hasProtection = false;
            }
        }
    }
}
