using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementGrid : MonoBehaviour
{

    public int sizeX;
    public int sizeY;
    public GameObject tile;

    public bool canBuild;

    // nec?
    private Node[,] grid;
    private GameObject[,] placementGrid;
    private GameObject gridParent;
    private Vector3 offset = new Vector3(0.0f, 0.01f, 0.0f);
    private BuildingController buildingController;

    private void Start()
    {
        grid = Grid.instance.Subgrid(sizeX, sizeY, transform.position);
        placementGrid = new GameObject[sizeX * 2 + 1, sizeY * 2 + 1];
        tile = Resources.Load<GameObject>("Tile");
        buildingController = GetComponent<BuildingController>();
        canBuild = true;

        InstantiateGrid();
        UpdateGrid();
    }

    private void Update()
    {
        Node currentNode = Grid.instance.NodeFromPoint(transform.position);
        if (grid[sizeX, sizeY] != currentNode)
        {
            UpdateGrid();
        }
    }

    private void InstantiateGrid()
    {
        gridParent = new GameObject("Placement grid");
        gridParent.transform.SetParent(transform);
        for (int x = 0; x < sizeX * 2 + 1; x++)
        {
            for (int y = 0; y < sizeY * 2 + 1; y++)
            {
                placementGrid[x, y] = Instantiate(tile, gridParent.transform);
            }
        }
    }

    private void UpdateGrid()
    {
        grid = Grid.instance.Subgrid(sizeX, sizeY, transform.position);
        canBuild = true;

        for (int x = 0; x < sizeX * 2 + 1; x++)
        {
            for (int y = 0; y < sizeY * 2 + 1; y++)
            {
                if (grid[x, y] != null && !grid[x, y].walkable)
                {
                    placementGrid[x, y].SetActive(true);
                    placementGrid[x, y].transform.position = grid[x, y].position + offset;
                    placementGrid[x, y].GetComponent<MeshRenderer>().material.color = Color.grey;

                }
                else
                {
                    placementGrid[x, y].SetActive(false);
                }
            }
        }

        CheckPlacement();
    }

    private void CheckPlacement()
    {
        float tmp = buildingController.bounds.x;
        int X = (int)(tmp / 2);
        if ((int)tmp % 2 == 1)
        {
            if (tmp % 1.0f > 0)
            {
                X++;
            }
        }

        tmp = buildingController.bounds.z;
        int Y = (int)(tmp / 2);
        if ((int)tmp % 2 == 1)
        {
            if (tmp % 1.0f > 0)
            {
                Y++;
            }
        }

        for (int x = -X; x <= X; x++)
        {
            for (int y = -Y; y <= Y; y++)
            {
                int checkX = sizeX - x;
                int checkY = sizeY - y;

                if (grid[checkX, checkY] != null)
                {
                    placementGrid[checkX, checkY].SetActive(true);
                    placementGrid[checkX, checkY].transform.position = grid[checkX, checkY].position + offset;

                    Color buildable;
                    if (grid[checkX, checkY].walkable)
                    {
                        buildable = Color.green;
                    }
                    else
                    {
                        buildable = Color.red;
                        canBuild = false;
                    }
                    placementGrid[checkX, checkY].GetComponent<MeshRenderer>().material.color = buildable;
                }
                else
                {
                    canBuild = false;
                }
            }
        }
    }

    public void Show()
    {
        gridParent.SetActive(true);
    }

    public void Hide()
    {
        gridParent.SetActive(false);
    }
}