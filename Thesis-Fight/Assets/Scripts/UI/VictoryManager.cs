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

    public bool gameOver;

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
        gameOver = false;
    }

    public void DeclareDefeat(int teamID)
    {
        victor = "Team East";
        if (teamID == (int)Teams.TEAM_EAST)
        {
            victor = "Team West";
        }

        victoryText.text = victor + " Wins";
        endScreen.SetActive(true);
        Time.timeScale = 0f;
        gameOver = true;
    }
}
