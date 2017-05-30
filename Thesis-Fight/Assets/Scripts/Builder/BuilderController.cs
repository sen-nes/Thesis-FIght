using UnityEngine;

public class BuilderController : MonoBehaviour
{
    public GameObject building;

    private int floorMask;

    private bool isBuilding;
    private GameObject currentBuilding;
    private Vector3 placementOffset;

    public Material originalMaterial;
    public Material buildingOK;
    public Material buildingError;

    public int playerID;

    // Create IBuilding field

    private void Start()
    {
        floorMask = LayerMask.GetMask("Floor");
        isBuilding = false;

        placementOffset = Vector3.zero;//new Vector3(Grid.instance.nodeSize / 2, 0.0f, Grid.instance.nodeSize / 2);

        buildingOK = Resources.Load<Material>("Materials/building_OK");
        buildingError = Resources.Load<Material>("Materials/building_NOT_OK");

        playerID = 0;
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
                if (GoldManager.instance.HasGold(0, 300))
                {
                    StartBuilding(0);
                }
            }

            if (Input.GetKeyUp(KeyCode.W))
            {
                if (GoldManager.instance.HasGold(0, 300))
                {
                    StartBuilding(1);
                }
            }
        }
    }


    private void StartBuilding(int buildingIndex)
    {
        Vector3 worldPoint = GetWorldPoint();

        if (buildingIndex == 0)
        {
            building = Resources.Load<GameObject>("Buildings/Building East");
        }
        else
        {
            building = Resources.Load<GameObject>("Buildings/Building West");
        }

        worldPoint = Grid.instance.NodeFromPoint(worldPoint).position - placementOffset;
        currentBuilding = Instantiate(building, worldPoint, Quaternion.identity);
        currentBuilding.transform.SetParent(GameObject.Find("Buildings").transform);
        originalMaterial = currentBuilding.transform.Find("Model").GetComponent<MeshRenderer>().material;

        isBuilding = true;
        SelectionManager.instance.IsBuilding = true;
    }

    private void MoveBuilding()
    {
        Vector3 worldPoint = GetWorldPoint();

        worldPoint = Grid.instance.NodeFromPoint(worldPoint).position - placementOffset;
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
            worldPoint = Grid.instance.NodeFromPoint(worldPoint).position - placementOffset;

            currentBuilding.GetComponent<IBuilding>().StartSpawning();
            currentBuilding.GetComponent<IBuilding>().HidePlacementGrid();

            currentBuilding.GetComponent<IBuilding>().UpdateGrid();

            currentBuilding.transform.Find("Model").GetComponent<MeshRenderer>().material = originalMaterial;
            isBuilding = false;
            SelectionManager.instance.IsBuilding = false;
            currentBuilding = null;

            GoldManager.instance.Pay(0, 300);
            GoldManager.instance.AddIncome(0, (int)(300 * 0.1f));
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
        SelectionManager.instance.IsBuilding = false;
    }

    private Vector3 GetWorldPoint()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(camRay, out hit, Mathf.Infinity, floorMask);

        return hit.point;
    }
}