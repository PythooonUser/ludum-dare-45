using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCreator : MonoBehaviour
{
    public Tile tile;
    public Action<TileCreator> OnInteraction = delegate { };

    private void OnMouseDown()
    {
        OnInteraction(this);
    }
}
