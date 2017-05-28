using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementGrid : MonoBehaviour
{

    public int sizeX;
    public int sizeY;
    public GameObject tile;

    // nec?
    private Node[,] grid;
    private GameObject[,] placementGrid;
    private Transform gridParent;
    private Vector3 offset = new Vector3(0.0f, 0.01f, 0.0f);

    private void Start()
    {
        grid = Grid.instance.Subgrid(sizeX, sizeY, transform.position);
        placementGrid = new GameObject[sizeX * 2 + 1, sizeY * 2 + 1];
        gridParent = GameObject.Find("Placement Grid").transform;

        InstantiateGrid();
        gameObject.SetActive(false);
    }

    private void Update()
    {
        Node currentNode = Grid.instance.NodeFromPoint(transform.position);
        if (grid[sizeX, sizeY] != currentNode)
        {
            Debug.Log("Changed position");
            UpdateGrid();
        }
    }

    private void InstantiateGrid()
    {
        for (int x = 0; x < sizeX * 2 + 1; x++)
        {
            for (int y = 0; y < sizeY * 2 + 1; y++)
            {
                placementGrid[x, y] = Instantiate(tile, gridParent);
            }
        }
    }

    private void UpdateGrid()
    {
        grid = Grid.instance.Subgrid(sizeX, sizeY, transform.position);

        for (int x = 0; x < sizeX * 2 + 1; x++)
        {
            for (int y = 0; y < sizeY * 2 + 1; y++)
            {
                Color tileColor = grid[x, y].walkable ? Color.green : Color.red;
                placementGrid[x, y].transform.position = grid[x, y].position + offset;
                placementGrid[x, y].GetComponent<MeshRenderer>().material.color = tileColor;
            }
        }
    }
}