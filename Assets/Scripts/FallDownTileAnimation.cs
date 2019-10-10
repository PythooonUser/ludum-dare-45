using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDownTileAnimation : TileAnimation
{
    private float finalHeight;
    private float startingHeight;
    private float animationDuration;
    private float timeSinceAnimationStart;

    public FallDownTileAnimation(Tile tile) : base(tile)
    {
        finalHeight = -10f;
        startingHeight = tile.height;
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
