using System.Collections;
using UnityEngine;

public class BattleEntity : MonoBehaviour
{
    [SerializeField] protected Material _defaultMaterial;
    [SerializeField] private Material _activeTurnMaterial;

    private Health _health;
    private Shield _shield;
    protected SpriteRenderer _entityRenderer;
    protected TargetEntity _targetingScript;
    protected IDamageable _damageable;

    public SpriteRenderer EntityRenderer { get { return _entityRenderer; } set { _entityRenderer = value; } }

    [SerializeField] protected string _name;
    public string Name { get { return _name; } set { _name = value; } }

    [SerializeField] protected int _battlePriority;
    public int BattlePriority { get { return _battlePriority; } }

    [SerializeField] protected float _hp;
    public float Hp { get { return _hp; } set { _hp = value; } }

    [SerializeField] protected float _defense;
    public float Defense { get { return _defense; } set { _defense = value; } }

    [SerializeField] protected float _damage;
    public float Damage { get { return _damage; } set { _damage = value; } }

    [SerializeField] protected float _speed;
    public float Speed { get { return _speed; } set { _speed = value; } }

    protected bool _isMyTurn;
    public bool IsMyTurn { get { return _isMyTurn; } set { _isMyTurn = value; } }

    protected virtual void Awake()
    {
        _entityRenderer = GetComponent<SpriteRenderer>();
        _shield = GetComponent<Shield>();
        _health = GetComponent<Health>();
        _targetingScript = GetComponent<TargetEntity>();
        _damageable = GetComponent<IDamageable>();
        
    }
    protected virtual void Start()
    {

    }

    void Update()
    {

    }

    public virtual void TurnBehaviour()
    {
        _isMyTurn = true;
        _entityRenderer.material = _activeTurnMaterial;
        print(_name + " ik ben aan de beurt");
    }

    public virtual void Takedamage(float pBaseDamage, float pMultiplier, float pMoveMultiplier)
    {
        if (_shield != null)
        {
            _shield.ShieldHit();
        }
        else 
        {
            _entityRenderer.material = _defaultMaterial;
            float damageTaken = _damageable.ClaculateDamage(pBaseDamage, pMultiplier, pMoveMultiplier, _defense);
            _hp -= damageTaken;
            _health.UpdateHealthImage(damageTaken);
        }
    }

    public virtual void EntityEndTurn()
    {
        _entityRenderer.material = _defaultMaterial;
    }
}
