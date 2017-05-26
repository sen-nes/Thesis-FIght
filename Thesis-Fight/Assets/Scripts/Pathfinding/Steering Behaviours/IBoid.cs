using System.Collections.Generic;
using UnityEngine;

public interface IBoid
{
    Vector3 Velocity { get; set; }
    Vector3 Position { get; set; }
    float Speed { get; set; }
    float Mass { get; set; }
    float SlowDownRadius { get; set; }
    float Force { get; set; }
    //Path UnitPath { get; set; }
    Stack<Vector3> Path { get; set; }
    // avoid force
}