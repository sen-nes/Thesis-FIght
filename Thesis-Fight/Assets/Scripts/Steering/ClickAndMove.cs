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
            PathRequestManager.RequestPath(new PathRequest(transform.position, Helpers.RaycastFloor(LayerMask.GetMask("Floor")), OnPathFound));
        }
    }

    public IEnumerator Follow()
    {
        anim.SetBool("Running", true);

        if (path != null)
        {
            Debug.Log("Following");
            while (true)
            {
                Vector3 accel = avoidance.Avoid();
                if (accel.magnitude < 0.005f)
                {
                    accel = followPath.Follow(path);
                }
                
                steeringManager.Steer(accel);
                steeringManager.FaceMovementDirection();

                if (accel == Vector3.zero)
                {
                    anim.SetBool("Running", false);
                    yield break;
                }

                yield return null;
            }
        }

        anim.SetBool("Running", false);
    }

    private void OnEnable()
    {
        if (path != null)
        {
            StartCoroutine(Follow());
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
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