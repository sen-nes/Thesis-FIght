using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitCombat : MonoBehaviour, IAttackable
{
    public bool showSearchRadius;
    public float searchRadius = 10.0f;
    //[HideInInspector]
    public Transform target;
    public float currentHealth;

    public Image healthBar;
    private Animator anim;

    // Attackable
    public float CurrentHealth { get { return currentHealth; } set { currentHealth = value; } }
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

        anim = transform.Find("Model").GetComponent<Animator>();
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
        // Closest, consider making it a field
        Vector3 closestPoint = GetClosestPoint();
        float distanceToTarget = (closestPoint - transform.position).magnitude * 100;
        //float centerToBounds = target.transform.g
        if (distanceToTarget <= unitStats.Range.FinalValue)
        {
            
            //float attackSpeed = unitStats.AttackSpeed.FinalValue;
            //lastAttack += Time.deltaTime * 1000;

            //// Consider returning wheather target is dead in TakeDamage()

            //if (targetAttackable.CurrentHealth > 0)
            //{
            //    if (lastAttack >= attackSpeed)
            //    {
            //        float attackDamage = unitStats.AttackDamage.FinalValue;

            //        //anim.SetTrigger("Attack");
            //        
            //        targetAttackable.TakeDamage(attackDamage);
            //        lastAttack = 0.0f;
            //    }
            //}
            //else
            //{
            //    targetAttackable = null;
            //    target = null;
            //}
        }
    }

    public void OnAttack()
    {
        if (targetAttackable.CurrentHealth > 0)
        {
            float attackDamage = unitStats.AttackDamage.FinalValue;
            targetAttackable.TakeDamage(attackDamage);
        }
        else
        {
            targetAttackable = null;
            target = null;
            anim.SetBool("Running", true);
            anim.SetFloat("Attack Speed", 0);
            
        }
    }

    private List<Transform> SearchForEnemies()
    {
        Collider[] surroundingUnits = Physics.OverlapSphere(transform.position, searchRadius, combatMask);
        List<Transform> enemies = new List<Transform>();

        foreach (Collider unit in surroundingUnits)
        {
            Details details = unit.transform.GetComponent<Details>();
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

    public Vector3 GetClosestPoint()
    {
        Vector3 closestPoint = target.GetComponent<Collider>().bounds.ClosestPoint(transform.position);
        Vector3 selfExtents = transform.GetComponent<Collider>().bounds.extents;
        Vector3 fwd = transform.forward;

        fwd.Scale(selfExtents);
        closestPoint += -fwd;

        return closestPoint;
    }

    public void TakeDamage(float amount)
    {
        CurrentHealth -= amount;
        healthBar.fillAmount = currentHealth / unitStats.Health.FinalValue;

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        ISelectable selectable = transform.Find("Selectable").GetComponent<ISelectable>();
        // Clean up after unit
        if (selectable.Selected)
        {
            SelectionManager.instance.OnDeath(selectable);
        }

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