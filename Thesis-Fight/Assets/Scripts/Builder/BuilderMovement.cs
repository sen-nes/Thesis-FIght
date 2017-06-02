using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum MouseButtonDown
{
    MBD_LEFT = 0,
    MBD_RIGHT,
    MBD_MIDDLE,
};

public class BuilderMovement : MonoBehaviour, IBoid
{
    public bool displayPath;
    public bool displayForces;

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
    private ISelectable selectable;

    private void Start()
    {
        steer = new SteeringManager(this);
        selectable = transform.Find("Selectable").GetComponent<ISelectable>();

        Speed = 10.0f;
        Mass = 10.0f;
        SlowDownRadius = 10.0f;
        Force = 15.0f;
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

    private void Update()
    {
        if (selectable.Selected)
        {
            if (Input.GetMouseButton((int)MouseButton.MB_RIGHT))
            {
                Vector3 destination = steer.GetMousePoint(Input.mousePosition);
                // Consider returning a stuct
                if (destination != Vector3.zero)
                {
                    PathRequestManager.RequestPath(new PathRequest(transform.position, destination, OnPathFound));
                }
            }

            if (Input.GetKeyUp(KeyCode.F))
            {
                Flash();
            }
        }

        steer.FollowPath();
        // steer.SeekMouse();
        steer.Avoid();
        steer.Update();
        transform.LookAt(transform.position + Velocity);
    }

    private void Flash()
    {
        Vector3 targetPosition = steer.GetMousePoint(Input.mousePosition);

        //Border case?
        if (targetPosition != Vector3.zero)
        {
            transform.position = targetPosition;
            Velocity = Vector3.zero;
            Path = null;
        }
    }

    private void OnDrawGizmos()
    {
        if (steer != null && displayForces)
        {
            // Velocity
            Gizmos.color = Color.green;
            // Gizmos.DrawLine(transform.position, transform.position + steer.forces.velocity);

            // Desired
            Gizmos.color = Color.black;
            // Gizmos.DrawLine(transform.position, transform.position + steer.forces.targetVelocity);

            // Steering
            Gizmos.color = Color.blue;
            //Gizmos.DrawLine(transform.position, transform.position + steer.forces.seek);

            // Look ahead
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(transform.position, transform.position + steer.forces.lookAhead);

            // Avoid
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + steer.forces.avoid);
        }

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