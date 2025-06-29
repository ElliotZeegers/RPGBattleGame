using UnityEngine;

public class DealDamage : MonoBehaviour, IDamageable
{
    //Calculeert de damage
    public float CalculateDamage(float pBaseDamage)
    {
        return Mathf.Clamp(Mathf.FloorToInt(pBaseDamage), 1, Mathf.Infinity);
    }
    //Calculeert de damage
    public float CalculateDamage(float pBaseDamage, float pMultiplier, float pMoveMultiplier, float pDefense)
    {
        //Rekent de damage uit op basis van de damage, multiplier, moveMultiplier en defense
        float totalDamage = pBaseDamage * pMultiplier * pMoveMultiplier / pDefense;
        //Houdt totalDamage tussen 1 en Infinity en rondt het af naar een heel getal naar beneden
        return Mathf.Clamp(Mathf.FloorToInt(totalDamage), 1, Mathf.Infinity);
    }
}