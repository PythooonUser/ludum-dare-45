using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private WorldManager worldManager;

    private new Camera camera;

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
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * hit.distance);
            Tile tile = worldManager.GetTileByWorldPosition(hit.point);
            Debug.Log("Hit: " + tile.coordinates.ToString());
        }
    }
}
