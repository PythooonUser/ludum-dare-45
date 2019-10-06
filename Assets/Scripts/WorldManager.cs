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

    private int score;

    private void Start()
    {
        SetupWorld();
        scoreText.text = score.ToString();
    }

    private void SetupWorld()
    {
        float halfWidth = (worldSize.x - 1) / 2f;
        float halfHeight = (worldSize.y - 1) / 2f;

        Vector2Int centerCoordinates = new Vector2Int((int)halfWidth, (int)halfHeight);

        for (int y = 0; y < worldSize.y; y++)
        {
            for (int x = 0; x < worldSize.x; x++)
            {
                Vector3 tilePosition = new Vector3(x - halfWidth, UnityEngine.Random.Range(-0.1f, 0.1f), y - halfHeight);
                TileController tileController = Instantiate(tilePrefab, tilePosition, Quaternion.identity, transform);
                tileController.coordinates = new Vector2Int(x, y);
                tileController.OnTileInteracted += OnTileInteraction;
                tileController.OnScoreGenerated += OnScoreChange;

                if (centerCoordinates.x == x && centerCoordinates.y == y)
                {
                    tileController.tileState = TileController.TileState.Tile;
                }
                else if (
                    (centerCoordinates.x == x && (centerCoordinates.y == y - 1 || centerCoordinates.y == y + 1))
                    || (centerCoordinates.y == y && (centerCoordinates.x == x - 1 || centerCoordinates.x == x + 1))
                )
                {
                    tileController.tileState = TileController.TileState.Plus;
                }
                else
                {
                    tileController.tileState = TileController.TileState.Empty;
                }
            }
        }
    }

    private void OnTileInteraction(Vector2Int coordinates)
    {
        Debug.Log("Interaction: " + coordinates.ToString());
    }

    private void OnScoreChange()
    {
        score += 1;
        scoreText.text = score.ToString();
    }
}
