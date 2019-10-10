using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMesh
{
    private List<Vector3> vertices = new List<Vector3>();
    private List<int> triangles = new List<int>();
    private List<Color> colors = new List<Color>();

    private Vector2 tileSize;
    private Vector2 tileInset;

    public Mesh GenerateMesh(Tile[,] tiles, Vector2 tileSize, Vector2 tileInset)
    {
        vertices.Clear();
        triangles.Clear();
        colors.Clear();

        this.tileSize = tileSize;
        this.tileInset = tileInset;

        Mesh mesh = new Mesh();
        mesh.name = "Generate tile mesh";

        Triangulate(tiles);

        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles, 0);
        mesh.SetColors(colors);

        mesh.RecalculateNormals();

        return mesh;
    }

    private void Triangulate(Tile[,] tiles)
    {
        int width = tiles.GetLength(0);
        int height = tiles.GetLength(1);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Triangulate(tiles[x, y]);
            }
        }
    }

    private void Triangulate(Tile tile)
    {
        TileCoordinates coordinates = tile.coordinates;
        float height = tile.height;
        float tileHeight = 4f;

        float tileSizeXHalf = tileSize.x * 0.5f;
        float tileSizeYHalf = tileSize.y * 0.5f;

        Vector3 v1 = new Vector3(coordinates.x * tileSize.x - tileSizeXHalf + tileInset.x, height, coordinates.y * tileSize.y - tileSizeYHalf + tileInset.y);
        Vector3 v2 = new Vector3(coordinates.x * tileSize.x + tileSizeXHalf - tileInset.x, height, coordinates.y * tileSize.y - tileSizeYHalf + tileInset.y);
        Vector3 v3 = new Vector3(coordinates.x * tileSize.x - tileSizeXHalf + tileInset.x, height, coordinates.y * tileSize.y + tileSizeYHalf - tileInset.y);
        Vector3 v4 = new Vector3(coordinates.x * tileSize.x + tileSizeXHalf - tileInset.y, height, coordinates.y * tileSize.y + tileSizeYHalf - tileInset.y);

        Color[] colors = GetTileColors(tile);
        Color c1 = colors[0];
        Color c2 = colors[1];

        // TOP
        AddQuad(v1, v2, v3, v4);
        AddQuadColors(c1);

        // FRONT
        AddQuad(v1 + Vector3.down * tileHeight, v2 + Vector3.down * tileHeight, v1, v2);
        AddQuadColors(c2, c2, c1, c1);

        // LEFT
        AddQuad(v3 + Vector3.down * tileHeight, v1 + Vector3.down * tileHeight, v3, v1);
        AddQuadColors(c2, c2, c1, c1);

        // BACK
        AddQuad(v4 + Vector3.down * tileHeight, v3 + Vector3.down * tileHeight, v4, v3);
        AddQuadColors(c2, c2, c1, c1);

        // RIGHT
        AddQuad(v2 + Vector3.down * tileHeight, v4 + Vector3.down * tileHeight, v2, v4);
        AddQuadColors(c2, c2, c1, c1);
    }

    private void AddQuad(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4)
    {
        AddTriangle(v1, v3, v4);
        AddTriangle(v1, v4, v2);
    }

    private void AddQuadColors(Color c1, Color c2, Color c3, Color c4)
    {
        AddTriangleColors(c1, c3, c4);
        AddTriangleColors(c1, c4, c2);
    }

    private void AddQuadColors(Color c1)
    {
        AddTriangleColors(c1);
        AddTriangleColors(c1);
    }

    private void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
    {
        int vertexIndex = vertices.Count;

        vertices.Add(v1);
        vertices.Add(v2);
        vertices.Add(v3);

        triangles.Add(vertexIndex);
        triangles.Add(vertexIndex + 1);
        triangles.Add(vertexIndex + 2);
    }

    private void AddTriangleColors(Color c1, Color c2, Color c3)
    {
        colors.Add(c1);
        colors.Add(c2);
        colors.Add(c3);
    }

    private void AddTriangleColors(Color c1)
    {
        colors.Add(c1);
        colors.Add(c1);
        colors.Add(c1);
    }

    private Color[] GetTileColors(Tile tile)
    {
        Color[] colors = {
            new Color(0.624625f, 0.6792453f, 0.1057316f, 1f),
            new Color(0.745283f, 0.7306919f, 0.1792898f, 1f)
        };

        bool isSelected = tile.isSelected;
        bool isNeighborSelected = false;

        foreach (Tile neighbor in tile.GetNeighbors())
        {
            if (neighbor != null && neighbor.isSelected)
            {
                isNeighborSelected = true;
                break;
            }
        }

        colors[0] = isSelected ? Color.white : (isNeighborSelected ? Color.yellow : colors[0]);
        colors[1] = isSelected ? Color.white : (isNeighborSelected ? Color.yellow : colors[1]);

        return colors;
    }
}
