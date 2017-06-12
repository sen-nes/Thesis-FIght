using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindTarget : MonoBehaviour {

    public float searchRadius;
    // GameObject or just Attackable
    public GameObject target;

    private int combatMask;
    // Field to denote team affiliation

    // Use OveplapSphereNonAlloc
    private Transform units;
    private Transform buildings;

    private void Awake()
    {
        combatMask = LayerMask.GetMask("Combat");
        units = GameObject.Find("Units").transform;
        buildings = GameObject.Find("Buildings").transform;
    }

    private void Update()
    {
        // Check in every tenth frame and start checking every frame if you detect a unit or sth
        if (target == null)
        {
            FindClosestTarget();
        }
    }

    private void FindClosestTarget()
    {
        List<Transform> enemies = SearchForEnemies();

        if (enemies.Count > 0)
        {
            float distanceClosest = Mathf.Infinity;
            Transform closestEnemy = enemies[0];
            foreach (Transform enemy in enemies)
            {
                float distanceToEnemy = (enemy.transform.position - transform.position).sqrMagnitude;

                if (distanceToEnemy < distanceClosest)
                {
                    // Prioritize
                    distanceClosest = distanceToEnemy;
                    closestEnemy = enemy;
                }
            }

            target = closestEnemy.gameObject;
        }
    }

    private List<Transform> SearchForEnemies()
    {
        List<Transform> enemies = new List<Transform>();
        Collider[] surroundingUnits = Physics.OverlapSphere(transform.position, searchRadius, combatMask);

        foreach(Collider unit in surroundingUnits)
        {
            // Cache team ID            
            if (unit.GetComponent<Attackable>().teamID != GetComponent<Attackable>().teamID)
            {
                enemies.Add(unit.transform);
            }
        }

        return enemies;
    }
}
