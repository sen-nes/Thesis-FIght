using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SteeringManager))]
public class Pursue : MonoBehaviour {

    public float maxPrediction = 1f;

    private Rigidbody rb;
    private SteeringManager sm;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        sm = GetComponent<SteeringManager>();
    }

    public Vector3 PursueTarget(Rigidbody target)
    {
        Vector3 displacement = target.position - transform.position;
        float distance = displacement.magnitude;

        float speed = rb.velocity.magnitude;

        float predicion;
        if (speed <= distance / maxPrediction)
        {
            predicion = maxPrediction;
        }
        else
        {
            predicion = distance / speed;
        }

        Vector3 predictedTarget = target.position + target.velocity * predicion;

        return sm.Seek(predictedTarget);
    }
}
