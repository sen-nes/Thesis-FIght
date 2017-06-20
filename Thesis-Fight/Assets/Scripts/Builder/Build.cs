using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour {

    // Generic array?
    public List<GameObject> buildings;
    public bool isBuilding;

    private SelectionManager selectionManager;
    private InGameMenuManager inGameMenuManager;

    private Transform buildingsParent;
    private GameObject currentBuilding;

    private BuildingGrid buildingGrid;

    private Material buildingMaterial;

    // Set in inspector or set in code?
    public Material buildingOK;
    public Material buildingError;

    private int floorLayer;
    private int combatLayer;

    private void Awake()
    {
        isBuilding = false;

        // Ensure selection of correct objects
        selectionManager = GameObject.FindObjectOfType<SelectionManager>();
        inGameMenuManager = GameObject.FindObjectOfType<InGameMenuManager>();

        buildingsParent = GameObject.Find("Buildings").transform;

        // Find in a more appropriate way
        buildingGrid = GameObject.FindObjectOfType<BuildingGrid>();

        floorLayer = LayerMask.GetMask("Floor");
        combatLayer = LayerMask.NameToLayer("Combat");
    }

    private void Update()
    {
        // create local variable

        if (!isBuilding)
        {
            if (selectionManager.selectedObject == this.gameObject && !inGameMenuManager.isOpen)
            {
                if (Input.GetKeyUp(KeyCode.Q))
                {
                    BeginBuilding(0);
                }

                if (Input.GetKeyUp(KeyCode.W))
                {
                    BeginBuilding(1);
                }

                if (Input.GetKeyUp(KeyCode.E))
                {
                    BeginBuilding(2);
                }
            }
        }
        else
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
    }

    // Consider setting up a coroutine for building from HUD
    public void BeginBuilding(int index)
    {
        // Give correct values to GoldManager
        if (GoldManager.instance.HasGold(GetComponent<BuilderController>().playerID, buildings[index].GetComponent<BuildingController>().building.cost))
        {
            Vector3 buildPoint = GetBuildPoint();

            GameObject building = buildings[index];

            building.GetComponent<BuildingController>().teamID = GetComponent<BuilderController>().teamID;
            building.GetComponent<BuildingController>().playerID = GetComponent<BuilderController>().playerID;
            building.GetComponent<BuildingController>().enemyCastle = GetComponent<BuilderController>().enemyCastle;

            currentBuilding = Instantiate(building, buildPoint, Quaternion.identity, buildingsParent);

            // Renderer or MeshRenderer
            buildingMaterial = currentBuilding.GetComponentInChildren<Renderer>().material;

            buildingGrid.Show();
            // Update material accordingly
            if (buildingGrid.UpdateGrid(currentBuilding.transform))
            {
                currentBuilding.GetComponentInChildren<Renderer>().material = buildingOK;
            }
            else
            {
                currentBuilding.GetComponentInChildren<Renderer>().material = buildingError;
            }



            isBuilding = true;
            // Notify menu
        }
        else
        {
            Debug.Log("Not enough gold.");
        }
    }

    private void MoveBuilding()
    {
        Vector3 buildPoint = GetBuildPoint();

        // Cache transform, consider setting up a check 
        // to see if any movement happened
        currentBuilding.transform.position = buildPoint;

        // Update material accordingly
        if (buildingGrid.UpdateGrid(currentBuilding.transform))
        {
            currentBuilding.GetComponentInChildren<Renderer>().material = buildingOK;
        }
        else
        {
            currentBuilding.GetComponentInChildren<Renderer>().material = buildingError;
        }

        // Check BuildingGrid for canBuild and update material
    }

    private void PlaceBuilding()
    {
        // Check if player has enough money again

        if (buildingGrid.UpdateGrid(currentBuilding.transform))
        {
            // Hide grid and update world
            buildingGrid.Hide();

            // Renderer or MeshRenderer
            currentBuilding.GetComponentInChildren<Renderer>().material = buildingMaterial;
            currentBuilding.layer = combatLayer;
            currentBuilding.GetComponent<Spawner>().StartSpawning();

            // Give correct playerID and building cost
            // should GoldManager be a global object or local to the builder
            GoldManager.instance.Pay(GetComponent<BuilderController>().playerID, currentBuilding.GetComponent<BuildingController>().building.cost);

            currentBuilding = null;
            isBuilding = false;

            // Notify menu
        }
        else
        {
            // Log a notification if the player can't build at point
            Debug.Log("Can't build at this spot.");
        }
    }

    private void CancelBuilding()
    {
        if (currentBuilding != null)
        {
            buildingGrid.Hide();
            Destroy(currentBuilding);
            currentBuilding = null;
        }

        isBuilding = false;
        // Notify menu
    }

    private Vector3 GetBuildPoint()
    {
        // Think of a neater way to ensure you are getting discernible feedback
        // Cache layer mask
        Vector3 buildPoint = Helpers.RaycastFloor(LayerMask.GetMask("Floor"));
        buildPoint = Grid.instance.NodeFromPoint(buildPoint).position;

        return buildPoint;
    }
}
