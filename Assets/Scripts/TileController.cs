using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    [Header("Tile Parameters")]
    public Vector2Int coordinates;
    public TileGroundType groundType = TileGroundType.None;
    public List<TileGroundColor> groundColors = new List<TileGroundColor>();
    public float hoverEffectStrength = 0.15f;

    [Header("References")]
    public GameObject tileGraphics;
    public GameObject plusGraphics;

    private void Start()
    {
        SetGroundType(groundType);
    }

    private void OnMouseEnter()
    {
        ActivateHoverEffect();
    }

    private void OnMouseExit()
    {
        DeactivateHoverEffect();
    }

    private void OnMouseOver() { }

    public void SetGroundType(TileGroundType groundType)
    {
        // if (this.groundType == groundType) { return; }
        this.groundType = groundType;

        if (groundType == TileGroundType.None)
        {
            tileGraphics.SetActive(false);
            plusGraphics.SetActive(true);
        }
        else if (groundType == TileGroundType.Dirt)
        {
            tileGraphics.SetActive(true);
            tileGraphics.GetComponent<Renderer>().material.color = GetTileGroundColor(groundType);
            plusGraphics.SetActive(false);
        }
        else if (groundType == TileGroundType.Grass)
        {
            tileGraphics.SetActive(true);
            tileGraphics.GetComponent<Renderer>().material.color = GetTileGroundColor(groundType);
            plusGraphics.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Invalid ground type: " + groundType.ToString() + " set!");
        }
    }

    private Color GetTileGroundColor(TileGroundType groundType)
    {
        foreach (TileGroundColor groundColor in groundColors)
        {
            if (groundColor.groundType == groundType)
            {
                return groundColor.color;
            }
        }

        Debug.LogWarning("No color found for ground type: " + groundType.ToString() + ". Did you forget to define it?");
        return Color.white;
    }

    private void ActivateHoverEffect()
    {
        transform.localScale = Vector3.one * (1f + hoverEffectStrength);
    }

    private void DeactivateHoverEffect()
    {
        transform.localScale = Vector3.one;
    }
}
