using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public enum SlotType
{
    Inventory,
    Board
}

public class GameBoardSlot : MonoBehaviour
{
    public CollectibleData CollectibleData;
    public SlotType Type;
    //public int Coordinate_X;
    //public int Coordinate_Y;
    public int Index;
    public bool IsOccupied;

    private Image _draggableIcon;

    public void Awake()
    {
        _draggableIcon = transform.GetComponent<Image>();
    }

    public void OnSlotChange(GameBoardSlot oldSlot, GameBoardSlot newSlot) {
        var oldData = oldSlot.CollectibleData;
        var newData = newSlot.CollectibleData;

        oldSlot.Release();

        if (newSlot.IsOccupied)
        {
            oldSlot.Assign(newData);
        }

        newSlot.Release();
        newSlot.Assign(oldData);
    }

    public void Assign(CollectibleData data)
    {
        Debug.Log($"Assign {Index} - {data.Name}");
        CollectibleData = data;
        IsOccupied = true;
        _draggableIcon.sprite = Resources.Load<Sprite>(data.IconResourcePath);
        Color tmp = _draggableIcon.color;
        tmp.a = 1f;
        _draggableIcon.color = tmp;
    }

    public void Release()
    {
        Debug.Log($"Release {Index} - {CollectibleData.Name}");
        CollectibleData = new CollectibleData();
        IsOccupied = false;
        _draggableIcon.sprite = null;
        Color tmp = _draggableIcon.color;
        tmp.a = 0f;
        _draggableIcon.color = tmp;
    }
}
