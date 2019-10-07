using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class World : MonoBehaviour
{
    public static World Instance;

    [Header("World Parameters")]
    public Vector2Int worldSize = new Vector2Int(11, 7);
    public Tile tilePrefab = default;

    public Action<int> OnScoreChange = delegate { };
    [HideInInspector]
    public Tile[] tiles;

    public Text scoreUI;
    private int score;

    public Text costsDirtUI;
    public int CostsForDirt = 1;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("World already instantiated!");
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        GenerateWorld();
        UpdateScoreUI();
        UpdateDirtCostsUI();
    }

    private void GenerateWorld()
    {
        float halfWidth = (worldSize.x - 1) / 2f;
        float halfHeight = (worldSize.y - 1) / 2f;

        Vector2Int centerCoordinates = new Vector2Int((int)halfWidth, (int)halfHeight);
        Tile centerTile = null;

        tiles = new Tile[worldSize.x * worldSize.y];

        for (int y = 0, i = 0; y < worldSize.y; y++)
        {
            for (int x = 0; x < worldSize.x; x++, i++)
            {
                Vector2Int tileCoordinates = new Vector2Int(x, y);
                Vector3 tilePosition = new Vector3(x - halfWidth, 0f, y - halfHeight);

                Tile tile = tiles[i] = Instantiate(tilePrefab, tilePosition, Quaternion.identity, transform);
                tile.name = tilePrefab.name + " " + i.ToString();
                tile.coordinates = tileCoordinates;

                if (tileCoordinates.x == centerCoordinates.x && tileCoordinates.y == centerCoordinates.y)
                {
                    centerTile = tile;
                }

                if (x > 0)
                {
                    tile.SetNeighbor(TileDirection.West, tiles[i - 1]);
                    tiles[i - 1].SetNeighbor(TileDirection.East, tile);
                }

                if (y > 0)
                {
                    tile.SetNeighbor(TileDirection.South, tiles[i - worldSize.x]);
                    tiles[i - worldSize.x].SetNeighbor(TileDirection.North, tile);
                }

                tile.OnScoreGenerated += OnScoreGenerated;
                tile.OnTileCreated += OnTileCreated;

                tile.Init(TileState.Empty);
            }
        }

        centerTile.Init(TileState.Dirt);
    }

    private void OnScoreGenerated(int newScore)
    {
        score += newScore;
        OnScoreChange(score);
        UpdateScoreUI();
    }

    private void OnTileCreated()
    {
        CostsForDirt *= 2;
        OnScoreChange(score);
        UpdateDirtCostsUI();
    }

    public int GetScore()
    {
        return score;
    }

    private void UpdateScoreUI()
    {
        scoreUI.text = "Score: " + score.ToString();
    }

    private void UpdateDirtCostsUI()
    {
        costsDirtUI.text = "Costs: " + CostsForDirt.ToString();
    }
}
