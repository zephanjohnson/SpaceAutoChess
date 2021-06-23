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
            CombineUpgradeableAllies();
            return;
        }
    }

    public void RemoveFromInventorySlot(Collectible col)
    {
        foreach (var slot in _inventorySlots)
        {
            if (slot.Collectible == col)
            {
                slot.Release();
                return;
            }
        }
    }

    private void CombineUpgradeableAllies()
    {
        Dictionary<string, List<int>> allyIndexMap = new Dictionary<string, List<int>>();
        for (int i = 0; i < _inventorySlots.Length; i++)
        {
            var slot = _inventorySlots[i];

            // Skip slot if it isn't occupied
            if (!slot.IsOccupied) { continue; }

            // Update ally map with slot index of each matching ally found
            if (allyIndexMap.Keys.Contains(slot.Collectible.Data.Name))
            {
                allyIndexMap[slot.Collectible.Data.Name].Add(i);
            }
            else {
                allyIndexMap.Add(slot.Collectible.Data.Name, new List<int> { i });
            }
        }

        foreach (var allyIndexes in allyIndexMap) {
            if (allyIndexes.Value.Count >= 3) {
                var upgradeableAlly = _inventorySlots[allyIndexes.Value.First()];

                // Upgrade the first ally
                upgradeableAlly.Collectible.Upgrade();

                //We don't want to release the first ally because that's the upgraded one
                allyIndexes.Value.RemoveAt(0);

                // Keep track of the number of release allies
                int releaseCount = 0;
                foreach (var remainingAlleyIndex in allyIndexes.Value) {
                    // If we have released two allies, stop releasing them
                    if (releaseCount >= 2) { return; }

                    // Release the other two allies
                    var slot = _inventorySlots[remainingAlleyIndex];
                    slot.Release();

                    // Update release count
                    releaseCount++;
                }
            }
        }
    }
}
