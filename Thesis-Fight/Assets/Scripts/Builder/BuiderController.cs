using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuilderController : MonoBehaviour
{

    public int repairSpeed;
    // Not 100% necessary
    public int movementSmoothing;
    public GameObject building;

    private int id;
    private int gold;
    private int floorMask;
    // private List<Building> buildings;
    private bool currentlyBuilding;
    private GameObject currentBuilding;
    private UnitStats stats;

    private void Start()
    {
        floorMask = LayerMask.GetMask("Floor");
        stats = GetComponent<UnitStats>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            Debug.Log(stats.Health.FinalValue);
        }

        if (!currentlyBuilding)
        {
            if (Input.GetKeyUp(KeyCode.Q))
            {
                Build(0);
            }
        }

        if (currentlyBuilding)
        {
            MoveBuilding();

            if (Input.GetMouseButtonUp(0))
            {
                PlaceBuilding();
            }

            if (Input.GetKeyUp(KeyCode.Escape))
            {
                CancelBuilding();
            }
        }
    }


    private void Build(int buildingIndex)
    {
        Debug.Log("Building!");

        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit buildPoint;
        if (Physics.Raycast(camRay, out buildPoint, Mathf.Infinity, floorMask))
        {
            Vector3 position = new Vector3(buildPoint.point.x, building.transform.localScale.y / 2f, buildPoint.point.z);
            currentBuilding = Instantiate(building, position, Quaternion.identity);
        }

        currentlyBuilding = true;
    }

    private void PlaceBuilding()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit buildPoint;
        if (Physics.Raycast(camRay, out buildPoint, Mathf.Infinity, floorMask))
        {
            Debug.Log("Placed building at: " + buildPoint.point + "!");
        }

        currentBuilding.transform.SetParent(GameObject.Find("Buildings").transform);

        currentlyBuilding = false;
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

        currentlyBuilding = false;
    }

    private void MoveBuilding()
    {
        Debug.Log("Moved!");

        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit buildPoint;
        if (Physics.Raycast(camRay, out buildPoint, Mathf.Infinity, floorMask))
        {
            Vector3 position = new Vector3(buildPoint.point.x, building.transform.localScale.y / 2f, buildPoint.point.z);
            currentBuilding.transform.position = Vector3.Lerp(currentBuilding.transform.position, position, Time.deltaTime * movementSmoothing);
        }
    }

    private void Repair()
    {

    }

    private void payGold()
    {

    }

}