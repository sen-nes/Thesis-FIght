using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryManager : MonoBehaviour {

    // Singleton is properly used here, or is it
    // won't static methods do the same job
    public static VictoryManager instance;

    public string victor;
    public GameObject endScreen;
    public Text victoryText;

    private void Awake()
    {
        if (instance != null)
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }

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
        for (int i = 0; i < units.childCount; i++)
        {
            Transform child = units.GetChild(i);

            child.GetComponent<UnitCombat>().enabled = false;
            child.GetComponent<UnitMovement>().enabled = false;
        }

        Transform buildings = GameObject.Find("Buildings").transform;
        for (int i = 0; i < buildings.childCount; i++)
        {
            Transform building = buildings.GetChild(i);

            // Stop spawning units
            building.GetComponent<BuildingController>().enabled = false;
            building.GetComponent<BuildingCombat>().enabled = false;
        }

        victoryText.text = victor + " Wins";
        endScreen.SetActive(true);
    }
}
