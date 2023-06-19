using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    AGrid grid;

    public Transform StartObject;
    public Transform TargetObject;

    private void Awake()
    {
        grid = GetComponent<AGrid>();
    }

    private void Update()
    {
        FindPath(StartObject.position, TargetObject.position);
    }

    private void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        ANode startNode = grid.GetNodeFromWorldPoint(startPos);
        ANode targetNode = grid.GetNodeFromWorldPoint(targetPos);

        List<ANode> openList = new List<ANode>();
        HashSet<ANode> closedList = new HashSet<ANode>();
        openList.Add(startNode);

        while (openList.Count > 0)
        {
            ANode currentNode = openList[0];

            for (int i = 1; i < openList.Count; i++)
            {
                if (openList[i].fCost < currentNode.fCost || openList[i].fCost == currentNode.fCost && openList[i].hCost < currentNode.hCost)
                {
                    currentNode = openList[i];
                }
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if (currentNode == targetNode)
            {
                RetracePath(startNode, targetNode);
                return;
            }

            foreach (ANode node in grid.GetNeighbors(currentNode))
            {
                if (!node.isWalkable || closedList.Contains(node))
                    continue;

                ////int newCurrentToNeighbourCost = currentNode.gCost + GetDistanceCost(currentNode, node);
                //if (newCurrentToNeighbourCost < node.gCost || !openList.Contains(node))
                //{
                //    node.gCost = newCurrentToNeighbourCost;
                //    //node.hCost = GetDistanceCost(node, targetNode);
                //    node.parentNode = currentNode;
                //
                //    if (!openList.Contains(node))
                //        openList.Add(node);
                //}
            }
        }
    }

    private void RetracePath(ANode startNode, ANode endNode)
    {
        List<ANode> path = new List<ANode>();
        ANode currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
        }
    }

    private void RetracePath()
    {

    }
}
