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
    public GameObject killGold;

    public Team teamID;

    private IBaseStats stats;

    private void Start()
    {
        stats = GetComponent<IBaseStats>();

        CurrentHealth = stats.Health.FinalValue;
        Debug.Log("After instantiate: " + KillValue);
    }

    public int TakeDamage(float amount)
    {
        // Perform type calculations

        CurrentHealth -= amount;
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

        // Show gold on death to human player only
        Debug.Log(KillValue);
        killGold.GetComponent<KillGold>().goldValue = KillValue;
        Instantiate(killGold, GetComponent<Collider>().bounds.center, Quaternion.identity);

        Destroy(gameObject);
    }
}

public enum Priorities
{
    CASTLE = 0,
    BUILDING,
    UNIT
}