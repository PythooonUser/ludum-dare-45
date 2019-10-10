using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiseUpTileAnimation : TileAnimation
{
    private float finalHeight;
    private float startingHeight;
    private float animationDuration;
    private float timeSinceAnimationStart;

    public RiseUpTileAnimation(Tile tile) : base(tile)
    {
        finalHeight = tile.height;
        startingHeight = -10f;
        animationDuration = 2f;
        timeSinceAnimationStart = 0f;
    }

    public override void Tick(float deltaTime)
    {
        if (isRunning)
        {
            timeSinceAnimationStart += deltaTime;

            if (timeSinceAnimationStart > animationDuration)
            {
                isRunning = false;
                return;
            }

            tile.height = Mathf.Lerp(startingHeight, finalHeight, timeSinceAnimationStart / animationDuration);
        }
    }
}
