using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimationHandler : MonoBehaviour {

    private Attack attack;

    private void Start()
    {
        Debug.Log(name);
        attack = transform.parent.GetComponent<Attack>();
    }

    public void Attack()
    {
        attack.PerformAttack();
    }
}
