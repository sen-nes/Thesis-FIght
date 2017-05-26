using System;
using UnityEngine;

public struct PathRequest
{
    public Vector3 pathStart;
    public Vector3 pathTarget;
    public Action<Vector3[], bool> callback;

    public PathRequest(Vector3 _start, Vector3 _target, Action<Vector3[], bool> _callback)
    {
        pathStart = _start;
        pathTarget = _target;
        callback = _callback;
    }
}
