using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2Int coordinates;
    public GameObject tileGraphics;
    public GameObject plusGraphics;
    public GameObject treeGraphics;
    public Color colorDirt;
    public Color colorGrass;
    public int scoreByDirt;
    public int scoreByGrass;
    public Action<int> OnScoreGenerated = delegate { };
    public Action OnTileCreated = delegate { };
    public int costsForGrass;

    private TileState state = TileState.Uninitialized;
    public Tile[] neighbors = new Tile[4];
    private Coroutine scoreGenerator;

    private void Start()
    {
        World.Instance.OnScoreChange += OnScoreChange;
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

            if (World.Instance.GetScore() >= World.Instance.CostsForDirt)
            {
                plusGraphics.SetActive(true);
            }
            else
            {
                plusGraphics.SetActive(false);
            }
        }
        else if (newState == TileState.Dirt)
        {
            state = newState;
            tileGraphics.SetActive(true);
            tileGraphics.GetComponent<Renderer>().material.color = colorDirt;
            plusGraphics.SetActive(false);

            UpdateNeighbors();
            ActivateScoreGenerator();
            OnTileCreated();
        }
        else if (newState == TileState.Grass)
        {
            state = newState;
            tileGraphics.SetActive(true);
            tileGraphics.GetComponent<Renderer>().material.color = colorGrass;
            plusGraphics.SetActive(false);

            GrowTree();
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

    private void GrowTree()
    {
        treeGraphics.transform.Rotate(0f, UnityEngine.Random.Range(0f, 45f), 0f);
        treeGraphics.SetActive(true);
    }

    private void ActivateScoreGenerator()
    {
        if (scoreGenerator != null)
        {
            StopCoroutine(scoreGenerator);
        }

        scoreGenerator = StartCoroutine(GenerateScore());
    }

    private IEnumerator GenerateScore()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);

            if (state == TileState.Dirt)
            {
                OnScoreGenerated(scoreByDirt);
            }
            else if (state == TileState.Grass)
            {
                OnScoreGenerated(scoreByGrass);
            }
        }
    }

    private void OnMouseDown()
    {
        if (state == TileState.Plus && plusGraphics.activeSelf)
        {
            UpdateState(TileState.Dirt);
        }
        else if (state == TileState.Dirt)
        {
            UpdateState(TileState.Grass);
        }
    }

    private void OnScoreChange(int score)
    {
        if (state == TileState.Plus)
        {
            if (score >= World.Instance.CostsForDirt)
            {
                plusGraphics.SetActive(true);
            }
            else
            {
                plusGraphics.SetActive(false);
            }
        }
    }
}
