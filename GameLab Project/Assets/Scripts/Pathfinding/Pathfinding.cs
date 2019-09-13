using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    // The player
    public Transform seeker;
    // The destination
    public Transform target;

    Grid grid;

    private void Awake()
    {
        grid = GetComponent<Grid>();
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, seeker.position) >= 15)
            transform.position += new Vector3(transform.position.x, transform.position.y + 15, transform.position.z);
        //FindPath(seeker.position, target.position);
    }

    void BreadthFirstSearch()
    {
        Node startNode = grid.NodeFromWorldPoint(seeker.position);
        startNode.yDistance = 0;
        // set of nodes to be evaluated
        Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
        // set of nodes already evaluated
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        int maxYDistance = 0;
        Node maxNode = startNode;

        // Checking all the walkable nodes
        while (openSet.Count > 0)
        {
            Node currentNode = openSet.RemoveFirst();
            closedSet.Add(currentNode);

            foreach (Node neighbour in grid.GetNeighbours(currentNode))
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour))
                    continue;

                if (neighbour.gridY > maxYDistance)
                {
                    maxYDistance = neighbour.gridY;
                    maxNode = neighbour;
                }

                neighbour.parent = currentNode;
                if (!openSet.Contains(neighbour))
                    openSet.Add(neighbour);
            }
        }

        RetracePath(startNode, maxNode);
        Debug.Log(maxNode.worldPosition);

    }

    void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();
        seeker.GetComponent<CharacterController>().SetPath(path);
    }

    // A* pathfinding
    void FindPath(Vector2 startPos, Vector2 targetPos)
    {
        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);

        Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet.RemoveFirst();
            
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                RetracePath(startNode, targetNode);
                return;
            }
            foreach (Node neighbour in grid.GetNeighbours(currentNode))
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour))
                    continue;

                int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                if (newMovementCostToNeighbour < neighbour.gCost || ! openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }

        }
    }

    // Calculating Manhattan Distance (Heuristic for A*)
    int GetDistance (Node nodeA, Node nodeB)
    {
        return Mathf.Abs(nodeA.gridX - nodeB.gridX) + Mathf.Abs(nodeA.gridY - nodeB.gridY);
    }
}
