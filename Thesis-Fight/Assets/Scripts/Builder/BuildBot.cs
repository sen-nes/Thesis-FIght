using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildBot : MonoBehaviour {

    public List<GameObject> buildings;

    private Node[,] buildArea;
    private Transform buildingsParent;
    private GameObject currentBuilding;
    private GameObject nextBuilding;
    private BuilderController builderController;
    private int combatLayer;

    private bool onBuildingTask;
    private Animator anim;
    private StackPath path;

    private SteeringManager steeringManager;
    private FollowPath followPath;
    private Avoidance avoidance;

    // pattern

    private void Awake()
    {
        buildingsParent = GameObject.Find("Buildings").transform;
        builderController = GetComponent<BuilderController>();
        combatLayer = LayerMask.NameToLayer("Combat");

        anim = GetComponentInChildren<Animator>();

        steeringManager = GetComponent<SteeringManager>();
        followPath = GetComponent<FollowPath>();
        avoidance = GetComponent<Avoidance>();

        onBuildingTask = false;
    }

    private void Start()
    {
        buildArea = Grid.instance.GetTeamArea(builderController.teamID);
        nextBuilding = buildings[UnityEngine.Random.Range(0, buildings.Count)];
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

            transform.position = Grid.instance.FindFreeNode((Teams)GetComponent<BuilderController>().teamID, transform.position);
            currentBuilding.SetActive(true);
            var extents = currentBuilding.transform.Find("Model").GetComponent<Collider>().bounds.extents;
            Destroy(currentBuilding);

            Grid.instance.UpdateGridRegion((int)extents.x, (int)extents.z, currentBuilding.transform.position);
            onBuildingTask = false;
        }
    }
    
    private void Update()
    {
        if (onBuildingTask)
        {
            float distanceToBuilding = Vector3.Distance(transform.position, currentBuilding.transform.Find("Spawn Point").position);

            if (distanceToBuilding <= 1f)
            {
                //currentBuilding.transform.Find("Model").GetChild(0).gameObject.SetActive(true);
                GoldManager.instance.Pay(GetComponent<BuilderController>().playerID, currentBuilding.GetComponent<BuildingController>().building.cost);
                currentBuilding.SetActive(true);
                currentBuilding.GetComponent<Spawner>().StartSpawning();

                currentBuilding = null;
                onBuildingTask = false;

                // Stop moving
                CancelPath();
                nextBuilding = buildings[UnityEngine.Random.Range(0, buildings.Count)];
            }
        }
        else
        {
            if (GoldManager.instance.HasGold(builderController.playerID, nextBuilding.GetComponent<BuildingController>().building.cost))
            {
                Build();
            }
        }
    }

    private void CancelPath()
    {
        StopAllCoroutines();
        path = null;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        anim.SetBool("Running", false);
    }

    private IEnumerator Follow()
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

    private void StartBuildingTask()
    {
        Vector3 pos = currentBuilding.GetComponent<Spawner>().UpdateSpawnPoint();

        // Request Path
        StopAllCoroutines();
        PathRequestManager.RequestPath(new PathRequest(transform.position, pos, OnPathFound));
        
        //currentBuilding.transform.Find("Model").GetChild(0).gameObject.SetActive(false);
        currentBuilding.SetActive(false);
        onBuildingTask = true;
    }

    private void Build()
    {
        nextBuilding.GetComponent<BuildingController>().teamID = GetComponent<BuilderController>().teamID;
        nextBuilding.GetComponent<BuildingController>().playerID = GetComponent<BuilderController>().playerID;
        nextBuilding.GetComponent<BuildingController>().enemyCastle = GetComponent<BuilderController>().enemyCastle;

        currentBuilding = Instantiate(nextBuilding);

        Vector3 buildPoint = GetBuildPoint();
        if (buildPoint != Vector3.zero)
        {
            currentBuilding.transform.SetParent(buildingsParent);
            currentBuilding.transform.position = buildPoint;
            currentBuilding.layer = combatLayer;

            Grid.instance.UpdateGridRegion((int)currentBuilding.transform.Find("Model").GetComponent<Collider>().bounds.extents.x, (int)currentBuilding.transform.Find("Model").GetComponent<Collider>().bounds.extents.z, currentBuilding.transform.position);
            StartBuildingTask();
        }
        else
        {
            Destroy(currentBuilding);
        }
    }

    private Vector3 GetBuildPoint()
    {
        int X = buildArea.GetLength(0);
        int Y = buildArea.GetLength(1);

        for (int x = 0; x < X; x++)
        {
            for (int y = 0; y < Y; y++)
            {
                if (CheckBuildPoint(buildArea[x, y].position))
                {
                    return buildArea[x, y].position;
                }
            }
        }

        return Vector3.zero;
    }
    
    private bool CheckBuildPoint(Vector3 pos)
    {
        Vector3 size = currentBuilding.transform.Find("Model").GetComponent<Collider>().bounds.size;
        float tmp = size.x;
        int X = (int)(tmp / 2);
        if ((int)tmp % 2 == 1)
        {
            if (tmp % 1.0f > 0)
            {
                X++;
            }
        }

        tmp = size.z;
        int Y = (int)(tmp / 2);
        if ((int)tmp % 2 == 1)
        {
            if (tmp % 1.0f > 0)
            {
                Y++;
            }
        }

        return Grid.instance.CheckGridRegion(X, Y, pos);
    }
}
