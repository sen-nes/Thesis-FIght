using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSelectable : MonoBehaviour, ISelectable {

    public bool Selected { get; set; }

    private MeshRenderer highlight;
    private UnitStats stats;
    private UnitCombat combat;
    private GameObject panel;

    private void Start()
    {
        highlight = transform.Find("Highlight").GetComponent<MeshRenderer>();
        stats = transform.parent.GetComponent<UnitStats>();
        combat = transform.parent.GetComponent<UnitCombat>();
    }

    public void Select()
    {
        Selected = true;
        highlight.enabled = true;
        panel = SelectionManager.instance.GetActivePanel();
        StartCoroutine("UpdateUI");
    }

    public void Deselect()
    {
        Selected = false;
        highlight.enabled = false;
        panel = null;
        StopCoroutine("UpdateUI");
    }

    // Enumerable?
    public IEnumerator UpdateUI()
    {
        yield return null;
    }
}
