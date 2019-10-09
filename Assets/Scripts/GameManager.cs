using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Input Settings")]
    [SerializeField] private KeyCode pauseMenuKey = KeyCode.Escape;

    [Header("References")]
    [SerializeField] private WorldGenerator worldGenerator = default;
    [SerializeField] private WorldManager worldManager = default;

    private void Start()
    {
        Tile[,] tiles = worldGenerator.GenerateWorld();
        worldManager.tiles = tiles;
    }

    private void Update()
    {
        if (Input.GetKeyDown(pauseMenuKey))
        {
            ExitGame();
        }
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
