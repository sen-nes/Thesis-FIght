using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitCombat : MonoBehaviour
{
    //public float searchRadius = 10.0f;

    //public Image healthBar;

    //// Attackable
    //public float CurrentHealth { get; set; }
    //public int AttackPriority { get; set; }
    //public int KillValue { get; set; }

    //private int combatMask;

    //private int teamID;
    //private UnitStats unitStats;
    //private Animator anim;

    //// Target
    //public Transform target;
    //public Bounds targetBounds;
    //private ICombat targetAttackable;

    //private void Awake()
    //{
    //    combatMask = LayerMask.GetMask("Combat");
    //    teamID = GetComponent<UnitDetails>().TeamID;
    //    unitStats = GetComponent<UnitStats>();

    //    AttackPriority = (int)Priority.UNIT;

    //    anim = transform.Find("Model").GetComponent<Animator>();
    //}

    //private void Start()
    //{
    //    CurrentHealth = unitStats.Health.FinalValue;
    //}

    //private void Update()
    //{
    //    if (target == null)
    //    {
    //        FindClosestTarget();
    //    }
    //}

    //public void FindClosestTarget()
    //{
    //    List<Transform> enemies = SearchForEnemies();

    //    if (enemies.Count > 0)
    //    {
    //        float distanceClosest = Mathf.Infinity;
    //        foreach (Transform enemy in enemies)
    //        {
    //            float distanceToEnemy = (enemy.transform.position - transform.position).magnitude;

    //            if (distanceToEnemy < distanceClosest)
    //            {
    //                // Prioritize
    //                distanceClosest = distanceToEnemy;
    //                target = enemy;
    //            }
    //        }

    //        targetBounds = target.GetComponent<Collider>().bounds;
    //        targetAttackable = target.GetComponent<ICombat>();
    //    }
    //}

    //private List<Transform> SearchForEnemies()
    //{
    //    Collider[] surroundingUnits = Physics.OverlapSphere(transform.position, searchRadius, combatMask);
    //    List<Transform> enemies = new List<Transform>();

    //    foreach (Collider unit in surroundingUnits)
    //    {
    //        IDetails details = unit.transform.GetComponent<IDetails>();
    //        if (details.TeamID != unitDetails.TeamID)
    //        {
    //            enemies.Add(unit.transform);
    //        }
    //    }

    //    return enemies;
    //}

    //public void Attack()
    //{
    //    float attackDamage = unitStats.AttackDamage.FinalValue;

    //    if (target != null)
    //    {
    //        if (targetAttackable.TakeDamage(attackDamage))
    //        {
    //            GoldManager.instance.AddGold(GetComponent<UnitDetails>().PlayerID, target.GetComponent<IDetails>().goldValue);
    //            target = null;
    //            targetAttackable = null;

    //            anim.SetFloat("Attack Speed", 0);
    //            anim.SetBool("Running", true);
    //        }
    //    }
    //    else
    //    {
    //        anim.SetFloat("Attack Speed", 0);
    //        anim.SetBool("Running", true);
    //    }
    //}

    //public bool TakeDamage(float amount)
    //{
    //    CurrentHealth -= amount;
    //    healthBar.fillAmount = currentHealth / unitStats.Health.FinalValue;

    //    // Update UI

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