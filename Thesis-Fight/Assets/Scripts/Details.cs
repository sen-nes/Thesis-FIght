using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Details : MonoBehaviour
{

    public int teamID;
    public int ownerID;
    public bool selected;

    private void Update()
    {
        if (selected)
        {
            transform.Find("Selectable").GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            transform.Find("Selectable").GetComponent<MeshRenderer>().enabled = false;
        }
    }
}

public enum Team
{
    TEAM_EAST = 0,
    TEAM_WEST
}
