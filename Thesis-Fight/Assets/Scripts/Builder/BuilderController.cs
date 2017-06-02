using UnityEngine;

public class BuilderController : MonoBehaviour
{
    public int playerID;

    private int floorLayer;
    private int combatLayer;

    private bool isBuilding;
    private GameObject[] buildings;
    private GameObject currentBuilding;
    private Transform buildingsParent;

    private Material originalMaterial;
    private Material buildingOK;
    private Material buildingError;
    private float incomePercentage;

    private ISelectable selectable;

    // Create IBuilding field

    private void Start()
    {
        floorLayer = LayerMask.GetMask("Floor");
        combatLayer = LayerMask.NameToLayer("Combat");
        isBuilding = false;
        incomePercentage = 0.05f;

        buildingOK = Resources.Load<Material>("Materials/building_OK");
        buildingError = Resources.Load<Material>("Materials/building_NOT_OK");

        buildings = new GameObject[2];
        buildings[0] = Resources.Load<GameObject>("Buildings/Building East");
        buildings[1] = Resources.Load<GameObject>("Buildings/Building West");

        buildingsParent = GameObject.Find("Buildings").transform;

        selectable = transform.Find("Selectable").GetComponent<ISelectable>();

        playerID = 0;
    }

    private void Update()
    {
        if (isBuilding)
        {
            // more complex if?
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
            if (selectable.Selected)
            {
                if (Input.GetKeyUp(KeyCode.Q))
                {
                    StartBuilding(0);
                }

                if (Input.GetKeyUp(KeyCode.W))
                {
                    StartBuilding(1);
                }
            }
        }
    }
    
    private Vector3 GetWorldPoint()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(camRay, out hit, Mathf.Infinity, floorLayer);

        return hit.point;
    }

    public void StartBuilding(int buildingIndex)
    {
        if (GoldManager.instance.HasGold(playerID, 300) && !StateManager.instance.IsMenuOpen)
        {
            Vector3 worldPoint = GetWorldPoint();
            worldPoint = Grid.instance.NodeFromPoint(worldPoint).position;
            currentBuilding = Instantiate(buildings[buildingIndex], worldPoint, Quaternion.identity);
            
            currentBuilding.transform.SetParent(buildingsParent);
            currentBuilding.layer = 0;
            originalMaterial = currentBuilding.transform.Find("Model").GetComponent<MeshRenderer>().material;

            isBuilding = true;
            StateManager.instance.IsBuilding = true;
        }
    }

    private void MoveBuilding()
    {
        Vector3 worldPoint = GetWorldPoint();

        worldPoint = Grid.instance.NodeFromPoint(worldPoint).position;
        // Cache transform?
        currentBuilding.transform.position = worldPoint;

        if (currentBuilding.GetComponent<IBuilding>().CanBuild)
        {
            currentBuilding.transform.Find("Model").GetComponent<MeshRenderer>().material = buildingOK;
        }
        else
        {
            currentBuilding.transform.Find("Model").GetComponent<MeshRenderer>().material = buildingError;
        }
    }

    private void PlaceBuilding()
    {
        if (currentBuilding.GetComponent<IBuilding>().CanBuild)
        {
            Vector3 worldPoint = GetWorldPoint();
            worldPoint = Grid.instance.NodeFromPoint(worldPoint).position;

            currentBuilding.GetComponent<IBuilding>().HidePlacementGrid();
            currentBuilding.GetComponent<IBuilding>().UpdateGrid();
            currentBuilding.GetComponent<IBuilding>().StartSpawning();

            currentBuilding.transform.Find("Model").GetComponent<MeshRenderer>().material = originalMaterial;
            currentBuilding.layer = combatLayer;

            currentBuilding = null;
            isBuilding = false;
            StateManager.instance.IsBuilding = false;

            GoldManager.instance.Pay(playerID, 300);
            GoldManager.instance.AddIncome(playerID, (int)(300 * incomePercentage));
        }
        else
        {
            Debug.Log("Can't build at this point.");
        }
    }

    private void CancelBuilding()
    {
        if (currentBuilding != null)
        {
            Destroy(currentBuilding);
            currentBuilding = null;
        }

        isBuilding = false;
        StateManager.instance.IsBuilding = false;
    }
}