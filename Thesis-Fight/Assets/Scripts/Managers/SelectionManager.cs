using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager instance;
    public bool IsBuilding { get; set; }
    // public GameObject selectionPlane;

    private int selectableMask;
    private List<Selectable> selectedUnits;
    

    private void Start()
    {
        instance = this;
        selectableMask = LayerMask.GetMask("Selectable");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown((int)MouseButton.MB_LEFT) && !IsBuilding)
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
            Selectable selected = rayHit.collider.GetComponent<Selectable>();

            // Create update selection method
            selected.GetComponent<Selectable>().Select();
            selectedUnits.Add(selected);
        }
        else
        {
            ClearSeletion();
            selectedUnits = null;
        }
    }

    private void ClearSeletion()
    {
        // Think about how this method should behave

        if (selectedUnits != null)
        {
            foreach (Selectable selected in selectedUnits)
            {
                if (selected != null)
                {
                    selected.Deselect();
                }
            }
        }

        selectedUnits = new List<Selectable>();
    }
}
