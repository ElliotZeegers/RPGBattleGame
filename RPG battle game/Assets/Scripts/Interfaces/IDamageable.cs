public interface IDamageable
{
    public float CalculateDamage(float pBaseDamage);

    public float CalculateDamage(float pBaseDamage, float pMultiplier, float pMoveMultiplier, float pDefense);
}
