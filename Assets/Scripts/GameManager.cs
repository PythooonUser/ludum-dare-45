using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Key Bindings")]
    public KeyCode pauseMenuKey = KeyCode.Escape;

    [Header("References")]
    public WorldManager worldManager;

    private void Start()
    {
        worldManager.Init();
    }
}
