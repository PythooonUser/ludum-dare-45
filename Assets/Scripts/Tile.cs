using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2Int coordinates;
    public GameObject tileGraphics;
    public GameObject plusGraphics;
    public Color colorDirt;
    public Color colorGrass;

    private TileState state = TileState.Uninitialized;
    public Tile[] neighbors = new Tile[4];

    private void Start()
    {
        // World.Instance.OnScoreChange += OnScoreChange;
    }

    public void Init(TileState initialState)
    {
        UpdateState(initialState);
    }

    private void UpdateState(TileState newState)
    {
        if (newState == state) { return; }

        if (newState == TileState.Empty)
        {
            state = newState;
            tileGraphics.SetActive(false);
            plusGraphics.SetActive(false);
        }
        else if (newState == TileState.Plus)
        {
            state = newState;
            tileGraphics.SetActive(false);
            plusGraphics.SetActive(true);
        }
        else if (newState == TileState.Dirt)
        {
            state = newState;
            tileGraphics.SetActive(true);
            tileGraphics.GetComponent<Renderer>().material.color = colorDirt;
            plusGraphics.SetActive(false);

            UpdateNeighbors();
        }
        else if (newState == TileState.Grass)
        {
            state = newState;
            tileGraphics.SetActive(true);
            tileGraphics.GetComponent<Renderer>().material.color = colorGrass;
            plusGraphics.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Invalid tile state set!");
        }
    }

    public void SetNeighbor(TileDirection direction, Tile tile)
    {
        neighbors[(int)direction] = tile;
    }

    public Tile GetNeighbor(TileDirection direction)
    {
        return neighbors[(int)direction];
    }

    private void UpdateNeighbors()
    {
        for (int i = 0; i < neighbors.Length; i++)
        {
            Tile neighbor = neighbors[i];
            if (neighbor == null) { continue; }

            neighbor.SetPlusState();
        }
    }

    public void SetPlusState()
    {
        if (state == TileState.Empty)
        {
            UpdateState(TileState.Plus);
        }
    }

    private void OnMouseDown()
    {
        if (state == TileState.Plus)
        {
            UpdateState(TileState.Dirt);
        }
        else if (state == TileState.Dirt)
        {
            UpdateState(TileState.Grass);
        }
    }

    private void OnScoreChange(int score) { }
}
