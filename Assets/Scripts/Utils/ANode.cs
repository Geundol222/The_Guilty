using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANode
{
    public bool isWalkable;
    public Vector3 worldPos;
    public int gridX;
    public int gridY;

    public int gCost;
    public int hCost;
    public ANode parentNode;

    public ANode(bool isWalkable, Vector3 worldPos, int gridX, int gridY)
    {
        this.isWalkable = isWalkable;
        this.worldPos = worldPos;
        this.gridX = gridX;
        this.gridY = gridY;
    }

    public int fCost { get { return gCost + hCost; } }
    
}
