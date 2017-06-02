using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour {

    public static StateManager instance;
    public bool IsBuilding { get; set; }
    public bool IsMenuOpen { get; set; }

    private void Awake()
    {
        instance = this;
        IsBuilding = false;
        IsMenuOpen = false;
    }
}
