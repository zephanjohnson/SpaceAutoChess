using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.PlayerLoop;

public struct GameBoardState
{
    public List<InventorySlotState> InventoryState;
    public List<BoardSlotState> BoardState;
}

public struct InventorySlotState
{
    public int Index;
    public CollectibleData Data;
    public bool IsOccupied;
}

public struct BoardSlotState
{
    public int CoordinateX;
    public int CoordinateY;
    public CollectibleData Data;
    public bool IsOccupied;
}

public class GameBoardManager : MonoBehaviour
{
    public float BoardColumnSpacing = 3f;
    public float BoardRowSpacing = 1.2f;
    public float InventorySlotSpacing = 2f;

    public static int BOARD_NUM_COLUMNS = 2;
    public static int BOARD_NUM_ROWS = 6;
    public static int INVENTORY_NUM_SLOTS = 9;

    private Vector2 boardTopLeft = new Vector2(-7.5f, 3.75f);

    private GameBoardSlot[,] _boardSlots = new GameBoardSlot[BOARD_NUM_COLUMNS, BOARD_NUM_ROWS];
    private GameBoardSlot[] _inventorySlots = new GameBoardSlot[INVENTORY_NUM_SLOTS];

    public Vector3 InventorySpawnLocation = new Vector3(-8, -2, 0);
    private Transform _canvasTransform;
    private Transform _inventorySlotsTransform;
    private Transform _boardSlotsTransform;
    private GamestateManager _gamestateManager;
    private GameBoardState _gameBoardState;

    public int ShipCount;

    public void Awake()
    {
        _gamestateManager = GetComponent<GamestateManager>();
    }

    public void Initialize(GameBoardState state)
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
                    slot.Coordinate_X = column;
                    slot.Coordinate_Y = row;
                    _boardSlots[column, row] = slot;

                }
            }
        }

        LoadAutoplayState(state);
    }

    public GameBoardState GetState(bool firstLoad)
    {
        var retval = new GameBoardState();
        retval.BoardState = new List<BoardSlotState>();
        retval.InventoryState = new List<InventorySlotState>();

        if (_gamestateManager.State == GameState.Autoplay ||
            _gamestateManager.State == GameState.PostAutoPlay)
        {
            retval.BoardState = _gameBoardState.BoardState;
        }
        else {
            foreach (var slot in _boardSlots)
            {
                var state = new BoardSlotState();
                if (!firstLoad)
                {
                    state.CoordinateX = slot.Coordinate_X;
                    state.CoordinateY = slot.Coordinate_Y;
                    state.Data = slot.CollectibleData;
                    state.IsOccupied = slot.IsOccupied;
                }

                retval.BoardState.Add(state);
            }
        }        

        foreach (var slot in _inventorySlots)
        {
            var state = new InventorySlotState();

            if (slot) {
                state.Data = slot.CollectibleData;
                state.Index = slot.Index;
                state.IsOccupied = slot.IsOccupied;
            }
            
            retval.InventoryState.Add(state);
        }

        return retval;
    }

    public void LoadPlanningState(GameBoardState state)
    {
        state.InventoryState = state.InventoryState != null ? state.InventoryState : new List<InventorySlotState>();
        state.BoardState = state.BoardState != null ? state.BoardState : new List<BoardSlotState>();

        // Populate Inventory
        foreach (var slotState in state.InventoryState)
        {
            if (slotState.IsOccupied)
            {
                UpdateInventorySlot(slotState.Data, slotState.Index);
            }
        }

        foreach (var boardState in state.BoardState)
        {
            if (boardState.IsOccupied)
            {
                Debug.Log(boardState.CoordinateX);
                UpdateBoardSlot(boardState.Data, boardState.CoordinateX, boardState.CoordinateY);
                var slot = _boardSlots[boardState.CoordinateX, boardState.CoordinateY];
                slot.CollectibleData = boardState.Data;
                slot.Coordinate_X = boardState.CoordinateX;
                slot.Coordinate_Y = boardState.CoordinateY;
                slot.IsOccupied = boardState.IsOccupied;
                _boardSlots[slot.Coordinate_X, slot.Coordinate_Y] = slot;
            }
        }
    }
        
    public void LoadAutoplayState(GameBoardState state)
    {
        ShipCount = 0;
        _gameBoardState = state;
        state.InventoryState = state.InventoryState != null ? state.InventoryState : new List<InventorySlotState>();
        state.BoardState = state.BoardState != null ? state.BoardState : new List<BoardSlotState>();

        // Instantiate Ships
        if (_gamestateManager.State == GameState.PreAutoPlay)
        {
            
            foreach (var boardState in state.BoardState)
            {
                if (boardState.IsOccupied)
                    LoadShip(boardState);
                var slot = _boardSlots[boardState.CoordinateX, boardState.CoordinateY];
                slot.CollectibleData = boardState.Data;
                slot.Coordinate_X = boardState.CoordinateX;
                slot.Coordinate_Y = boardState.CoordinateY;
                slot.IsOccupied = boardState.IsOccupied;
                _boardSlots[slot.Coordinate_X, slot.Coordinate_Y] = slot;
            }
                
        }
        else {
            // Populate Board
            foreach (var boardState in state.BoardState)
                if (boardState.IsOccupied)
                    UpdateBoardSlot(boardState.Data, boardState.CoordinateX, boardState.CoordinateY);
        }
        
        // Populate Inventory
        foreach (var slotState in state.InventoryState)
        {
            if (slotState.IsOccupied)
            {
                UpdateInventorySlot(slotState.Data, slotState.Index);
            }
        }
    }
    
     public void LoadShip(BoardSlotState slot)
     {
        ShipCount++;

        //Debug.Log("x, y:" + slot.CoordinateX +", " + slot.CoordinateY);
        var go = Instantiate(Resources.Load(slot.Data.ResourcePath), new Vector3(slot.CoordinateY, slot.CoordinateX, 0f), Quaternion.Euler(0, 0, 0)) as GameObject;
        var spaceObject = go.GetComponent<AllySpaceObject>();
        var allyData = AllyDataLibrary.Allies[slot.Data.Key];
        spaceObject.SetData(allyData);
        spaceObject.OnAllySpaceObjectDestroyed += (ship) => {
            ShipCount--;
            if (ShipCount == 0) {
                _gamestateManager.LoseLevel();
            }
        };
        go.transform.position = new Vector3(boardTopLeft.x + 2.5f * slot.CoordinateX, boardTopLeft.y - 1.25f * slot.CoordinateY, -1);
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
    
    public void UpdateBoardSlot(CollectibleData data, int corX, int corY)
    {
        //Debug.Log("UpdateBoardSlot called");
        var slot = _boardSlots[corX, corY];
        slot.Release();
        slot.Assign(data);

        CombineUpgradeableAllies();
    }

    public void UpdateInventorySlot(CollectibleData data, int index)
    {
        var slot = _inventorySlots[index];
        slot.Release();
        slot.Assign(data);

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
