using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public Vector2Int worldSize;
    public TileController tilePrefab;

    private void Start()
    {
        SetupWorld();
    }

    private void SetupWorld()
    {
        float halfWidth = (worldSize.x - 1) / 2f;
        float halfHeight = (worldSize.y - 1) / 2f;

        for (int y = 0; y < worldSize.y; y++)
        {
            for (int x = 0; x < worldSize.x; x++)
            {
                Vector3 tilePosition = new Vector3(x - halfWidth, 0f, y - halfHeight);
                TileController tileController = Instantiate(tilePrefab, tilePosition, Quaternion.identity, transform);
            }
        }
    }
}
