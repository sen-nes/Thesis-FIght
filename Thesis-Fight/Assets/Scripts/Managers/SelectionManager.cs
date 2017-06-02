using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager instance;
    // public GameObject selectionPlane;

    public GameObject builderPanel;
    public GameObject buildingPanel;
    public GameObject unitPanel;
    public GameObject castlePanel;

    private GameObject activePanel;

    private int selectableMask;
    private List<ISelectable> selectedUnits;
    private GameObject selectionParent;
    private EventSystem es;

    private void Start()
    {
        instance = this;
        selectableMask = LayerMask.GetMask("Selectable");
        selectedUnits = new List<ISelectable>();
        es = EventSystem.current;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown((int)MouseButton.MB_LEFT) && !StateManager.instance.IsBuilding && !es.IsPointerOverGameObject())
        {
            CheckSelection(Input.mousePosition);
        }
    }

    private void CheckSelection(Vector3 mousePosition)
    {
        Ray camRay = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit rayHit;

        if (Physics.Raycast(camRay, out rayHit, Mathf.Infinity, selectableMask))
        {
            ClearSeletion();
            ISelectable selected = rayHit.collider.GetComponent<ISelectable>();

            // Create update selection method
            selectedUnits.Add(selected);
            selectionParent = rayHit.collider.transform.parent.gameObject;
            UpdateUI();
            selected.Select();

        }
        else
        {
            ClearSeletion();
            if (activePanel != null)
            {
                activePanel.SetActive(false);
            }
            
            activePanel = null;
        }
    }

    public void UpdateUI()
    {
        if (selectedUnits.Count > 0)
        {
            if (selectionParent.CompareTag("Builder"))
            {
                builderPanel.SetActive(true);
                activePanel = builderPanel;
            }
            else
            {
                builderPanel.SetActive(false);
            }

            if (selectionParent.CompareTag("Building"))
            {
                buildingPanel.SetActive(true);
                activePanel = buildingPanel;
            }
            else
            {
                buildingPanel.SetActive(false);
            }

            if (selectionParent.CompareTag("Unit"))
            {
                unitPanel.SetActive(true);
                activePanel = unitPanel;
            }
            else
            {
                unitPanel.SetActive(false);
            }

            if (selectionParent.CompareTag("Castle"))
            {
                castlePanel.SetActive(true);
                activePanel = castlePanel;
            }
            else
            {
                castlePanel.SetActive(false);
            }
        }
    }

    // Building
    public void BuildingStopProduction()
    {
        if (selectedUnits.Count > 0)
        {
            // check
            BuildingController building = selectionParent.GetComponent<BuildingController>();
            building.StopSpawning();
        }
    }

    public void BuildingStartProduction()
    {
        if (selectedUnits.Count > 0)
        {
            BuildingController building = selectionParent.GetComponent<BuildingController>();
            building.StartSpawning();
        }
    }

    private void ClearSeletion()
    {
        // Think about how this method should behave

        if (selectedUnits.Count > 0)
        {
            foreach (ISelectable selected in selectedUnits)
            {
                // ?
                if (selected != null)
                {
                    selected.Deselect();
                }
            }

            selectedUnits.Clear();
        }
        // shoul i null it?
        selectionParent = null;
    }

    public GameObject GetActivePanel()
    {
        // validate?
        return activePanel;
    }

    public void OnDeath(ISelectable unit)
    {
        if (selectedUnits.Contains(unit))
        {
            ClearSeletion();
            activePanel.SetActive(false);
            activePanel = null;
        }
    }
}
