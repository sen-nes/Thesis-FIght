using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    public Vector3 EnemyCastle;

    private SteeringManager steeringManager;

    private Animator anim;
    // Unit stats for move speed

    private void Awake()
    {
        steeringManager = GetComponent<SteeringManager>();
        anim = transform.Find("Model").GetComponent<Animator>();
    }

    private void Start()
    {
        // PathRequestManager.RequestPath(new PathRequest(transform.position, EnemyCastle, OnPathFound));
    }
}