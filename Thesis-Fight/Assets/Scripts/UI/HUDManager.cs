using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour {

    public GameObject builderPanel;
    public GameObject buildingPanel;
    public GameObject unitPanel;
    public GameObject castlePanel;

    private SelectionManager selectionManager;
    private GameObject activePanel;

    private void Start()
    {
        selectionManager = GameObject.FindObjectOfType<SelectionManager>();
    }

    private void Update()
    {
        if (selectionManager.selectedObject != null)
        {
            activePanel.SetActive(false);

            switch (selectionManager.selectedObject.tag)
            {
                case "Builder":
                    builderPanel.SetActive(true);
                    activePanel = builderPanel;
                    break;
                case "Building":
                    buildingPanel.SetActive(true);
                    activePanel = buildingPanel;
                    break;
                case "Unit":
                    unitPanel.SetActive(true);
                    activePanel = unitPanel;
                    break;
                case "Castle":
                    castlePanel.SetActive(true);
                    activePanel = castlePanel;
                    break;
                default:
                    activePanel = null;
                    break;
            }
        }
    }

    // Building production
}
