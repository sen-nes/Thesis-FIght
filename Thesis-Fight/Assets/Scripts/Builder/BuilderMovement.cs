using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuilderMovement : MonoBehaviour
{

    enum MouseButtonDown
    {
        MBD_LEFT = 0,
        MBD_RIGHT,
        MBD_MIDDLE,
    };

    public float speed;
    public float stoppingDistance;

    private Vector3[] path;
    private int waypointIndex;
    //private NavMeshAgent playerAgent;
    private int floorMask;

    private void Start()
    {
        //playerAgent = GetComponent<NavMeshAgent>();
        //playerAgent.speed = speed;
        floorMask = LayerMask.GetMask("Floor");
    }

    private void Update()
    {
        if (Input.GetMouseButton((int)MouseButtonDown.MBD_RIGHT))
        {
            Move();
        }

        if (Input.GetKeyUp(KeyCode.F))
        {
            Flash();
        }
    }

    private void ChangeForwardVector()
    {
        Vector3 steering = transform.right;
        steering.Scale(new Vector3(3f, 0f, 0f));
        transform.position = Vector3.MoveTowards(transform.position, transform.position + steering, speed * Time.deltaTime);

    }

    private void Move()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit targetInfo;

        if (Physics.Raycast(ray, out targetInfo, Mathf.Infinity, floorMask))
        {
            PathRequest request = new PathRequest(transform.position, targetInfo.point, OnPathFound);
            PathRequestManager.RequestPath(request);
            //transform.LookAt(targetInfo.point);
            //transform.position = Vector3.MoveTowards(transform.position, targetInfo.point, speed * Time.deltaTime);
        }
    }

    public void OnPathFound(Vector3[] newPath, bool success)
    {
        if (success)
        {
            path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    private IEnumerator FollowPath()
    {
        Vector3 unitHeight = new Vector3(0f, transform.position.y, 0f);
        Vector3 currentWaypoint = path[0] + unitHeight;
        Vector3 lastWaypoint = path[path.Length - 1];
        waypointIndex = 0;
        transform.LookAt(currentWaypoint);

        if (waypointIndex >= path.Length || Vector3.Distance(transform.position, lastWaypoint) <= 2f)
        {
            Debug.Log(Vector3.Distance(transform.position, lastWaypoint));
            yield break;
        }

        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                waypointIndex++;
                if (waypointIndex >= path.Length || Vector3.Distance(transform.position, lastWaypoint) <= 2f)
                {
                    Debug.Log(Vector3.Distance(transform.position, lastWaypoint));
                    yield break;
                }
                currentWaypoint = path[waypointIndex] + unitHeight;
                transform.LookAt(currentWaypoint);
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnDrawGizmos()
    {

        if (path != null)
        {
            for (int i = waypointIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one);

                if (i == waypointIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }

    private void Flash()
    {
        // costs

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit targetInfo;

        if (Physics.Raycast(ray, out targetInfo, Mathf.Infinity))
        {
            //playerAgent.destination = targetInfo.point;
            transform.position = targetInfo.point;
        }
    }
}