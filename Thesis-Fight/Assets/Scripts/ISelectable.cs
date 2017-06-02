using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelectable{
    bool Selected { get; set; }
    void Select();
    void Deselect();
    IEnumerator UpdateUI();
}
