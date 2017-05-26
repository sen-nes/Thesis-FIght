using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Forces
{
    public Vector3 velocity;
    public Vector3 seek;
    public Vector3 avoid;
    public Vector3 targetVelocity;
    public Vector3 lookAhead;
}

public class SteeringManager
{

    public Forces forces;

    private IBoid boid;
    private Vector3 steering;
    private int floorMask;

    public SteeringManager(IBoid _boid)
    {
        steering = Vector3.zero;
        boid = _boid;
        floorMask = LayerMask.GetMask("Floor");
    }

    public void Update()
    {
        steering = Vector3.ClampMagnitude(steering, boid.Force);
        steering /= boid.Mass;

        boid.Velocity += steering;
        boid.Velocity = Vector3.ClampMagnitude(boid.Velocity, boid.Speed);
        forces.velocity = boid.Velocity;

        boid.Velocity = new Vector3(boid.Velocity.x, 0.0f, boid.Velocity.z);
        boid.Position += boid.Velocity * Time.deltaTime;
    }

    public void SeekMouse()
    {
        Vector3 mouse = GetMousePoint(Input.mousePosition);
        Seek(mouse);
    }

    public void Seek(Vector3 target)
    {
        Vector3 force = Vector3.zero;
        Vector3 targetVelocity = target - boid.Position;

        targetVelocity = targetVelocity.normalized * boid.Speed;

        force = targetVelocity - boid.Velocity;
        steering += force;

        forces.seek = force;
        forces.targetVelocity = targetVelocity;
    }

    public void Avoid()
    {
        Vector3 force = Vector3.zero;
        float aheadModifier = boid.Velocity.magnitude / boid.Speed;
        Vector3 ahead = boid.Velocity.normalized * boid.Speed * 2;
        //ahead *= aheadModifier;
        forces.lookAhead = ahead;
        Transform obstacle = FindObstacle(ahead);

        if (obstacle != null)
        {
            Debug.Log("Avoiding " + obstacle.name);
            //force.x = ahead.x - obstacle.position.x;
            //force.z = ahead.z - obstacle.position.z;
            force = ahead - obstacle.position;

            force = force.normalized * 30.0f;//boid.Force;
        }
        else
        {
            force = Vector3.zero;
        }

        steering += force;
        forces.avoid = force;
    }

    public void FollowPath()
    {

        if (boid.Path != null)
        {
            Vector3 target = boid.Path.Peek();
            float distance = (target - boid.Position).magnitude;
            if (distance <= 3.0f)
            {
                boid.Path.Pop();
            }

            if (boid.Path.Count == 0)
            {
                boid.Path.Push(target);
            }

            if (boid.Path.Count > 1)
            {

                // Result of comparing V3 with null is always true?
                if (target != null)
                {
                    Seek(target);
                }
            }
            else
            {
                if (target != null)
                {
                    Arrive(target);
                }
            }
        }
    }

    public void Arrive(Vector3 target)
    {
        Vector3 force = Vector3.zero;
        Vector3 targetVelocity = target - boid.Position;
        float distance = targetVelocity.magnitude;

        targetVelocity = targetVelocity.normalized;
        if (distance <= boid.SlowDownRadius)
        {
            float speedModifier = distance / boid.SlowDownRadius;
            targetVelocity = targetVelocity * boid.Speed * speedModifier;
        }
        else
        {
            targetVelocity *= boid.Speed;
        }

        force = targetVelocity - boid.Velocity;
        steering += force;

        forces.seek = force;
        forces.targetVelocity = targetVelocity;
    }

    private Transform FindObstacle(Vector3 ahead)
    {
        RaycastHit obstacle;
        if (Physics.SphereCast(boid.Position, 2.0f, boid.Position + ahead, out obstacle))
        {
            Debug.Log(obstacle.transform.name);
            return obstacle.transform;
        }

        return null;
    }

    public Vector3 GetMousePoint(Vector3 mousePos)
    {
        Vector3 target = Vector3.zero;
        Ray camRay = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit targetInfo;

        if (Physics.Raycast(camRay, out targetInfo, Mathf.Infinity, floorMask))
        {
            target = targetInfo.point;
        }
        else
        {
            //Debug.Log("Raycast not hit.");
        }

        return target;
    }
}
