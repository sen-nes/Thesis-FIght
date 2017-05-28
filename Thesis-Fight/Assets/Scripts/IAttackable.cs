public interface IAttackable
{
    int AttackPriority { get; set; }
    float CurrentHealth { get; set; }

    void TakeDamage(float amount);
}

public enum Priority
{
    CASTLE = 0,
    BUILDING,
    UNIT
}