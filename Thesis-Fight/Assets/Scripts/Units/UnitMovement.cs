using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour, IBoid
{
    public bool displayPath;

    // Boid
    public Vector3 Velocity { get; set; }
    public float Speed { get; set; }
    public float Mass { get; set; }
    public float SlowDownRadius { get; set; }
    public float Force { get; set; }
    public Stack<Vector3> Path { get; set; }
    public Vector3 Position
    {
        get
        {
            return transform.position;
        }

        set
        {
            transform.position = value;
        }
    }

    private SteeringManager steer;

    // Targets
    private Transform enemyCastle;
    private UnitCombat unitCombat;
    private UnitStats unitStats;

    // Unit stats for move speed

    private void Start()
    {
        // find enemy castle in method
        GameObject[] castles = GameObject.FindGameObjectsWithTag("Castle");

        foreach (GameObject castle in castles)
        {
            if (castle.GetComponent<Details>().teamID != GetComponent<Details>().teamID)
            {
                enemyCastle = castle.transform;
            }
        }
        Vector3 attackCastleSide = (enemyCastle.position - transform.position).normalized * 5;
        PathRequestManager.RequestPath(new PathRequest(transform.position, enemyCastle.position - attackCastleSide, OnPathFound));

        steer = new SteeringManager(this);
        unitStats = GetComponent<UnitStats>();
        unitCombat = GetComponent<UnitCombat>();

        Speed = 10.0f;
        Mass = 10.0f;
        SlowDownRadius = 10.0f;
        Force = 5.0f;
    }

    public void OnPathFound(Vector3[] path, bool success)
    {
        if (success)
        {
            Array.Reverse(path);
            Path = new Stack<Vector3>(path);
            //StopCoroutine("FollowPath");
            //StartCoroutine("FollowPath");
        }
        else
        {
            Path = null;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (!unitCombat.target)
        {
            if (Path != null)
            {
                transform.LookAt(Path.Peek());
            }

            steer.FollowPath();
        }
        else
        {
            Vector3 closestPoint = unitCombat.GetClosestPoint();
            float range = unitStats.Range.FinalValue / 100.0f;
            float distance = (closestPoint - transform.position).magnitude;

            if (distance > range)
            {
                steer.Arrive(closestPoint);
            }
            else
            {
                Velocity = Vector3.zero;
            }

            transform.LookAt(closestPoint);
        }

        steer.Update();
    }

    private void OnDrawGizmos()
    {
        if (Path != null && displayPath)
        {
            Vector3[] waypoints = Path.ToArray();
            for (int i = 0; i < waypoints.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(waypoints[i], Vector3.one);

                if (i == 0)
                {
                    Gizmos.DrawLine(transform.position, waypoints[i]);
                }
                else
                {
                    Gizmos.DrawLine(waypoints[i - 1], waypoints[i]);
                }
            }
        }
    }
}