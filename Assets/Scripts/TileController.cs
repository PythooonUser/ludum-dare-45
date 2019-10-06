using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    public Vector2Int coordinates;
    public Action<Vector2Int> OnTileInteracted;

    private void OnMouseEnter()
    {
        OnTileInteracted(coordinates);
    }
}
