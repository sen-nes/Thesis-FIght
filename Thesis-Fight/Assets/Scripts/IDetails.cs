using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Teams
{
    TEAM_EAST = 0,
    TEAM_WEST
}

public interface IDetails
{
    int TeamID { get; set; }
}
