using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastleCombat : MonoBehaviour, IAttackable {

    public float currentHealth;
    public Image healthBar;

    // Attackable
    public float CurrentHealth { get { return currentHealth; } set { currentHealth = value; } }
    public int AttackPriority { get; set; }

    private CastleStats castleStats;

    private void Start()
    {
        castleStats = GetComponent<CastleStats>();
        CurrentHealth = castleStats.Health.FinalValue;
        AttackPriority = (int)Priority.CASTLE;
    }

    public void TakeDamage(float amount)
    {
        CurrentHealth -= amount;
        healthBar.fillAmount = currentHealth / castleStats.Health.FinalValue;

        if (CurrentHealth <= 0)
        {
            // currentHealth = 1;
            Die();
        }
    }

    private void Die()
    {
        // Do victory stuff
        VictoryManager.instance.DeclareDefeat(GetComponent<Details>().teamID);
        Destroy(gameObject);
    }
}
