using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionManager : MonoBehaviour {
    public GameObject selectedObject;

    private int selectableMask;

    private void Awake()
    {
        selectableMask = LayerMask.GetMask("Selectable");
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp((int)MouseButton.MB_LEFT) && !EventSystem.current.IsPointerOverGameObject())
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
    }

    private void ClearSelection()
    {
        selectedObject = null;
    }

    // On death
}
