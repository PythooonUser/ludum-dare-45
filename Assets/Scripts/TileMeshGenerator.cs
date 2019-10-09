using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMeshGenerator : MonoBehaviour
{
    [Header("Tile Mesh Parameters")]
    [SerializeField] private Vector2 tileSize = Vector3.one;
    [SerializeField] private Vector2 tileInset = new Vector2(0.15f, 0.15f);
    [SerializeField] private float groundLevel = -6f;

    private TileMesh tileMesh = new TileMesh();

    public Mesh GenerateMesh(Tile[,] tiles)
    {
        return tileMesh.GenerateMesh(tiles, tileSize, tileInset, groundLevel);
    }
}
