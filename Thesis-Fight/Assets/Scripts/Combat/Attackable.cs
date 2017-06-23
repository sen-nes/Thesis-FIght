using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(IBaseStats))]
public class Attackable : MonoBehaviour
{
    public float CurrentHealth { get; private set; }
    public float HealthPercentage
    {
        get
        {
            return CurrentHealth / stats.Health.FinalValue;
        }
    }

    public Image healthbar;

    public Priorities AttackPriority { get; set; }
    public int KillValue { get; set; }

    public Teams teamID;

    private IBaseStats stats;

    private void Start()
    {
        stats = GetComponent<IBaseStats>();

        CurrentHealth = stats.Health.FinalValue;
    }

    public int TakeDamage(DamageTypes damageType, float amount)
    {
        // Perform type calculations
        amount *=  ArmorTable.GetArmorModifier(damageType, stats.ArmorType);
        float reduction = ArmorTable.GetDamageReduction(stats.Armor.FinalValue);

        CurrentHealth -= Mathf.RoundToInt(amount * reduction);
        healthbar.fillAmount = HealthPercentage;

        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            Die();

            return KillValue;
        }

        return 0;
    }

    public void Die()
    {
        // Should notify selection manager in case of death?

        // Move castle notification in CastleController component on* event?
        if (CompareTag("Castle"))
        {
            // Feed proper teamID
            VictoryManager.instance.DeclareDefeat((int)teamID);
        }

        if (CompareTag("Building"))
        {
            Grid.instance.UpdateGridRegion((int)transform.Find("Model").GetComponent<Collider>().bounds.size.x + 1, (int)transform.Find("Model").GetComponent<Collider>().bounds.size.z + 1, transform.position);
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