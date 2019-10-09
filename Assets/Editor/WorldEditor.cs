using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WorldManager))]
public class WorldEditor : Editor
{
    void OnSceneGUI()
    {
        WorldManager worldManager = target as WorldManager;
        if (worldManager == null) { return; }

        if (worldManager.tiles != null)
        {
            Vector2 tileSize = worldManager.tileMeshGenerator.tileSize;

            for (int y = 0; y < worldManager.tiles.GetLength(1); y++)
            {
                for (int x = 0; x < worldManager.tiles.GetLength(0); x++)
                {
                    Tile tile = worldManager.tiles[x, y];

                    Handles.Label(
                        new Vector3(tile.coordinates.x * tileSize.x, tile.height, tile.coordinates.y * tileSize.y),
                        tile.coordinates.ToString()
                    );
                }
            }
        }
    }
}
