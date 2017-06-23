using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SteeringManager))]
public class FollowPath : MonoBehaviour {

    public float stopRadius = 0.05f;
    public float turnRadius = 0.5f;

    public bool hasArrived;
    
    private Rigidbody rb;
    private SteeringManager steeringManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        steeringManager = GetComponent<SteeringManager>();
    }

    public Vector3 Follow(StackPath path)
    {
        hasArrived = false;

        // Throws a nullPointer exception when target position is in the grass
        if (GameStartManager.HumanBuilder == gameObject)
        {
            Debug.Log(Vector3.Distance(transform.position, path.Last));
        }
        if (Vector3.Distance(transform.position, path.Last) < stopRadius)
        {
            rb.velocity = Vector3.zero;
            hasArrived = true;
            Debug.Log("Arrive");
            return Vector3.zero;
        }

        Vector3 targetPosition = path.GetTargetPosition(transform.position, turnRadius);

        return steeringManager.Arrive(targetPosition);
    }
}
