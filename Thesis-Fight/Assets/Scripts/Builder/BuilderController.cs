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

    // Create IBuilding field

    private void Start()
    {
        floorMask = LayerMask.GetMask("Floor");
        isBuilding = false;

        building = Resources.Load<GameObject>("Buildings/Building East");
        placementOffset = Vector3.zero;//new Vector3(Grid.instance.nodeSize / 2, 0.0f, Grid.instance.nodeSize / 2);

        buildingOK = Resources.Load<Material>("Materials/building_OK");
        buildingError = Resources.Load<Material>("Materials/building_NOT_OK");
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
        Vector3 worldPoint = GetWorldPoint();

        worldPoint = Grid.instance.NodeFromPoint(worldPoint).position - placementOffset;
        currentBuilding = Instantiate(building, worldPoint, Quaternion.identity);
        currentBuilding.transform.SetParent(GameObject.Find("Buildings").transform);
        originalMaterial = currentBuilding.transform.Find("Model").GetComponent<MeshRenderer>().material;

        isBuilding = true;
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
            currentBuilding = null;
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
    }

    private Vector3 GetWorldPoint()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(camRay, out hit, Mathf.Infinity, floorMask);

        return hit.point;
    }
}