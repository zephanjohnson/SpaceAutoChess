using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSlot : MonoBehaviour
{
    public bool IsOccupied;
    public AllySpaceObject Shipship;
    public Vector2 Coordinate;

    private GamestateManager _gamestateManager;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _gamestateManager = FindObjectOfType<GamestateManager>();
        _spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        Highlight(Color.white);
    }

    public void Highlight(Color color) {
        _spriteRenderer.color = color;
    }

    private void OnMouseDown()
    {

    }

    private void OnMouseEnter()
    {
        if (_gamestateManager.IsCollectibleSelected)
        {
            _gamestateManager.HighlightSlots(Coordinate);
        }
    }

    private void OnMouseExit()
    {
        _gamestateManager.UnHighlightSlots();
    }
}
