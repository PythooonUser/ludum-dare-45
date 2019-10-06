using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldManager : MonoBehaviour
{
    public Vector2Int worldSize;
    public TileController tilePrefab;
    public Text scoreText;
    public float startDelay = 1.5f;

    private int score;
    private Dictionary<Vector2Int, TileController> tiles;

    private void Start()
    {
        scoreText.text = score.ToString();

        SetupWorld();
        StartCoroutine(SpawnInitialTile());
    }

    private void SetupWorld()
    {
        tiles = new Dictionary<Vector2Int, TileController>();

        float halfWidth = (worldSize.x - 1) / 2f;
        float halfHeight = (worldSize.y - 1) / 2f;

        for (int y = 0; y < worldSize.y; y++)
        {
            for (int x = 0; x < worldSize.x; x++)
            {
                Vector3 tilePosition = new Vector3(x - halfWidth, 0f, y - halfHeight);
                Vector2Int tileCoordinates = new Vector2Int(x, y);

                TileController tileController = Instantiate(tilePrefab, tilePosition, Quaternion.identity, transform);
                tiles.Add(tileCoordinates, tileController);

                tileController.coordinates = tileCoordinates;
                tileController.tileState = TileController.TileState.Empty;
                tileController.OnTileInteracted += OnTileInteraction;
                tileController.OnScoreGenerated += OnScoreChange;
            }
        }
    }

    private IEnumerator SpawnInitialTile()
    {
        yield return new WaitForSeconds(startDelay);

        float halfWidth = (worldSize.x - 1) / 2f;
        float halfHeight = (worldSize.y - 1) / 2f;
        Vector2Int centerCoordinates = new Vector2Int((int)halfWidth, (int)halfHeight);

        SpawnTile(centerCoordinates);
    }

    private void SpawnTile(Vector2Int coordinates)
    {
        TileController tileController = tiles[coordinates];

        if ((int)tileController.tileState < 2)
        {
            tileController.tileState = TileController.TileState.DirtTile;
            tileController.UpdateState();
            SpawnNeighbors(tileController);
        }
    }

    private void SpawnNeighbors(TileController tileController)
    {
        SpawnNeighbor(GetNeighborTop(tileController.coordinates));
        SpawnNeighbor(GetNeighborRight(tileController.coordinates));
        SpawnNeighbor(GetNeighborBottom(tileController.coordinates));
        SpawnNeighbor(GetNeighborLeft(tileController.coordinates));
    }

    private void SpawnNeighbor(TileController tileController)
    {
        if (tileController == null) { return; }
        if (tileController.tileState != TileController.TileState.Empty) { return; }

        tileController.tileState = TileController.TileState.Plus;
        tileController.UpdateState();
    }

    private void OnTileInteraction(TileController tileController)
    {
        if (tileController.tileState == TileController.TileState.Plus)
        {
            SpawnTile(tileController.coordinates);
        }
    }

    private void OnScoreChange()
    {
        score += 1;
        scoreText.text = score.ToString();
    }

    private TileController GetNeighborTop(Vector2Int coordinates)
    {
        return GetNeighbor(new Vector2Int(coordinates.x, coordinates.y + 1));
    }

    private TileController GetNeighborRight(Vector2Int coordinates)
    {
        return GetNeighbor(new Vector2Int(coordinates.x + 1, coordinates.y));
    }

    private TileController GetNeighborBottom(Vector2Int coordinates)
    {
        return GetNeighbor(new Vector2Int(coordinates.x, coordinates.y - 1));
    }

    private TileController GetNeighborLeft(Vector2Int coordinates)
    {
        return GetNeighbor(new Vector2Int(coordinates.x - 1, coordinates.y));
    }

    private TileController GetNeighbor(Vector2Int coordinates)
    {
        if (tiles.ContainsKey(coordinates))
        {
            return tiles[coordinates];
        }
        else
        {
            return null;
        }
    }
}
