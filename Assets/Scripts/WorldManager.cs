using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    [Header("World Parameters")]
    public Vector2Int worldSize;

    [Header("Prefabs")]
    public TileController tilePrefab;

    TileController[,] tiles;

    public void Init()
    {
        GenerateWorld();
    }

    private void GenerateWorld()
    {
        tiles = new TileController[worldSize.x, worldSize.y];

        for (int y = 0; y < worldSize.y; y++)
        {
            for (int x = 0; x < worldSize.x; x++)
            {
                Vector3 position = new Vector3(x, 0f, y);
                TileController tile = tiles[x, y] = Instantiate(tilePrefab, position, Quaternion.identity, transform);

                tile.SetGroundType((TileGroundType)Random.Range(0, 3));
            }
        }
    }
}
