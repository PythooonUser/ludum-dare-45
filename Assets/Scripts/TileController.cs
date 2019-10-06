using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    public Vector2Int coordinates;
    public Action<Vector2Int> OnTileInteracted = delegate { };
    public Color colorDirt;
    public Color colorGrass;
    public GameObject tileGraphics;
    public GameObject plusGraphics;

    public enum TileState { Empty, Plus, Tile }
    public TileState tileState;

    private void Start()
    {
        tileState = (TileState)UnityEngine.Random.Range(0, 3);
        UpdateState();
    }

    private void UpdateState()
    {
        if (tileState == TileState.Empty)
        {
            tileGraphics.SetActive(false);
            plusGraphics.SetActive(false);
        }
        else if (tileState == TileState.Plus)
        {
            tileGraphics.SetActive(false);
            plusGraphics.SetActive(true);
        }
        else if (tileState == TileState.Tile)
        {
            tileGraphics.SetActive(true);
            plusGraphics.SetActive(false);
        }
        else
        {
            Debug.LogError("Invalid tile state!");
        }
    }

    private void OnMouseEnter()
    {
        OnTileInteracted(coordinates);
    }
}
