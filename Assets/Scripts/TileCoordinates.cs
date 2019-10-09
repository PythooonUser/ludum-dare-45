using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCoordinates
{
    public int x { get; private set; }
    public int y { get; private set; }

    public TileCoordinates(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public override string ToString()
    {
        return x.ToString() + " : " + y.ToString();
    }
}
