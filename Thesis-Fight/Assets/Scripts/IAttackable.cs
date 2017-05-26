public interface IAttackable
{
    int AttackPriority { get; set; }
    int CurrentHealth { get; set; }

    void TakeDamage(int amount);
}

public enum Priority
{
    CASTLE = 0,
    BUILDING,
    UNIT
}