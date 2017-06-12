using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGrid : MonoBehaviour
{
    public int sizeX;
    public int sizeY;
    public GameObject tile;

    private Node[,] grid;
    private Transform gridParent;
    private GameObject[,] buildingGrid;

    private Vector3 offset = new Vector3(0f, 0.16f, 0f);

    private void Start()
    {
        buildingGrid = new GameObject[sizeX * 2 + 1, sizeY * 2 + 1];
        
        InstantiateGrid();
    }


    // What is the check in Update for?

    //private void Update()
    //{
    //    Node currentNode = Grid.instance.NodeFromPoint(transform.position);
    //    if (grid[sizeX, sizeY] != currentNode)
    //    {
    //        UpdateGrid();
    //    }
    //}

    private void InstantiateGrid()
    {
        gridParent = transform.Find("Grid");
        for (int x = 0; x < sizeX * 2 + 1; x++)
        {
            for (int y = 0; y < sizeY * 2 + 1; y++)
            {
                buildingGrid[x, y] = Instantiate(tile, gridParent.transform, false);
            }
        }

        gridParent.gameObject.SetActive(false);
    }

    public bool UpdateGrid(Transform tr)
    {
        // Consider placing the tiles in a grid beforehand and avoid setting the position of each one
        transform.position = tr.position;
        grid = Grid.instance.Subgrid(sizeX, sizeY, tr.position);

        for (int x = 0; x < sizeX * 2 + 1; x++)
        {
            for (int y = 0; y < sizeY * 2 + 1; y++)
            {
                if (grid[x, y] != null && !grid[x, y].walkable)
                {
                    buildingGrid[x, y].SetActive(true);
                    // buildingGrid[x, y].transform.position = grid[x, y].position + offset;
                    buildingGrid[x, y].GetComponent<MeshRenderer>().material.color = Color.grey;

                }
                else
                {
                    buildingGrid[x, y].SetActive(false);
                }
            }
        }

        // Extents or size
        return CheckPlacement(tr.GetComponent<Collider>().bounds.extents);
    }

    private bool CheckPlacement(Vector3 extents)
    {
        bool canBuild = true;

        float tmp = extents.x;
        int X = (int)(tmp / 2);
        if ((int)tmp % 2 == 1)
        {
            if (tmp % 1.0f > 0)
            {
                X++;
            }
        }

        tmp = extents.z;
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
                    buildingGrid[checkX, checkY].SetActive(true);
                    buildingGrid[checkX, checkY].transform.position = grid[checkX, checkY].position + offset;

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
                    buildingGrid[checkX, checkY].GetComponent<MeshRenderer>().material.color = buildable;
                }
                else
                {
                    canBuild = false;
                }
            }
        }

        return canBuild;
    }

    public void Show()
    {
        gridParent.gameObject.SetActive(true);
    }

    public void Hide()
    {
        gridParent.gameObject.SetActive(false);
    }
}