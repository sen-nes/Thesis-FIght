public interface ICombat
{
    float CurrentHealth { get; set; }
    int AttackPriority { get; set; }
    int KillValue { get; set; }

    bool TakeDamage(float amount);
}

public enum Priority
{
    CASTLE = 0,
    BUILDING,
    UNIT
}