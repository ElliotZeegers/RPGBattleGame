using UnityEngine;

public class BattleEntity : MonoBehaviour
{
    //materials voor shaders wisselen
    [SerializeField] protected Material _defaultMaterial;
    [SerializeField] private Material _activeTurnMaterial;

    //Algemene scripts die een entity nodig kan hebben
    private Health _health;
    private Shield _shield;
    protected SpriteRenderer _entityRenderer;
    protected TargetEntity _targetingScript;
    protected IDamageable _damageable;

    //Verschillende stats van een enemy
    [SerializeField] protected string _name;
    [SerializeField] protected int _battlePriority;
    [SerializeField] protected float _hp;
    [SerializeField] protected float _defense;
    [SerializeField] protected float _damage;
    [SerializeField] protected float _speed;

    //Properties van de stats zodat scripts die ze nodig hebben het aan kunnen passen of de waarde kunnen bekijken van de stats
    public SpriteRenderer EntityRenderer { get { return _entityRenderer; } set { _entityRenderer = value; } }
    public string Name { get { return _name; } set { _name = value; } }
    public int BattlePriority { get { return _battlePriority; } }
    public float Hp { get { return _hp; } set { _hp = value; } }
    public float Defense { get { return _defense; } set { _defense = value; } }
    public float Damage { get { return _damage; } set { _damage = value; } }
    public float Speed { get { return _speed; } set { _speed = value; } }

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

    //Wordt aan het begin van een beurt aangeroepen
    public virtual void TurnBehaviour()
    {
        //Veranderd de huidige material met _activeTurnMaterial, zodat de speler kan zien we er aan de beurt is
        _entityRenderer.material = _activeTurnMaterial;
    }
    
    public virtual void Takedamage(float pBaseDamage, float pMultiplier, float pMoveMultiplier)
    {
        //Checkt of er een shield is. Zo ja, dan voert hij de code uit en wordt het shield geraakt en verlies je geen HP. Als je geen shield hebt, dan wordt de else uitgevoerd en krijg je damage.
        if (_shield != null)
        {
            _shield.ShieldHit();
        }
        else
        {
            //zet de material terug naar de standaard material
            _entityRenderer.material = _defaultMaterial;
            //berekent de damage
            float damageTaken = _damageable.CalculateDamage(pBaseDamage, pMultiplier, pMoveMultiplier, _defense);
            //haalt de damage van de hp af
            _hp -= damageTaken;
            //update de UI van de hp van de speler
            _health.UpdateHealthImage(damageTaken);
        }
    }
    //Aan het einde van de beurt wordt deze aangeroepen en zet het de material terug naar standaard, zodat je kunt zien dat deze entity niet meer aan de beurt is.
    public virtual void EntityEndTurn()
    {
        _entityRenderer.material = _defaultMaterial;
    }
}
