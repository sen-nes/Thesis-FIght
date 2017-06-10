using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SteeringManager : MonoBehaviour
{
    // Consider transfering to calculations in 2D

    public float maxVelocity = 5.0f;
    public float maxForce = 10.0f;
    public float arrivalRadius = 0.05f;
    public float slowRadius = 1.0f;
    public float arrivalTime = 0.1f;

    private Rigidbody rb;
    // Transform tr;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Steer(Vector3 steering)
    {
        rb.velocity += steering * Time.deltaTime;

        // Consider using sqrMaginitude
        if (rb.velocity.magnitude > maxVelocity)
        {
            rb.velocity = rb.velocity.normalized * maxVelocity;
        }
    }

    public Vector3 SeekMouse()
    {
        // Cache layer mask
        Vector3 mouse = Helpers.GetFloorPoint(LayerMask.GetMask("Floor"));

        return Seek(mouse);
    }

    public Vector3 Seek(Vector3 targetPosition)
    {
        Vector3 acceleration = targetPosition - transform.position;
        acceleration.y = 0f;

        acceleration.Normalize();
        acceleration = acceleration * maxForce;

        return acceleration;
    }

    public Vector3 Seek(Vector3 targetPosition, float maxSeekForce)
    {
        Vector3 acceleration = targetPosition - transform.position;
        acceleration.y = 0f;

        acceleration.Normalize();
        acceleration = acceleration * maxSeekForce;

        return acceleration;
    }

    public Vector3 ArriveAtMouse()
    {
        // Cache layer mask
        Vector3 mouse = Helpers.GetFloorPoint(LayerMask.GetMask("Floor"));

        return Arrive(mouse);
    }

    public Vector3 Arrive(Vector3 targetPosition)
    {
        Vector3 targetVelocity = targetPosition - transform.position;
        targetVelocity.y = 0f;

        // Consider using sqrMagnitude
        float distance = targetVelocity.magnitude;
        if (distance < arrivalRadius)
        {
            rb.velocity = Vector3.zero;
            return Vector3.zero;
        }

        float speedModifier;
        if (distance > slowRadius)
        {
            speedModifier = maxVelocity;
        }
        else
        {
            speedModifier = maxVelocity * (distance / slowRadius);
        }

        targetVelocity.Normalize();
        targetVelocity *= speedModifier;

        // Calculate arrival in a predifined interval of time
        Vector3 acceleration = targetVelocity - new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);
        acceleration *= 1 / arrivalTime;

        if (acceleration.magnitude > maxForce)
        {
            acceleration.Normalize();
            acceleration *= maxForce;
        }
        
        return acceleration;
    }

    public void FaceMovementDirection()
    {
        transform.LookAt(transform.position + rb.velocity);
    }
}
