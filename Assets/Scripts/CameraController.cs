using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private WorldManager worldManager;

    public Action<Tile> OnTileSelected = delegate { };
    public Action<Tile> OnTileDeselected = delegate { };
    public Action<Tile> OnTileClick = delegate { };

    private new Camera camera;
    private Tile selectedTile;

    private void Awake()
    {
        camera = GetComponent<Camera>();
    }

    private void Update()
    {
        HandleTileMeshInteraction();
    }

    private void HandleTileMeshInteraction()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Tile tile = worldManager.GetTileByWorldPosition(hit.point);

            if (selectedTile == null)
            {
                SelectTile(tile);
            }
            else
            {
                if (selectedTile != tile)
                {
                    DeselectTile();
                    SelectTile(tile);
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                OnTileClick(tile);
            }
        }
        else
        {
            if (selectedTile != null)
            {
                DeselectTile();
            }
        }
    }

    private void SelectTile(Tile tile)
    {
        OnTileSelected(tile);
        selectedTile = tile;
    }

    private void DeselectTile()
    {
        OnTileDeselected(selectedTile);
        selectedTile = null;
    }
}
