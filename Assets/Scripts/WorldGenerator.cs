using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TileMeshGenerator)), DisallowMultipleComponent]
public class WorldGenerator : MonoBehaviour
{
    [Header("World Parameters")]
    [SerializeField] private Vector2Int worldSize = new Vector2Int(11, 11);

    [Header("References")]
    [SerializeField] private Transform worldParent = default;
    [SerializeField] private TileMeshGenerator tileMeshGenerator = default;

    [Header("Prefabs")]
    [SerializeField] private Transform tileMeshPrefab = default;

    private Tile[,] tiles;

    public Tile[,] GenerateWorld()
    {
        GenerateTileMap();
        GenerateTileMesh();

        return tiles;
    }

    private void GenerateTileMap()
    {
        tiles = new Tile[worldSize.x, worldSize.y];

        for (int y = 0; y < worldSize.y; y++)
        {
            for (int x = 0; x < worldSize.x; x++)
            {
                GenerateTile(x, y);
            }
        }
    }

    private void GenerateTile(int x, int y)
    {
        Tile tile = new Tile();
        tiles[x, y] = tile;

        TileCoordinates coordinates = new TileCoordinates(x, y);
        tile.coordinates = coordinates;

        float height = Random.Range(-0.25f, 0.25f) - 0.25f * Mathf.Sqrt(Mathf.Pow(x - (worldSize.x - 1) * 0.5f, 2f) + Mathf.Pow(y - (worldSize.y - 1) * 0.5f, 2f));
        tile.height = height;
    }

    private void GenerateTileMesh()
    {
        Mesh mesh = tileMeshGenerator.GenerateMesh(tiles);

        Transform tileMesh = Instantiate(tileMeshPrefab, Vector3.zero, Quaternion.identity, worldParent);
        tileMesh.GetComponent<MeshFilter>().mesh = mesh;
        tileMesh.GetComponent<MeshCollider>().sharedMesh = mesh;
    }
}
