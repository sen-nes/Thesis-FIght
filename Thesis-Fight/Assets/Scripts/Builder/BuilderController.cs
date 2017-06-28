using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderController : MonoBehaviour
{

    public Teams teamID;
    public int playerID;
    public Vector3 enemyCastle;
    public float flashRange = 10f;

    private SelectionManager selectionManager;

    private void Awake()
    {
        selectionManager = FindObjectOfType<SelectionManager>();
    }

    private void Update()
    {
        if (selectionManager.selectedObject == gameObject && !InGameMenuManager.instance.isOpen && Input.GetKeyUp(KeyCode.F))
        {
            if (!GetComponent<Build>().isBuilding)
            {
                Flash();
            }
        }
    }

    private void Flash()
    {
        Debug.Log("Flash");
        Node targetPoint = Grid.instance.NodeFromPoint(Helpers.RaycastFloor());
        
        if(targetPoint.walkable)
        {
            GetComponent<ClickAndMove>().CancelPath();
            GetComponent<Build>().CancelBuildingTask();
            float distance = Vector3.Distance(transform.position, targetPoint.position);

            if (distance <= flashRange)
            {
                transform.position = targetPoint.position;
            }
        }
    }
}