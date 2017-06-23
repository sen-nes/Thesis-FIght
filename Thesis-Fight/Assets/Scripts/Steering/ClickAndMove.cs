using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(FollowPath))]
[RequireComponent(typeof(Avoidance))]
public class ClickAndMove : MonoBehaviour
{
    public bool drawPath;
    public StackPath path;

    private SelectionManager selectionManager;
    private SteeringManager steeringManager;
    private FollowPath followPath;
    private Avoidance avoidance;

    private Animator anim;

    private void Start()
    {
        selectionManager = GameObject.FindObjectOfType<SelectionManager>();
        steeringManager = GetComponent<SteeringManager>();
        followPath = GetComponent<FollowPath>();
        avoidance = GetComponent<Avoidance>();
        anim = GetComponentInChildren<Animator>();
    }

    public void OnPathFound(Vector3[] path, bool success)
    {
        if (success)
        {
            Array.Reverse(path);
            this.path = new StackPath(path);

            StopAllCoroutines();
            StartCoroutine(Follow());
        }
        else
        {
            this.path = null;
        }
    }

    private void Update()
    {
        if (selectionManager.selectedObject == this.gameObject && Input.GetMouseButtonUp((int)MouseButton.MB_RIGHT))
        {
            GetComponent<Build>().CancelBuildingTask();
            CancelPath();
            RequestPathToLocation(Helpers.RaycastFloor(LayerMask.GetMask("Floor")));
        }
    }

    public void RequestPathToLocation(Vector3 loc)
    {
        StopAllCoroutines();
        PathRequestManager.RequestPath(new PathRequest(transform.position, loc, OnPathFound));
    }

    public void CancelPath()
    {
        StopAllCoroutines();
        path = null;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        anim.SetBool("Running", false);
    }

    public IEnumerator Follow()
    {
        anim.SetBool("Running", true);

        if (path != null)
        {
            while (true)
            {
                Vector3 follow = followPath.Follow(path);

                if (followPath.hasArrived)
                {
                    anim.SetBool("Running", false);
                    Debug.Log("Stopped at " + transform.position + GetComponent<Rigidbody>().velocity);
                    yield break;
                }

                Vector3 avoid = avoidance.Avoid();

                if (avoid.magnitude > 0.005f)
                {
                    follow = avoid;
                }

                steeringManager.Steer(follow);
                steeringManager.FaceMovementDirection();

                yield return null;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (drawPath && path != null)
        {
            Vector3[] waypoints = path.pathNodes;

            for (int i = 0; i < waypoints.Length; i++)
            {
                Gizmos.DrawWireCube(waypoints[i], Vector3.one);
            }
        }
    }
}