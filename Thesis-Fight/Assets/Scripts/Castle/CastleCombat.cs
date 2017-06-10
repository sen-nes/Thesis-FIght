using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastleCombat : MonoBehaviour {

    //public float currentHealth;
    //public Image healthBar;

    //// Attackable
    //public float CurrentHealth { get { return currentHealth; } set { currentHealth = value; } }
    //public int AttackPriority { get; set; }

    //private CastleStats castleStats;

    //private void Start()
    //{
    //    castleStats = GetComponent<CastleStats>();
    //    CurrentHealth = castleStats.Health.FinalValue;
    //    AttackPriority = (int)Priority.CASTLE;
    //}

    //public bool TakeDamage(float amount)
    //{
    //    CurrentHealth -= amount;
    //    healthBar.fillAmount = currentHealth / castleStats.Health.FinalValue;

    //    if (CurrentHealth <= 0)
    //    {
    //        // currentHealth = 1;
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

    //    // Do victory stuff
    //    VictoryManager.instance.DeclareDefeat(GetComponent<IDetails>().TeamID);
    //    Destroy(gameObject);
    //}
}
