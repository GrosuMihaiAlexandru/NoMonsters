using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public bool onlyDisplayPathGizmos;

    public Transform player;

    public LayerMask walkableMask;

    public Vector2 gridWorldSize;
    public float nodeRadius;
    Node[,] grid;

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();

    }

    public int MaxSize
    {
        get { return gridSizeX * gridSizeY; }
    }

    // Creating the grid at the start of the game
    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        // Get the possition of the bottom left corner of the grid area
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.up * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
            for (int y = 0; y < gridSizeY; y++)
            {
                // Getting every point where a node will be
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
                RaycastHit2D hit = Physics2D.Raycast(new Vector2(worldPoint.x, worldPoint.y), Vector2.zero, 0, walkableMask);
                // Only walkable if it has the tag FixedBlock or Player
                bool walkable = hit ? (hit.transform.tag == "WalkableBlock"  || hit.transform.tag == "FixedBlock" || hit.transform.tag == "Player" || hit.transform.tag == "Collectible" ? true : false) : false;
                grid[x, y] = new Node(walkable, worldPoint, x, y);
            }
    }

    // Called whenever an IceBlock is snapped
    void UpdateGrid()
    {
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.up * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
                RaycastHit2D hit = Physics2D.Raycast(new Vector2(worldPoint.x, worldPoint.y), Vector2.zero, 0, walkableMask);
                bool walkable = hit ? (hit.transform.tag == "WalkableBlock" || hit.transform.tag == "FixedBlock" || hit.transform.tag == "Player" || hit.transform.tag == "Collectible" ? true : false) : false;
                grid[x, y] = new Node(walkable, worldPoint, x, y);
            }
    }

    void ResetWalkable()
    {
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.up * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
                RaycastHit2D hit = Physics2D.Raycast(new Vector2(worldPoint.x, worldPoint.y), Vector2.zero, 0, walkableMask);
                if (hit && hit.transform.tag == "WalkableBlock")
                    hit.transform.tag = "FixedBlock";
            }

    }

    // Return a list of neighbours
    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        int x = node.gridX;
        int y = node.gridY;
        // Checking in 4 directions (up, down, left, right)
        if (x - 1 >= 0)
            neighbours.Add(grid[x - 1, y]);
        if (x + 1 < gridSizeX)
            neighbours.Add(grid[x + 1, y]);
        if (y - 1 >= 0)
            neighbours.Add(grid[x, y - 1]);
        if (y + 1 < gridSizeY)
            neighbours.Add(grid[x, y + 1]);
              
        return neighbours;
    }

    // Getting the position of the player on the grid
    public Node NodeFromWorldPoint(Vector2 worldPos)
    {
        // Removing the offset from transforms position as the grid moves up
        worldPos.y -= transform.position.y;
        float percentX = (worldPos.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPos.y + gridWorldSize.y / 2) / gridWorldSize.y;
        // Clamping to avoid weird values for indices
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);
        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        // y is offset by 1???
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        return grid[x, y];
    }

    public List<Node> path;
    // Displayig the grid for Debugging
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 1));

        if (onlyDisplayPathGizmos)
        {
            if (path != null)
            {
                foreach (Node n in path)
                {
                    Gizmos.color = Color.black;
                    Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
                }
            }
        }
        else
        {
            if (grid != null)
            {
                Node playerNode = NodeFromWorldPoint(player.position);
                foreach (Node n in grid)
                {
                    Gizmos.color = (n.walkable) ? Color.white : Color.red;
                    if (playerNode == n)
                        Gizmos.color = Color.cyan;
                    if (path != null)
                        if (path.Contains(n))
                            Gizmos.color = Color.black;
                    Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
                }
            }
        }
    }
}
