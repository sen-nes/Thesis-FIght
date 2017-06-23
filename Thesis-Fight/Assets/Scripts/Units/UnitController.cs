using System;
using UnityEngine;

public class UnitController : MonoBehaviour {

    // Scriptable object for configuration

    public bool drawPath;
    // Change name of class to a more generic one
    public Unit unit;
    public Vector3 enemyCastle;

    public Teams teamID;
    public int playerID;
    public int killValue;

    private SteeringManager steeringManager;
    private FollowPath followPath;
    private Avoidance avoidance;
    private Pursue pursue;
    private FighterStats fighterStats;
    private FindTarget findTarget;

    private StackPath path;

    private void Awake()
    {
        unit.Initialize(gameObject);
        this.GetComponent<Attackable>().teamID = teamID;
        this.GetComponent<Attackable>().KillValue = killValue;
    }

    private void Start()
    {
        steeringManager = GetComponent<SteeringManager>();
        followPath = GetComponent<FollowPath>();
        avoidance = GetComponent<Avoidance>();   
        pursue = GetComponent<Pursue>();
        fighterStats = GetComponent<FighterStats>();
        findTarget = GetComponent<FindTarget>();

        steeringManager.maxVelocity = fighterStats.MovementSpeed.FinalValue;

        PathRequestManager.RequestPath(new PathRequest(transform.position, enemyCastle, OnPathFound));
    }

    public void OnPathFound(Vector3[] path, bool success)
    {
        if (success)
        {
            Array.Reverse(path);
            this.path = new StackPath(path);
        }
        else
        {
            this.path = null;
        }
    }

    private void Update()
    {
        // Finite-State Machine seems very appropriate for the task

        if (findTarget.target == null)
        {
            if (path != null)
            {
                Vector3 accel;
                if (Time.frameCount % 5 == 0)
                {
                    accel = avoidance.Avoid();
                    if (accel.magnitude < 0.005f)
                    {
                        accel = followPath.Follow(path);
                    }
                }
                else
                {
                    accel = followPath.Follow(path);
                }
                
                steeringManager.Steer(accel);
                steeringManager.FaceMovementDirection();
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
