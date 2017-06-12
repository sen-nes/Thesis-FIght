using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionManager : MonoBehaviour {
    public GameObject selectedObject;

    private int selectableMask;
    private HUDManager hudManager;

    private void Awake()
    {
        selectableMask = LayerMask.GetMask("Selectable");
        hudManager = GameObject.Find("UI").transform.Find("HUD").GetComponent<HUDManager>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp((int)MouseButton.MB_LEFT) && !EventSystem.current.IsPointerOverGameObject() 
            && !GameStartManager.HumanBuilder.GetComponent<Build>().isBuilding)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, selectableMask))
            {
                // find correct root
                SelectObject(hit.transform.gameObject);
            }
            else
            {
                ClearSelection();
            }
        }
        else
        {
            // Setup a proper check
            if (selectedObject == null)
            {
                ClearSelection();
            } 
        }
    }

    private void SelectObject(GameObject obj)
    {
        if (selectedObject != null)
        {
            if (selectedObject == obj)
            {
                return;
            }
            else
            {
                ClearSelection();
            }
        }

        Debug.Log(obj.name + " selected");
        selectedObject = obj;
        hudManager.UpdateHUD(selectedObject);
    }

    private void ClearSelection()
    {
        selectedObject = null;
        hudManager.UpdateHUD(selectedObject);
    }

    // On death
}
