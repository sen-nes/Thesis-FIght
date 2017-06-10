using System;
using System.Collections.Generic;
using UnityEngine;

public class StackPath {

    public Vector3 Next
    {
        get
        {
            return nodes.Peek();
        }
    }

    public Vector3 Last
    {
        get
        {
            return nodes.ToArray()[Length - 1];
        }
    }

    public int Length
    {
        get
        {
            return nodes.Count;
        }
    }

    public Vector3[] pathNodes
    {
        get
        {
            return nodes.ToArray();
        }
    }

    private Stack<Vector3> nodes;

    public StackPath(Vector3[] _nodes)
    {
        this.nodes = new Stack<Vector3>(_nodes);
    }

    public Vector3 GetTargetPosition(Vector3 position, float turnRadius)
    {
        if (nodes.Count == 1)
        {
            return nodes.Peek();
        }
        
        if (Vector3.Distance(position, nodes.Peek()) < turnRadius)
        {
            nodes.Pop();
        }

        return nodes.Peek();
    }
}
