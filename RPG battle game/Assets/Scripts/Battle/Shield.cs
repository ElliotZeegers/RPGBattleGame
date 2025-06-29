using UnityEngine;

public class Shield : MonoBehaviour
{
    //Aantal keer dat het shield geraakt kan worden
    private int _shieldAmount = 1;
    //prefab van het schild
    [SerializeField] private GameObject _shieldCircle;
    private GameObject _shieldObject;
    //entity waar het schild op zit
    private BattleEntity _entity;

    public int ShieldAmount { get { return _shieldAmount; } }

    void Start()
    {
        _entity = GetComponent<BattleEntity>();
        //Instantiate het schild
        _shieldObject = Instantiate(_shieldCircle, this.transform);
    }

    public void ShieldHit()
    {
        _shieldAmount--;
        //Checkt of het schild nog actief moet blijven
        CheckShieldAmount();
    }

    //Verwijdert het schildobject en dit script als het schild op is
    public void CheckShieldAmount()
    {
        if(_shieldAmount <= 0)
        {
            Destroy(_shieldObject.gameObject);
            Destroy(_entity.GetComponent<Shield>());
        }
    }
}
