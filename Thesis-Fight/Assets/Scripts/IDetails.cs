using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Team
{
    TEAM_EAST = 0,
    TEAM_WEST
}

public interface IDetails
{
    int TeamID { get; set; }
}
