using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldManager : MonoBehaviour
{
    [Header("References")]
    public TileCreator tileCreatorPrefab;
    public TileMeshGenerator tileMeshGenerator;
    public CameraController cameraController;
    public Text selectedTileCoordinatesText;

    [HideInInspector]
    public Tile[,] tiles;
    private Dictionary<Tile, TileCreator> tileCreators = new Dictionary<Tile, TileCreator>();

    private void Start()
    {
        cameraController.OnTileSelected += OnTileSelected;
        cameraController.OnTileDeselected += OnTileDeselected;
        cameraController.OnTileClick += OnTileClick;

        selectedTileCoordinatesText.text = "";
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
        UpdateWorld();
        selectedTileCoordinatesText.text = tile.coordinates.ToString();
    }

    private void OnTileDeselected(Tile tile)
    {
        tile.isSelected = false;
        UpdateWorld();
        selectedTileCoordinatesText.text = "";
    }

    private void OnTileClick(Tile tile)
    {
        // if (tile.animation != null && tile.animation.isRunning)
        // {
        //     return;
        // }

        // tile.animation = new FallDownTileAnimation(tile);
    }

    private void OnTileCreated(TileCreator tileCreator)
    {
        Tile tile = tileCreator.tile;
        tile.isActive = true;

        tile.animation = new RiseUpTileAnimation(tile);
        tile.animation.OnAnimationFinished += OnTileRiseUpAnimationFinished;

        UpdateWorld();
    }

    private void OnTileRiseUpAnimationFinished(TileAnimation animation)
    {
        animation.OnAnimationFinished -= OnTileRiseUpAnimationFinished;
        UpdateTileCreators();
    }

    private void RedrawMesh()
    {
        Mesh mesh = tileMeshGenerator.GenerateMesh(tiles);

        Transform tileMesh = transform.GetChild(0);
        tileMesh.GetComponent<MeshFilter>().mesh = mesh;
        tileMesh.GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    public void UpdateMesh(Mesh mesh)
    {
        Transform tileMesh = transform.GetChild(0);
        tileMesh.GetComponent<MeshFilter>().mesh = mesh;
        tileMesh.GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    public void UpdateWorld()
    {
        RedrawMesh();
        UpdateTileCreators();
    }

    private void UpdateTileCreators()
    {
        foreach (Tile tile in tileCreators.Keys)
        {
            TileCreator creator = tileCreators[tile];

            creator.OnInteraction -= OnTileCreated;
            Destroy(creator.gameObject);
        }

        tileCreators.Clear();

        for (int y = 0; y < tiles.GetLength(1); y++)
        {
            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                Tile tile = tiles[x, y];

                if (tile.isActive)
                {
                    foreach (Tile neighbor in tile.GetNeighbors())
                    {
                        if (neighbor == null || (tile.animation != null && tile.animation.isRunning))
                        {
                            continue;
                        }

                        if (!neighbor.isActive)
                        {
                            CreateTileCreator(neighbor, tile);
                        }
                    }
                }
            }
        }
    }

    private void CreateTileCreator(Tile tile, Tile neighbor)
    {
        if (tileCreators.ContainsKey(tile))
        {
            return;
        }

        Vector3 position = new Vector3(
            tile.coordinates.x * tileMeshGenerator.tileSize.x,
            neighbor.height,
            tile.coordinates.y * tileMeshGenerator.tileSize.y
        );

        TileCreator creator = Instantiate(tileCreatorPrefab, position, Quaternion.identity, transform);
        creator.tile = tile;
        creator.OnInteraction += OnTileCreated;

        tileCreators.Add(tile, creator);
    }
}
