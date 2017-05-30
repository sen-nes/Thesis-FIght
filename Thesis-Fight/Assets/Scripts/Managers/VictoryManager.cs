using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryManager : MonoBehaviour {

    public static VictoryManager instance;

    public string victor;

    private void Awake()
    {
        instance = this;
        victor = "None";
    }

    public void DeclareDefeat(int teamID)
    {
        victor = "Team East";
        if (teamID == (int)Team.TEAM_EAST)
        {
            victor = "Team West";
        }

        Transform units = GameObject.Find("Units").transform;
        for(int i = 0; i < units.childCount; i++)
        {
            Transform child = units.GetChild(i);
            child.GetComponent<UnitCombat>().enabled = false;
            child.GetComponent<UnitMovement>().enabled = false;
        }

        Transform buildings = GameObject.Find("Buildings").transform;
        for (int i = 0; i < buildings.childCount; i++)
        {
            Transform building = buildings.GetChild(i);
            building.GetComponent<IBuilding>().StopSpawning();
            building.GetComponent<BuildingCombat>().enabled = false;
            building.GetComponent<BuildingController>().enabled = false;
        }
    }
}
