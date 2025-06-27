using UnityEngine;

public class DealDamage : MonoBehaviour, IDamageable
{
    public float ClaculateDamage(float pBaseDamage)
    {
        return Mathf.Clamp(Mathf.FloorToInt(pBaseDamage), 1, Mathf.Infinity);
    }

    public float ClaculateDamage(float pBaseDamage, float pMultiplier, float pMoveMultiplier, float pDefense)
    {
        float totalDamage = pBaseDamage * pMultiplier * pMoveMultiplier / pDefense;
        return Mathf.Clamp(Mathf.FloorToInt(totalDamage), 1, Mathf.Infinity);
    }
}