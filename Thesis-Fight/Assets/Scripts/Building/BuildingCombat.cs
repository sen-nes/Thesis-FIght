using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingCombat : MonoBehaviour, IAttackable {

    public float currentHealth;
    public Image healthBar;

    // Attackable
    public float CurrentHealth { get { return currentHealth; } set { currentHealth = value; } }
    public int AttackPriority { get; set; }

    private BuildingStats buildingStats;

    private void Start()
    {
        buildingStats = GetComponent<BuildingStats>();
        CurrentHealth = buildingStats.Health.FinalValue;
        AttackPriority = (int)Priority.BUILDING;
    }

    public void TakeDamage(float amount)
    {
        CurrentHealth -= amount;
        healthBar.fillAmount = currentHealth / buildingStats.Health.FinalValue;

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Clean up

        Destroy(gameObject);
    }
}
