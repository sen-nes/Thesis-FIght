using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour {

    public bool selected;

    private MeshRenderer highlight;

    private void Start()
    {
        highlight = transform.Find("Highlight").GetComponent<MeshRenderer>();
    }

    public void Select()
    {
        selected = true;
        highlight.enabled = true;
    }

    public void Deselect()
    {
        selected = false;
        highlight.enabled = false;
    }
}
