using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    public TileCoordinates coordinates;
    private Tile[] neighbors = new Tile[4];
    public TileAnimation animation;

    public float height;
    public bool isSelected;

    public Tile GetNeighbor(TileDirection direction)
    {
        return neighbors[(int)direction];
    }

    public Tile[] GetNeighbors()
    {
        return neighbors;
    }

    public void SetNeighbor(TileDirection direction, Tile neighbor)
    {
        neighbors[(int)direction] = neighbor;
    }
}
