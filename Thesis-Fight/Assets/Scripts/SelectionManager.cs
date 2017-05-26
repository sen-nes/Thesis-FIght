using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{

    // public GameObject selectionPlane;

    // Check instance technique
    private static SelectionManager instance;
    private List<Transform> selectedUnits;
    private int selectableMask;

    private void Start()
    {
        instance = this;
        selectableMask = LayerMask.GetMask("Selectable");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown((int)MouseButton.MB_LEFT))
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
            Transform selected = rayHit.transform;
            selectedUnits.Add(selected);

            // Create update selection method
            selected.GetComponent<Details>().selected = true;
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
            foreach (Transform selected in selectedUnits)
            {
                if (selected != null)
                {
                    selected.GetComponent<Details>().selected = false;
                }
            }
        }

        selectedUnits = new List<Transform>();
    }
}
