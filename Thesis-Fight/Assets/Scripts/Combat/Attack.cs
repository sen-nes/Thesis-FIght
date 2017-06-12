using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FindTarget))]
public class Attack : MonoBehaviour {

    public bool stationary;

    private Rigidbody rb;
    private SteeringManager steeringManager;
    private Avoidance avoidance;
    private Pursue pursue;
    private FindTarget findTarget;
    private FighterStats fighterStats;
    private Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        steeringManager = GetComponent<SteeringManager>();
        avoidance = GetComponent<Avoidance>();
        pursue = GetComponent<Pursue>();
        findTarget = GetComponent<FindTarget>();
        fighterStats = GetComponent<FighterStats>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (findTarget.target != null)
        {
            float range = fighterStats.Range.FinalValue / 100;
            float distance = (transform.position - findTarget.target.transform.position).magnitude;
            distance -= SteeringManager.GetBoundingRadius(transform);
            distance -= SteeringManager.GetBoundingRadius(findTarget.target.transform);

            if (distance <= range)
            {
                if (!stationary)
                {
                    anim.SetBool("Running", false);
                    rb.velocity = Vector3.zero;
                }

                // Or at least set the lookAt direction before starting the attack
                transform.LookAt(findTarget.target.transform.position);

                // Start attack animation
                anim.SetFloat("Attack Speed", fighterStats.AttackSpeed.FinalValue);
            }
            else
            {
                if (!stationary)
                {
                    anim.SetBool("Running", true);
                    Vector3 accel = avoidance.Avoid();
                    if (accel.magnitude < 0.005f)
                    {
                        accel = pursue.PursueTarget(findTarget.target.GetComponent<Rigidbody>());
                    }

                    steeringManager.Steer(accel);
                    steeringManager.FaceMovementDirection();
                }

                // Stop attack animation
                anim.SetFloat("Attack Speed", 0.0f);
            }
        }
        else
        {
            // Check if you were attacking before so you don't repeat animation setting
            // read about performance implications
            anim.SetBool("Running", true);
            anim.SetFloat("Attack Speed", 0.0f);
        }

        // Anything that needs to be done in case of no target
    }

    public void PerformAttack()
    {
        // Cache references
        Attackable attackable = findTarget.target.GetComponent<Attackable>();
        float damage = fighterStats.AttackDamage.FinalValue;

        // Damage/Armor types
        int plunder = attackable.TakeDamage(damage);
        if (plunder > 0)
        {
            // Cache playerID
            GoldManager.instance.AddGold(GetComponent<UnitController>().playerID, plunder);
            findTarget.target = null;
        }
    }
}
