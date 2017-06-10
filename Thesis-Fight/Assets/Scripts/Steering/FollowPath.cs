using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SteeringManager))]
public class FollowPath : MonoBehaviour {

    public float stopRadius = 0.005f;
    public float turnRadius = 0.5f;
    
    private Rigidbody rb;
    private SteeringManager steeringManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        steeringManager = GetComponent<SteeringManager>();
    }

    public Vector3 Follow(StackPath path)
    {
        // Throws a nullPointer exception when target position is in the grass
        if (Vector3.Distance(transform.position, path.Last) < stopRadius)
        {
            rb.velocity = Vector3.zero;
            return Vector3.zero;
        }

        Vector3 targetPosition = path.GetTargetPosition(transform.position, turnRadius);

        return steeringManager.Arrive(targetPosition);
    }
}
