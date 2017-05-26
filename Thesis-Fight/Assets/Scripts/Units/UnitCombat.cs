using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCombat : MonoBehaviour, IAttackable
{
    public bool showSearchRadius;
    public float searchRadius = 10.0f;
    [HideInInspector]
    public Transform target;
    public int currentHealth;

    // Attackable
    public int CurrentHealth { get { return currentHealth; } set { currentHealth = value; } }
    public int AttackPriority { get; set; }

    private int combatMask;
    private UnitStats unitStats;
    private Details unitDetails;
    private IAttackable targetAttackable;

    // Consider assigning half attack speed initial value
    private float lastAttack;

    private void Start()
    {
        combatMask = LayerMask.GetMask("Combat");
        unitStats = GetComponent<UnitStats>();
        unitDetails = GetComponent<Details>();
        lastAttack = 0.0f;
        CurrentHealth = unitStats.Health.FinalValue;
        AttackPriority = (int)Priority.UNIT;
    }

    private void Update()
    {
        PerformCombatAction();
    }

    private void PerformCombatAction()
    {
        if (target)
        {
            Attack();
        }
        else
        {
            List<Transform> enemies = SearchForEnemies();
            if (enemies.Count > 0)
            {
                FindCosestTarget(enemies);
            }
        }
    }

    private void Attack()
    {
        float distanceToTarget = (target.position - transform.position).magnitude * 100;

        if (distanceToTarget <= unitStats.Range.FinalValue)
        {
            int attackSpeed = unitStats.AttackSpeed.FinalValue;
            lastAttack += Time.deltaTime * 1000;

            // Consider returning wheather target is dead in TakeDamage()
            if (targetAttackable.CurrentHealth > 0)
            {
                if (lastAttack >= attackSpeed)
                {
                    int attackDamage = unitStats.AttackDamage.FinalValue;

                    targetAttackable.TakeDamage(attackDamage);
                    lastAttack = 0.0f;
                }
            }
            else
            {
                targetAttackable = null;
                target = null;
            }
        }
    }

    private List<Transform> SearchForEnemies()
    {
        Collider[] surroundingUnits = Physics.OverlapSphere(transform.position, searchRadius, combatMask);
        List<Transform> enemies = new List<Transform>();

        foreach (Collider unit in surroundingUnits)
        {
            Details details = unit.GetComponent<Details>();
            if (details.teamID != unitDetails.teamID)
            {
                enemies.Add(unit.transform);
            }
        }

        return enemies;
    }

    private void FindCosestTarget(List<Transform> enemies)
    {
        float distanceClosest = Mathf.Infinity;

        foreach (Transform enemy in enemies)
        {
            float distanceToEnemy = (enemy.transform.position - transform.position).magnitude;

            if (distanceToEnemy < distanceClosest)
            {
                // Prioritize
                distanceClosest = distanceToEnemy;
                target = enemy;
            }
        }

        targetAttackable = target.GetComponent<IAttackable>();
    }

    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Clean up after unit

        Destroy(gameObject);
    }


    private void OnDrawGizmos()
    {
        if (showSearchRadius)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, searchRadius);
        }
    }
}
