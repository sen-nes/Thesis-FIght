using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimationHandler : MonoBehaviour {

    private Attack attack;

    private void Start()
    {
        attack = transform.parent.GetComponent<Attack>();
    }

    public void Attack()
    {
        attack.PerformAttack();
    }
}
