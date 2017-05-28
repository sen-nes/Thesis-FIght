using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuilderController : MonoBehaviour
{
    public GameObject building;

    private int floorMask;

    private bool isBuilding;
    private GameObject currentBuilding;

    private void Start()
    {
        floorMask = LayerMask.GetMask("Floor");
        isBuilding = false;

        building = Resources.Load<GameObject>("Buildings/Building East");
    }

    private void Update()
    {
        if (isBuilding)
        {
            MoveBuilding();

            if (Input.GetMouseButtonUp((int)MouseButton.MB_LEFT))
            {
                PlaceBuilding();
            }

            if (Input.GetKeyUp(KeyCode.Escape))
            {
                CancelBuilding();
            }
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.Q))
            {
                StartBuilding(0);
            }
        }
    }


    private void StartBuilding(int buildingIndex)
    {
        Debug.Log("Building...");
        Vector3 worldPoint = GetWorldPoint();

        worldPoint = Grid.instance.NodeFromPoint(worldPoint).position;
        currentBuilding = Instantiate(building, worldPoint, Quaternion.identity);

        isBuilding = true;
    }

    private void MoveBuilding()
    {
        Debug.Log("Moved!");
        Vector3 worldPoint = GetWorldPoint();

        worldPoint = Grid.instance.NodeFromPoint(worldPoint).position;
        currentBuilding.transform.position = worldPoint;
    }

    private void PlaceBuilding()
    {
        Vector3 worldPoint = GetWorldPoint();

        worldPoint = Grid.instance.NodeFromPoint(worldPoint).position;

        Debug.Log("Placed building at " + worldPoint);
        currentBuilding.transform.SetParent(GameObject.Find("Buildings").transform);
        currentBuilding.GetComponent<IBuilding>().StartSpawning();

        isBuilding = false;
        currentBuilding = null;
    }

    private void CancelBuilding()
    {
        Debug.Log("Canceled building!");

        if (currentBuilding != null)
        {
            Destroy(currentBuilding);
            currentBuilding = null;
        }

        isBuilding = false;
    }

    private Vector3 GetWorldPoint()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(camRay, out hit, Mathf.Infinity, floorMask);

        return hit.point;
    }
}