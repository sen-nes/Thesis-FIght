using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SteeringManager))]
public class Avoidance : MonoBehaviour {

    // Fidget with those values
    public float checkAhead = 1.75f;
    public float distFromObstacle = 1f;
    public float checkSides = 1.2f;
    public float sideAngle = 45f;

    public float maxAcceleration = 40.0f;

    private Rigidbody rb;
    private SteeringManager steeringManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        steeringManager = GetComponent<SteeringManager>();
    }

    public Vector3 Avoid()
    {
        Vector3 acceleraion = Vector3.zero;

        Vector3[] rayDirs = new Vector3[3];

        // Is this calculated correctly
        // Throws an Object reference not set to an instance of an object exception at times.
        // Happened when units were circling around the wall and destroying enemy structures and castle.
        float orientation = Mathf.Atan2(rb.velocity.z, rb.velocity.x);

        rayDirs[0] = rb.velocity.normalized;
        rayDirs[1] = OrientationToVector(orientation + sideAngle * Mathf.Deg2Rad);
        rayDirs[2] = OrientationToVector(orientation - sideAngle * Mathf.Deg2Rad);

        // Test if it works as expected
        RaycastHit hit;
        if (!FindObstacle(rayDirs, out hit))
        {
            return acceleraion;
        }

        // Give correct avoidance value
        Vector3 targetPosition = hit.point + hit.normal * distFromObstacle;
        Vector3 cross = Vector3.Cross(rb.velocity, hit.normal);
        if (cross.magnitude < 0.005f)
        {
            // Why are x and z swapped
            targetPosition += new Vector3(-hit.normal.z, hit.normal.y, hit.normal.x);
        }

        // Take into account avoidance force
        return steeringManager.Seek(targetPosition, maxAcceleration);
    }   

    private Vector3 OrientationToVector(float orientation)
    {
        // Find out where do the cos and sin go
        return new Vector3(Mathf.Cos(orientation), 0.0f, Mathf.Sin(orientation));
    }

    private bool FindObstacle(Vector3[] rayDirs, out RaycastHit firstHit)
    {
        firstHit = new RaycastHit();
        bool obstacle = false;

        for (int i = 0; i < rayDirs.Length; i++)
        {
            float dist = (i == 0) ? checkAhead : checkSides;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, rayDirs[i], out hit, dist))
            {
                if (!hit.transform.CompareTag("Floor"))
                {
                    firstHit = hit;
                    obstacle = true;
                    break;
                }
            }
        }

        return obstacle;
    }
}
