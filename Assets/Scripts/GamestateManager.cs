using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public enum GameState
{
    MainMenu,
    Pause,
    Planning,
    PreAutoPlay,
    Autoplay,
    PostAutoPlay,
    Unknown,
}

public class GamestateManager : MonoBehaviour
{
    public GameState State;

    /// The inventory manager for the game objects
    [SerializeField]
    private InventoryManager _inventoryManager;
    [SerializeField]
    private BoardManager _boardManager;
    [SerializeField]
    private GameBoardManager _gameBoardManager;

    [SerializeField] 
    private GameObject _startingShip;

    [SerializeField] 
    private LevelProgressionManager _levelProgressionManager;

    [SerializeField]
    private List<Collectible> prevInventoryState = null;
    private BoardSlot[,] prevBoardState = null;

    //internal representation of the inventory and board
    private Inventory _inventory = new Inventory();
    private Board _board = new Board();
    [SerializeField]
    private bool firstStart = true;
    
    
    //This is the currently selected collectible that is waiting to be placed.
    private Collectible selectedCollectible;
    private List<Vector2> highlightedBoardSlots;
    
    public int Level = 1;
    

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneChange;
    }

    public void Start()
    {
        DontDestroyOnLoad(this);
    }
    //call at the end of every planning phase so we can rollback if the player loses
    public void SaveCurrentGameState()
    {
        //prevBoardState = _boardManager.GetBoardState();
        //prevInventoryState = new List<Collectible>(inventory.Collectibles);
        //Update IsInInventory since the collectibles will have to be re-added to the inventory on scene transition
        //if (prevInventoryState != null)
        //    foreach (var col in prevInventoryState)
        //        col.Location = CollectibleLocation.None;
    }

    //call if the player loses
    //load all current ship positions on the board
    //restore the health of all ships
    //load state of the inventory
    //load the correct level's planning phase
    public void LoadPreviousGameState()
    {
        //_inventoryManager.InitializeInventory();
        //_boardManager.LoadBoardState(prevBoardState);
        //_inventoryManager.LoadInventoryState(prevInventoryState);
    }

    //called when you pick up a new collectible
    public void AddToInventory(Collectible collectible)
    {
        _gameBoardManager.AddToInventory(collectible.Data);
        //_inventory.Collectibles.Add(collectible);
        //_inventoryManager.AddToInventorySlot(collectible);
    }

    //called when collectible is removed
    public void RemoveFrominventory(Collectible collectible)
    {

    }

    public Inventory inventory
    {
        get => _inventory;
    }

    public Board board
    {
        get => _board;
    }

    public CollectibleType SelectionType
    {
        get => IsCollectibleSelected ? selectedCollectible.Data.Type : CollectibleType.None;
    }

    public bool IsCollectibleSelected
    {
        get => selectedCollectible != null;
    }

    //can be called for a collectible either in the inventory or on the map, gets the ship nearest to the mouse click
    public void SelectCollectible(Collectible collectible)
    {
        Debug.Log($"Selected {collectible.Data.Name}");
        selectedCollectible = collectible;
    }

    //places the selected ship in the slot closest to the click.
    public void PlaceCollectible()
    {
        //switch (selectedCollectible.Location)
        //{
        //    case CollectibleLocation.Board:
        //        _boardManager.AddToBoardSlot(highlightedBoardSlots, selectedCollectible);
        //        _boardManager.RemoveFromBoardSlot(selectedCollectible);
        //        break;
        //    case CollectibleLocation.Inventory:
        //        _boardManager.AddToBoardSlot(highlightedBoardSlots, selectedCollectible);
        //        _inventoryManager.RemoveFromInventorySlot(selectedCollectible);
        //        break;

        //}
        //selectedCollectible = null;

        ////When a collectible moves from inventory to the board it changes to the model of the ship instead of an icon,
        ////It disapears if it is an item because it's attached to a ship
        ////It changes back to an icon in the inventory if it was on the board before
    }

    public void HighlightSlots(Vector2 slotCoordinate)
    {
        // Don't highlight unless something is selected
        if (selectedCollectible == null) { return; }

        var data = selectedCollectible.Data;

        if (data.Type == CollectibleType.Ally)
        {
            highlightedBoardSlots = GetSelectedAllyCoordinateRange(slotCoordinate);
            bool canPlaceAllyShip = CanPlaceCollectible(slotCoordinate);
            // Color each slot
            var color = canPlaceAllyShip ? Color.green : Color.red;
            foreach (var coordinate in highlightedBoardSlots)
            {
                //_boardManager.Highlight(coordinate, color);

            }
        }
        else {
            // Non Ally highlights
            switch(data.Type)
            {
                case CollectibleType.Item:
                    //_boardManager.Highlight(slotCoordinate, Color.blue);
                    break;
                default:
                    //_boardManager.Highlight(slotCoordinate, Color.white);
                    break;
            }
        }
    }

    public void UnHighlightSlots()
    {
        if (highlightedBoardSlots == null) { return; }

        // Unhighlight each slot
        var color = Color.white;
        foreach (var coordinate in highlightedBoardSlots)
        {
            //_boardManager.Highlight(coordinate, color);
        }
    }

    public List<Vector2> GetSelectedAllyCoordinateRange(Vector2 slotCoordinate) {
        var retval = new List<Vector2>();
        // Don't highlight unless something is selected
        if (selectedCollectible == null) { return retval; }

        var data = selectedCollectible.Data;
        if (data.Type == CollectibleType.Ally)
        {
            var allyData = AllyDataLibrary.Allies[data.Key];
            var range = allyData.MovementRange;

            // go through each slot in range and get coordinates
            for (int y = (int)slotCoordinate.y; y < slotCoordinate.y + range; y++)
            {
                var potentialCoordinate = new Vector2(slotCoordinate.x, y);
                retval.Add(potentialCoordinate);
            }
        }

        return retval;
    }

    public bool CanPlaceCollectible(Vector2 slotCoordinate)
    {
        // Don't highlight unless something is selected
        if (selectedCollectible == null) { return false; }

        var data = selectedCollectible.Data;

        if (data.Type == CollectibleType.Ally)
        {
            var coordinates = GetSelectedAllyCoordinateRange(slotCoordinate);
            bool canPlaceAllyShip = true;

            // go through each slot in range and see if it is occupied
            foreach(var coordinate in coordinates)
            {
                //canPlaceAllyShip = canPlaceAllyShip && !_boardManager.IsSlotOccupied(coordinate);
            }

            // Color each slot
            return canPlaceAllyShip;
        }

        // TODO Add path for placing non-ally

        return false;
    }

    public void FirstTimeLoad()
    {
        //SceneManager.LoadScene("Prep");
        SceneManager.LoadScene("Planning");
        GameObject startingShip = GameObject.Instantiate(_startingShip);
        startingShip.GetComponent<Collectible>().Location = CollectibleLocation.Field;
        startingShip.transform.position = new Vector3(0, 0, 0);
    }
    public void LoadPlanningPhase()
    {
        SceneManager.LoadScene("Planning");
        LoadPreviousGameState();
        //Enter the planning phase, this should call LoadPreviousGameState and transition us to the layout scene
    }

    public void CompleteLevel()
    {
        Level += 1;
        SaveCurrentGameState();
        LoadPlanningPhase();
    }

    public void LoseLevel()
    {
        LoadPlanningPhase();
    }

    public void LoadAutoplayPhase()
    {
        SceneManager.LoadScene("Autoplay");
        //_gameBoardManager.Initialize();
        //_inventoryManager.LoadInventoryState(prevInventoryState);
        //Load the autoplayPhase that corresponds to the current level. need to figure out some way to do this that doesn't suck. if we're doing procedural stuff
        //then we need only one level, 
    }

    private void OnSceneChange(Scene scene, LoadSceneMode loadSceneMode) {
        switch (scene.name) {
            case "Start":
                State = GameState.MainMenu;
                break;
            case "Autoplay":
                State = GameState.PreAutoPlay;
                _gameBoardManager.Initialize();
                break;
            case "Planning":
                State = GameState.Planning;
                _gameBoardManager.Initialize();
                break;
            default:
                State = GameState.Unknown;
                break;
        }
    }

    public class Inventory
    {
        //public List<Collectible> Collectibles = new List<Collectible>();
    }

    public class Board
    {
        //private AllySpaceObject[,] Ships = new AllySpaceObject[BoardManager.NUM_COLUMNS, BoardManager.NUM_ROWS];
    }
}
