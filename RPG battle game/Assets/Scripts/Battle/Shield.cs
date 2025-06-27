using UnityEngine;

public class Shield : MonoBehaviour
{
    private int _shieldAmount = 1;
    [SerializeField] private GameObject _shieldCircle;
    private GameObject _shieldObject;
    private BattleEntity _entity;
    public int ShieldAmount { get { return _shieldAmount; } }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _entity = GetComponent<BattleEntity>();
        _shieldObject = Instantiate(_shieldCircle, this.transform);
    }

    public void ShieldHit()
    {
        _shieldAmount--;
        CheckShieldAmount();
    }

    public void CheckShieldAmount()
    {
        if(_shieldAmount <= 0)
        {
            Destroy(_shieldObject.gameObject);
            Destroy(_entity.GetComponent<Shield>());
        }
    }
}
