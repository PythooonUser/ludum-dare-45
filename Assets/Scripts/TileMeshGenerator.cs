using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMeshGenerator : MonoBehaviour
{
    [Header("Tile Mesh Parameters")]
    public Vector2 tileSize = Vector3.one;
    [SerializeField] private Vector2 tileInset = new Vector2(0.15f, 0.15f);

    [Header("References")]
    [SerializeField] private WorldManager worldManager;

    private TileMesh tileMesh = new TileMesh();

    public Mesh GenerateMesh(Tile[,] tiles)
    {
        return tileMesh.GenerateMesh(tiles, tileSize, tileInset);
    }

    private void Update()
    {
        Tile[,] tiles = worldManager.tiles;
        bool needsUpdating = false;

        for (int y = 0; y < tiles.GetLength(1); y++)
        {
            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                Tile tile = tiles[x, y];

                if (tile.animation != null && tile.animation.isRunning)
                {
                    tile.animation.Tick(Time.deltaTime);
                    needsUpdating = true;
                }
            }
        }

        if (needsUpdating)
        {
            Mesh mesh = GenerateMesh(tiles);
            worldManager.UpdateMesh(mesh);
        }
    }
}
