using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingCombat : MonoBehaviour {

    //public float currentHealth;
    //public Image healthBar;

    //// Attackable
    //public float CurrentHealth { get { return currentHealth; } set { currentHealth = value; } }
    //public int AttackPriority { get; set; }
    //public int KillValue { get; set; }

    //private BuildingStats buildingStats;

    //private void Awake()
    //{
    //    buildingStats = GetComponent<BuildingStats>();
    //    AttackPriority = (int)Priority.BUILDING;
    //    KillValue = GetComponent<BuldingDetails>().buildingPrice * 0.1f;
    //}

    //private void Start()
    //{
    //    CurrentHealth = buildingStats.Health.FinalValue;
    //}

    //public bool TakeDamage(float amount)
    //{
    //    CurrentHealth -= amount;
    //    healthBar.fillAmount = currentHealth / buildingStats.Health.FinalValue;

    //    if (CurrentHealth <= 0)
    //    {
    //        Die();
    //        return true;
    //    }

    //    return false;
    //}

    //private void Die()
    //{
    //    ISelectable selectable = transform.Find("Selectable").GetComponent<ISelectable>();
    //    // Clean up after unit
    //    if (selectable.Selected)
    //    {
    //        SelectionManager.instance.OnDeath(selectable);
    //    }

    //    Destroy(gameObject);
    //}
}
