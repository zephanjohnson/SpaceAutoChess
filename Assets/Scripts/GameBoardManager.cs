﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameBoardManager : MonoBehaviour
{
    public float BoardColumnSpacing = 3f;
    public float BoardRowSpacing = 1.2f;
    public float InventorySlotSpacing = 2f;

    public static int BOARD_NUM_COLUMNS = 2;
    public static int BOARD_NUM_ROWS = 6;
    public static int INVENTORY_NUM_SLOTS = 9;

    private GameBoardSlot[,] _boardSlots = new GameBoardSlot[BOARD_NUM_COLUMNS, BOARD_NUM_ROWS];
    private GameBoardSlot[] _inventorySlots = new GameBoardSlot[INVENTORY_NUM_SLOTS];

    public Vector3 InventorySpawnLocation = new Vector3(-8, -2, 0);
    private Transform _canvasTransform;
    private Transform _inventorySlotsTransform;
    private Transform _boardSlotsTransform;
    private GamestateManager _gamestateManager;

    public void Awake()
    {
        _gamestateManager = GetComponent<GamestateManager>();
    }

    public void Initialize()
    {
        _canvasTransform = FindObjectOfType<Canvas>().transform;

        // Instantiate Inventory
        var inventorySlotsGo = Instantiate(Resources.Load("InventorySlots"), new Vector3(0, 0, 0), Quaternion.identity, _canvasTransform) as GameObject;
        _inventorySlotsTransform = inventorySlotsGo.transform;
        _inventorySlotsTransform.GetComponent<RectTransform>().anchoredPosition = Vector2.one;

        for (int index = 0; index < INVENTORY_NUM_SLOTS; index++)
        {
            var go = Instantiate(Resources.Load("InventorySlot"), new Vector3(0, 0, 0), Quaternion.identity, _inventorySlotsTransform) as GameObject;
            var tileTransform = go.transform.Find("Container").Find("DraggableTile");
            var drag = tileTransform.GetComponent<Drag>();
            var slot = tileTransform.GetComponent<GameBoardSlot>();
            drag.OnSlotChange += slot.OnSlotChange;
            drag.OnSlotChange += OnSlotChange;

            slot.Index = index;
            _inventorySlots[index] = slot;
        }

        if (_gamestateManager.State == GameState.Planning)
        {
            // Instantiate Board
            var boardSlotsGo = Instantiate(Resources.Load("BoardSlots"), new Vector3(0, 0, 0), Quaternion.identity, _canvasTransform) as GameObject;
            _boardSlotsTransform = boardSlotsGo.transform;
            _boardSlotsTransform.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            for (int column = 0; column < BOARD_NUM_COLUMNS; column++)
            {
                var boardSlotColumn = Instantiate(Resources.Load("BoardSlotColumn"), new Vector3(0, 0, 0), Quaternion.identity, _boardSlotsTransform) as GameObject;

                for (int row = 0; row < BOARD_NUM_ROWS; row++)
                {
                    var go = Instantiate(Resources.Load("GameBoardSlot"), new Vector3(0, 0, 0), Quaternion.identity, boardSlotColumn.transform) as GameObject;
                    var tileTransform = go.transform.Find("Container").Find("DraggableTile");
                    var drag = tileTransform.GetComponent<Drag>();
                    var slot = tileTransform.GetComponent<GameBoardSlot>();
                    drag.OnSlotChange += slot.OnSlotChange;
                    _boardSlots[column, row] = slot;
                }
            }
        }

        if (_gamestateManager.State == GameState.PreAutoPlay)
        {
            // Instantiate Ships
            for (int column = 0; column < BOARD_NUM_COLUMNS; column++)
            {
                for (int row = 0; row < BOARD_NUM_ROWS; row++)
                {
                   
                }
            }
        }
    }

    public GameBoardSlot[,] GetState()
    {
        return _boardSlots;
    }

    public void LoadState(GameBoardSlot[,] boardSlotsToLoad)
    {
        _boardSlots = boardSlotsToLoad;
    }

    public void PlaceCollectible()
    {

    }

    public void AddToInventory(CollectibleData data)
    {
        foreach (var slot in _inventorySlots)
        {
            if (slot.IsOccupied) {
                continue;
            }

            slot.Assign(data);
            break;
        }

        CombineUpgradeableAllies();
    }

    private void CombineUpgradeableAllies()
    {
        Dictionary<string, List<GameBoardSlot>> allyIndexMap = new Dictionary<string, List<GameBoardSlot>>();

        for (int i = 0; i < _boardSlots.GetLength(0); i++)
        {
            for (int j = 0; j < _boardSlots.GetLength(1); j++)
            {
                var slot = _boardSlots[i, j];

                // Skip slot if it isn't occupied
                if (!slot.IsOccupied) { continue; }

                // Update ally map with slot index of each matching ally found
                if (allyIndexMap.Keys.Contains(slot.CollectibleData.Name))
                {
                    allyIndexMap[slot.CollectibleData.Name].Add(slot);
                }
                else
                {
                    allyIndexMap.Add(slot.CollectibleData.Name, new List<GameBoardSlot> { slot });
                }
            }
        }

        for (int i = 0; i < _inventorySlots.Length; i++)
        {
            var slot = _inventorySlots[i];

            // Skip slot if it isn't occupied
            if (!slot.IsOccupied) { continue; }

            // Update ally map with slot index of each matching ally found
            if (allyIndexMap.Keys.Contains(slot.CollectibleData.Name))
            {
                allyIndexMap[slot.CollectibleData.Name].Add(slot);
            }
            else
            {
                allyIndexMap.Add(slot.CollectibleData.Name, new List<GameBoardSlot> { slot });
            }
        }

        foreach (var allyIndexes in allyIndexMap)
        {
            if (allyIndexes.Value.Count >= 3)
            {
                var upgradeableSlot = allyIndexes.Value.First();

                // Upgrade the first ally
                var upgradedData = upgradeableSlot.CollectibleData.GetUpgrade();
                upgradeableSlot.Assign(upgradedData);

                //We don't want to release the first ally because that's the upgraded one
                allyIndexes.Value.RemoveAt(0);

                // Keep track of the number of release allies
                int releaseCount = 0;
                foreach (var remainingSlot in allyIndexes.Value)
                {
                    // If we have released two allies, stop releasing them
                    if (releaseCount >= 2) { return; }

                    // Release the other two allies
                    remainingSlot.Release();

                    // Update release count
                    releaseCount++;
                }
            }
        }
    }

    private void OnSlotChange(GameBoardSlot oldSlot, GameBoardSlot newSlot)
    {
        CombineUpgradeableAllies();
    }

    public void Temporary_AddNewCollectible()
    {
        var allyData = GetRandomAllyData();
        AddToInventory(allyData.Data);
    }

    private AllyData GetRandomAllyData()
    {

        int rndFloat = Random.Range(0, AllyDataLibrary.LevelOneAllyKeys.Length);
        var allieKeys = AllyDataLibrary.LevelOneAllyKeys;
        return AllyDataLibrary.Allies[allieKeys[rndFloat]];
    }
}
