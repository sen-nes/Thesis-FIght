using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IHeapNode<Node>
{
    public bool walkable;
    public bool buildable;
    public Vector3 position;
    public int gridX;
    public int gridY;

    public int gCost;
    public int hCost;

    public Node parentNode;

    // private int heapIndex;

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }

    public int HeapIndex { get; set; }

    public Node(bool _walkable, bool _buildable, Vector3 _position, int _gridX, int _gridY)
    {
        walkable = _walkable;
        buildable = _buildable;
        position = _position;
        gridX = _gridX;
        gridY = _gridY;
    }

    public int CompareTo(Node other)
    {
        int compare = fCost.CompareTo(other.fCost);
        if (compare == 0)
        {
            compare = hCost.CompareTo(other.hCost);
        }

        return compare;
    }
}
