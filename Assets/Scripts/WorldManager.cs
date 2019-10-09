using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    [Header("References")]
    public TileMeshGenerator tileMeshGenerator;
    public CameraController cameraController;

    [HideInInspector]
    public Tile[,] tiles;

    private void Start()
    {
        cameraController.OnTileSelected += OnTileSelected;
        cameraController.OnTileDeselected += OnTileDeselected;
    }

    public Tile GetTileByWorldPosition(Vector3 position)
    {
        Vector2 tileSize = tileMeshGenerator.tileSize;

        float xPosition = position.x;
        float zPosition = position.z;

        int iX = 0;
        int iXFinal = 0;
        int iZ = 0;
        int iZFinal = 0;

        for (float x = -tileSize.x * 0.5f; x < tiles.GetLength(0) * tileSize.x; x += tileSize.x, iX++)
        {
            if (xPosition < x)
            {
                iXFinal = iX;
                break;
            }
        }

        for (float z = -tileSize.y * 0.5f; z < tiles.GetLength(0) * tileSize.y; z += tileSize.y, iZ++)
        {
            if (zPosition < z)
            {
                iZFinal = iZ;
                break;
            }
        }

        return tiles[iX - 1, iZ - 1];
    }

    private void OnTileSelected(Tile tile)
    {
        tile.isSelected = true;
        RedrawMesh();
    }

    private void OnTileDeselected(Tile tile)
    {
        tile.isSelected = false;
        RedrawMesh();
    }

    private void RedrawMesh()
    {
        Mesh mesh = tileMeshGenerator.GenerateMesh(tiles);

        Transform tileMesh = transform.GetChild(0);
        tileMesh.GetComponent<MeshFilter>().mesh = mesh;
        tileMesh.GetComponent<MeshCollider>().sharedMesh = mesh;
    }
}
