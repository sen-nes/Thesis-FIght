using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.Collections;
using System;

public class Pathfinder : MonoBehaviour
{
    public void FindPath(PathRequest request, Action<PathResult> callback)
    {
        Vector3[] waypoints = new Vector3[0];
        bool pathFound = false;

        Node startNode = Grid.instance.NodeFromPoint(request.pathStart);
        Node targetNode = Grid.instance.NodeFromPoint(request.pathTarget);
        if (/*startNode.walkable &&*/ targetNode.walkable)
        {
            Heap<Node> open = new Heap<Node>(Grid.instance.NodeCount);
            HashSet<Node> closed = new HashSet<Node>();

            open.Add(startNode);
            while (open.Count > 0)
            {
                Node currentNode = open.RemoveTop();
                closed.Add(currentNode);

                if (currentNode == targetNode)
                {
                    pathFound = true;
                    break;
                }

                foreach (Node neighbour in Grid.instance.GetNeighbours(currentNode))
                {
                    if (!neighbour.walkable || closed.Contains(neighbour))
                    {
                        continue;
                    }

                    int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                    if (newMovementCostToNeighbour < neighbour.gCost || !open.Contains(neighbour))
                    {
                        neighbour.gCost = newMovementCostToNeighbour;
                        neighbour.hCost = GetDistance(neighbour, targetNode);
                        neighbour.parentNode = currentNode;

                        if (!open.Contains(neighbour))
                        {
                            open.Add(neighbour);
                        }
                    }
                }
            }
        }

        if (pathFound)
        {
            waypoints = Retrace(startNode, targetNode);
            pathFound = waypoints.Length > 0;
        }
        callback(new PathResult(waypoints, pathFound, request.callback));
    }

    private int GetDistance(Node nodeA, Node nodeB)
    {
        int distanceX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distanceY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (distanceX > distanceY)
        {
            return 14 * distanceY + 10 * (distanceX - distanceY);
        }
        else
        {
            return 14 * distanceX + 10 * (distanceY - distanceX);
        }
    }

    private Vector3[] Retrace(Node startNode, Node targetNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = targetNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parentNode;
        }

        Vector3[] waypoints = new Vector3[0];
        if (path.Count > 0)
        {
            waypoints = GenerateWaypoints(path);
        }

        Array.Reverse(waypoints);

        return waypoints;
    }

    private Vector3[] GenerateWaypoints(List<Node> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 direction = Vector2.zero;

        waypoints.Add(path[0].position);
        for (int i = 1; i < path.Count; i++)
        {
            float horizontalDirection = path[i - 1].gridX - path[i].gridX;
            float verticalDirection = path[i - 1].gridY - path[i].gridY;
            Vector2 newDirection = new Vector2(horizontalDirection, verticalDirection);

            if (direction != newDirection)
            {
                direction = newDirection;
                waypoints.Add(path[i].position);
            }
            direction = newDirection;
        }

        return waypoints.ToArray();
    }
}
