using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimationHelper : MonoBehaviour {
    private UnitCombat unitCombat;

    private void Start()
    {
        unitCombat = transform.parent.GetComponent<UnitCombat>();
    }

    public void OnAttack()
    {
        Debug.Log("Attack!");
        //unitCombat.Attack();
    }
}
