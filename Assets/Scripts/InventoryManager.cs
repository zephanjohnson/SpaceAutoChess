using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private InventorySlot[] _inventorySlots;

    public void Awake()
    {
        _inventorySlots = FindObjectsOfType<InventorySlot>();
        _inventorySlots = _inventorySlots.OrderBy(slot => slot.transform.position.x).ToArray();
    }

    public void AddToInventorySlot(Collectible col)
    {
        foreach (var slot in _inventorySlots)
        {
            if (col.IsInInventory)
            {
                return;
            }

            if (slot.IsOccupied) {
                continue;
            }
            

            slot.Assign(col);
            return;
        }
    }
}
