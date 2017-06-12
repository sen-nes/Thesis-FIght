using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(IBaseStats))]
public class Attackable : MonoBehaviour
{
    public float CurrentHealth { get; set; }
    public Image healthbar;

    public Priorities AttackPriority { get; set; }
    public int KillValue { get; set; }

    public Team teamID;

    private IBaseStats stats;

    private void Start()
    {
        stats = GetComponent<IBaseStats>();

        CurrentHealth = stats.Health.FinalValue;
    }

    public bool TakeDamage(float amount)
    {
        // Perform type calculations

        CurrentHealth -= amount;
        healthbar.fillAmount = CurrentHealth / stats.Health.FinalValue;

        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            Die();

            return true;
        }

        return false;
    }

    public void Die()
    {
        // Should notify selection manager in case of death?

        // Move castle notification in CastleController component on* event?
        if (CompareTag("Castle"))
        {
            // Feed proper teamID
            VictoryManager.instance.DeclareDefeat(0);
        }

        Destroy(gameObject);
    }
}

public enum Priorities
{
    CASTLE = 0,
    BUILDING,
    UNIT
}