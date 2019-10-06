using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    public Vector2Int coordinates;
    public Action<Vector2Int> OnTileInteracted = delegate { };
    public Action OnScoreGenerated = delegate { };
    public Color colorDirt;
    public Color colorGrass;
    public GameObject tileGraphics;
    public GameObject plusGraphics;

    public enum TileState { Empty, Plus, Tile }
    public TileState tileState;

    private Coroutine scoreRoutine;

    private void Start()
    {
        UpdateState();
    }

    IEnumerator GenerateScore()
    {
        while (true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(1, 4));
            OnScoreGenerated();
        }
    }

    private void UpdateState()
    {
        if (tileState == TileState.Empty)
        {
            tileGraphics.SetActive(false);
            plusGraphics.SetActive(false);

            if (scoreRoutine != null)
            {
                StopCoroutine(scoreRoutine);
            }
        }
        else if (tileState == TileState.Plus)
        {
            tileGraphics.SetActive(false);
            plusGraphics.SetActive(true);

            if (scoreRoutine != null)
            {
                StopCoroutine(scoreRoutine);
            }
        }
        else if (tileState == TileState.Tile)
        {
            tileGraphics.SetActive(true);
            plusGraphics.SetActive(false);

            scoreRoutine = StartCoroutine(GenerateScore());
        }
        else
        {
            Debug.LogError("Invalid tile state!");
        }
    }

    private void OnMouseDown()
    {
        OnTileInteracted(coordinates);

        if (tileState == TileState.Plus)
        {
            tileState = TileState.Tile;
            UpdateState();
        }
    }
}
