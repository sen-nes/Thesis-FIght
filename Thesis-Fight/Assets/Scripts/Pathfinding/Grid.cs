using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public static Grid instance;

    public bool drawGizmos;
    public Vector2 gridSize;
    public float nodeSize;

    private int unwalkableMask;
    private int notBuildableMask;
    private Node[,] grid;
    private float nodeRadius;
    private float radiusOffset;
    private int nodeCountX;
    private int nodeCountY;

    public int NodeCount
    {
        get
        {
            return nodeCountX * nodeCountY;
        }
    }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Instance is already instantiated!");
        }
        instance = this;

        unwalkableMask = LayerMask.GetMask("Unwalkable");
        notBuildableMask = LayerMask.GetMask("NotBuildable");
        nodeRadius = nodeSize / 2;
        radiusOffset = 0.01f;
        nodeCountX = Mathf.RoundToInt(gridSize.x / nodeSize);
        nodeCountY = Mathf.RoundToInt(gridSize.y / nodeSize);
        CreateGrid();
    }

    private void CreateGrid()
    {
        grid = new Node[nodeCountX, nodeCountY];
        Vector3 bottomLeft = transform.position - Vector3.right * gridSize.x / 2 - Vector3.forward * gridSize.y / 2;

        for (int x = 0; x < nodeCountX; x++)
        {
            for (int y = 0; y < nodeCountY; y++)
            {
                Vector3 point = bottomLeft + Vector3.right * (x * nodeSize + nodeRadius) + Vector3.forward * (y * nodeSize + nodeRadius);
                bool walkable = !(Physics.CheckSphere(point, nodeRadius - radiusOffset, unwalkableMask));
                bool buildable = !(Physics.CheckSphere(point, nodeRadius - radiusOffset, notBuildableMask));
                grid[x, y] = new Node(walkable, buildable, point, x, y);
            }
        }
    }

    private void UpdateGrid()
    {
        foreach (Node node in grid)
        {
            bool walkable = !(Physics.CheckSphere(node.position, nodeRadius - radiusOffset, unwalkableMask));
            node.walkable = walkable;
        }
    }

    public Node NodeFromPoint(Vector3 point)
    {
        float percentX = Mathf.Clamp01((point.x + gridSize.x / 2) / gridSize.x);
        float percentY = Mathf.Clamp01((point.z + gridSize.y / 2) / gridSize.y);

        int x = Mathf.RoundToInt((nodeCountX - 1) * percentX);
        int y = Mathf.RoundToInt((nodeCountY - 1) * percentY);

        return grid[x, y];
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                {
                    continue;
                }

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < nodeCountX && checkY >= 0 && checkY < nodeCountY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }
    
    public Node[,] Subgrid(int X, int Y, Vector3 pos)
    {
        Node[,] subGrid = new Node[X * 2 + 1, Y * 2 + 1];
        Node centerNode = NodeFromPoint(pos);

        for (int x = -X; x <= X; x++)
        {
            for (int y = -Y; y <= Y; y++)
            {
                int checkX = centerNode.gridX + x;
                int checkY = centerNode.gridY + y;

                if (checkX >= 0 && checkX < nodeCountX && checkY >= 0 && checkY < nodeCountY)
                {
                    subGrid[x + X, y + Y] = grid[checkX, checkY];
                }
                else
                {
                    subGrid[x + X, y + Y] = null;
                }
                
            }
        }

        return subGrid;
    }

    public Node[,] GetTeamArea(Teams team)
    {
        int length = 60;
        int X = 0;
        if (team == Teams.TEAM_EAST)
        {
            X = (int)gridSize.x - length;
        }
        
        Node[,] subGrid = new Node[length, (int)gridSize.y];


        for (int y = 0; y < (int)gridSize.y; y++)
        {
            for (int x = X; x < X + length; x++)
            {
                subGrid[x - X, y] = grid[x, y];
            }
        }


        return subGrid;
    }

    public void UpdateGridRegion(int X, int Y, Vector3 pos)
    {
        Node placementNode = NodeFromPoint(pos);

        for (int x = -X; x <= X; x++)
        {
            for (int y = -Y; y <= Y; y++)
            {

                int checkX = placementNode.gridX + x;
                int checkY = placementNode.gridY + y;

                if (checkX >= 0 && checkX < nodeCountX && checkY >= 0 && checkY < nodeCountY)
                {
                    Node node = grid[checkX, checkY];

                    bool walkable = !(Physics.CheckSphere(node.position, nodeRadius - radiusOffset, unwalkableMask));
                    node.walkable = walkable;
                }

                
            }
        }
    }

    public bool CheckGridRegion(int X, int Y, Vector3 pos)
    {
        Node placementNode = NodeFromPoint(pos);

        for (int x = -X; x <= X; x++)
        {
            for (int y = -Y; y <= Y; y++)
            {
                int neighX = placementNode.gridX + x;
                int neighY = placementNode.gridY + y;

                if (neighX >= 0 && neighX < nodeCountX && neighY >= 0 && neighY < nodeCountY)
                {
                    if (!grid[neighX, neighY].walkable || !grid[neighX, neighY].buildable)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
                
            }
        }

        return true;
    }

    public Vector3 FindFreeNode(Teams team, Vector3 pos)
    {
        Node startingNode = NodeFromPoint(pos);
        Node[,] spawningArea = GetTeamArea(team);
        int length = spawningArea.GetLength(1);

        int Y = startingNode.gridY;
        int X = startingNode.gridX;
        if (X >= 290) {
            X -= 290;
        }
        
        for (int c = 1; c <= length / 2; c++)
        {
            for (int x = -c; x <= c; x++)
            {
                int checkX = X + x;
                int checkY = Y + c;

                if (checkX >= 0 && checkX < nodeCountX && checkY >= 0 && checkY < nodeCountY)
                {
                    if (spawningArea[checkX, checkY].walkable)
                    {
                        return spawningArea[checkX, checkY].position;
                    }
                }

                checkX = X + x;
                checkY = Y - c;

                if (checkX >= 0 && checkX < nodeCountX && checkY >= 0 && checkY < nodeCountY)
                {
                    if (spawningArea[checkX, checkY].walkable)
                    {
                        return spawningArea[checkX, checkY].position;
                    }
                }
            }

            for (int y = -c; y <= c; y++)
            {
                int checkX = X + c;
                int checkY = Y + y;

                if (checkX >= 0 && checkX < nodeCountX && checkY >= 0 && checkY < nodeCountY)
                {
                    if (spawningArea[checkX, checkY].walkable)
                    {
                        return spawningArea[checkX, checkY].position;
                    }
                }

                checkX = X - c;
                checkY = Y + y;

                if (checkX >= 0 && checkX < nodeCountX && checkY >= 0 && checkY < nodeCountY)
                {
                    if (spawningArea[checkX, checkY].walkable)
                    {
                        return spawningArea[checkX, checkY].position;
                    }
                }
            }
        }

        return Vector3.zero;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridSize.x, 1f, gridSize.y));
        if (grid != null && drawGizmos)
        {
            foreach (Node node in grid)
            {
                Gizmos.color = (node.walkable ? Color.green : Color.red);

                Gizmos.DrawWireCube(node.position, Vector3.one * (nodeSize - .1f));
            }
        }
    }
}