﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleController : MonoBehaviour {

    public Castle castle;
    public Teams teamID;

    private void Awake()
    {
        castle.Initiate(this.gameObject);
        GetComponent<Attackable>().teamID = teamID;
    }
}
