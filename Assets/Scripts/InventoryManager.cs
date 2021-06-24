using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static int NUM_INVENTORY_SLOTS = 9;

    public float SlotSpacing = 2f;

    private InventorySlot[] _inventorySlots = new InventorySlot[NUM_INVENTORY_SLOTS];

    public Vector3 SpawnLocation = new Vector3(-8,-4,0);
    private Transform _slotsTransform;

    public void InitializeInventory()
    {
        var boardSlots = new GameObject("InventorySlots");
        _slotsTransform = boardSlots.transform;

        for (int slotIndex = 0; slotIndex < NUM_INVENTORY_SLOTS; slotIndex++)
        {
            var go = Instantiate(Resources.Load("InventorySlot"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            go.transform.SetParent(_slotsTransform);
            go.transform.localPosition = new Vector3(slotIndex * SlotSpacing, 0, 0) + SpawnLocation;
            var inventorySlot = go.GetComponent<InventorySlot>();
            inventorySlot.Index = slotIndex;
            _inventorySlots[slotIndex] = inventorySlot;
        }

        foreach (var slot in _inventorySlots)
        {
            Debug.Log("Collectible null: " + slot.Collectible == null);
            Debug.Log("Slot is occupied: " + slot.IsOccupied);
            if (slot.Collectible == null)
                slot.IsOccupied = false;
        }
    }

    public void AddToInventorySlot(Collectible col)
    {
        if (col == null)
            return;
        if (_inventorySlots == null) 
            InitializeInventory();
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

    public void LoadInventoryState(List<Collectible> prevInventoryState)
    {
        if (prevInventoryState == null || prevInventoryState.Count == 0) 
            return;
        
        foreach (var collectible in prevInventoryState)
        {
            AddToInventorySlot(collectible);
        }
    }

    private void CombineUpgradeableAllies()
    {
        Dictionary<string, List<int>> allyIndexMap = new Dictionary<string, List<int>>();
        for (int i = 0; i < _inventorySlots.Length; i++)
        {
            var slot = _inventorySlots[i];

            // Skip slot if it isn't occupied
            if (!slot.IsOccupied || slot.Collectible == null) { continue; }
            
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
