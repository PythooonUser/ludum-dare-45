﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    public Vector2Int coordinates;
    public Action<TileController> OnTileInteracted = delegate { };
    public Action OnScoreGenerated = delegate { };
    public Color colorDirt;
    public Color colorGrass;
    public GameObject tileGraphics;
    public GameObject plusGraphics;
    public GameObject treeGraphics;

    public enum TileState { Empty, Plus, DirtTile, GrassTile }
    public TileState tileState;
    private bool hasTree = false;

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

    public void UpdateState()
    {
        if (tileState == TileState.Empty)
        {
            tileGraphics.SetActive(false);
            plusGraphics.SetActive(false);

            if (scoreRoutine != null)
            {
                StopCoroutine(scoreRoutine);
            }
            treeGraphics.SetActive(false);
        }
        else if (tileState == TileState.Plus)
        {
            tileGraphics.SetActive(false);
            plusGraphics.SetActive(true);

            if (scoreRoutine != null)
            {
                StopCoroutine(scoreRoutine);
            }
            treeGraphics.SetActive(false);
        }
        else if ((int)tileState >= 2)
        {
            tileGraphics.SetActive(true);
            plusGraphics.SetActive(false);

            if (tileState == TileState.DirtTile)
            {
                tileGraphics.GetComponent<Renderer>().material.color = colorDirt;
            }
            else if (tileState == TileState.GrassTile)
            {
                tileGraphics.GetComponent<Renderer>().material.color = colorGrass;
            }

            // scoreRoutine = StartCoroutine(GenerateScore());
            // treeGraphics.SetActive(true);
        }
        else
        {
            Debug.LogError("Invalid tile state!");
        }
    }

    private void OnMouseDown()
    {
        if (tileState == TileState.DirtTile)
        {
            tileState = TileState.GrassTile;
            UpdateState();
            treeGraphics.SetActive(true);
        }

        // if (tileState == TileState.Plus)
        // {
        //     tileState = TileState.Tile;
        //     UpdateState();
        // }

        OnTileInteracted(this);
    }
}
