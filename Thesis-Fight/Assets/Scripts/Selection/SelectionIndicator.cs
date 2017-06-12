using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionIndicator : MonoBehaviour {

    private SelectionManager selectionManager;

    private void Start()
    {
        // Ensure it's the right one
        selectionManager = GameObject.FindObjectOfType<SelectionManager>();
    }

    private void Update()
    {
        if (selectionManager.selectedObject != null)
        {
            transform.Find("Indicator").gameObject.SetActive(true);

            Collider collider = selectionManager.selectedObject.GetComponent<Collider>();
            Bounds bounds = collider.bounds;
            float diameter = Mathf.Sqrt(bounds.size.x * bounds.size.x + bounds.size.z * bounds.size.z);
            diameter *= 1.6f;

            this.transform.localScale = new Vector3(diameter, 1f, diameter);
            
            // Find out why indicator is changing in size
            this.transform.position = selectionManager.selectedObject.transform.position;
        }
        else
        {
            transform.Find("Indicator").gameObject.SetActive(false);
        }
    }
}
