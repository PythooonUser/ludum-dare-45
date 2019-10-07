using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(World))]
public class WorldEditor : Editor
{
    void OnSceneGUI()
    {
        World world = target as World;
        if (world == null) { return; }

        if (world.tiles != null)
        {
            for (int i = 0; i < world.tiles.Length; i++)
            {
                Tile tile = world.tiles[i];

                Handles.Label(
                    tile.transform.position,
                    i.ToString()
                );
            }
        }
    }
}
