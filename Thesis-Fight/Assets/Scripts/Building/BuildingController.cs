using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour {

    public Building building;
    public Team teamID;
    public int playerID;
    public Vector3 enemyCastle;

    private void Awake()
    {
        building.Initiate(this.gameObject);
        this.GetComponent<Attackable>().teamID = teamID;
    }
}
