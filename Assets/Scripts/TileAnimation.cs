using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileAnimation
{
    protected Tile tile;
    public bool isRunning;

    protected TileAnimation(Tile tile)
    {
        this.tile = tile;
        isRunning = true;
    }

    public virtual void Tick(float deltaTime)
    {
        throw new NotImplementedException();
    }
}
