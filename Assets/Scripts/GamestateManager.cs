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

    [SerializeField]
    private GameBoardManager _gameBoardManager;

    [SerializeField] 
    private GameObject _startingShip;

    [SerializeField]
    private GameBoardState prevBoardState;

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
    public void SaveCurrentGameState(bool firstLoad)
    {
        prevBoardState = _gameBoardManager.GetState(firstLoad);
    }

    //call if the player loses
    //load all current ship positions on the board
    //restore the health of all ships
    //load state of the inventory
    //load the correct level's planning phase
    public void LoadAutoplayGameState()
    {
        _gameBoardManager.LoadAutoplayState(prevBoardState);
    }

    public void LoadPlanningGameState()
    {
        _gameBoardManager.LoadPlanningState(prevBoardState);
    }

    //called when you pick up a new collectible
    public void AddToInventory(Collectible collectible)
    {
        _gameBoardManager.AddToInventory(collectible.Data);
    }

    ////called when collectible is removed
    //public void RemoveFrominventory(Collectible collectible)
    //{

    //}

    //public CollectibleType SelectionType
    //{
    //    get => IsCollectibleSelected ? selectedCollectible.Data.Type : CollectibleType.None;
    //}

    //public bool IsCollectibleSelected
    //{
    //    get => selectedCollectible != null;
    //}

    ////can be called for a collectible either in the inventory or on the map, gets the ship nearest to the mouse click
    //public void SelectCollectible(Collectible collectible)
    //{
    //    Debug.Log($"Selected {collectible.Data.Name}");
    //    selectedCollectible = collectible;
    //}

    //places the selected ship in the slot closest to the click.
    //public void PlaceCollectible()
    //{
    //    //switch (selectedCollectible.Location)
    //    //{
    //    //    case CollectibleLocation.Board:
    //    //        _boardManager.AddToBoardSlot(highlightedBoardSlots, selectedCollectible);
    //    //        _boardManager.RemoveFromBoardSlot(selectedCollectible);
    //    //        break;
    //    //    case CollectibleLocation.Inventory:
    //    //        _boardManager.AddToBoardSlot(highlightedBoardSlots, selectedCollectible);
    //    //        _inventoryManager.RemoveFromInventorySlot(selectedCollectible);
    //    //        break;

    //    //}
    //    //selectedCollectible = null;

    //    ////When a collectible moves from inventory to the board it changes to the model of the ship instead of an icon,
    //    ////It disapears if it is an item because it's attached to a ship
    //    ////It changes back to an icon in the inventory if it was on the board before
    //}

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
        //GameObject startingShip = GameObject.Instantiate(_startingShip);
        //startingShip.GetComponent<Collectible>().Location = CollectibleLocation.Field;
        //startingShip.transform.position = new Vector3(0, 0, 0);
        //SaveCurrentGameState(true);
    }
    public void LoadPlanningPhase()
    {
        SceneManager.LoadScene("Planning");
        LoadPlanningGameState();
        //Enter the planning phase, this should call LoadPreviousGameState and transition us to the layout scene
    }

    public void CompleteLevel()
    {
        Level += 1;
        SaveCurrentGameState(false);
        LoadPlanningPhase();
    }

    public void LoseLevel()
    {
        LoadPlanningPhase();
    }

    public void LoadAutoplayPhase()
    {
        // Save the planning scene
        SaveCurrentGameState(false);
        SceneManager.LoadScene("Autoplay");

        // Load the state from planning when scene changes
        LoadAutoplayGameState();
    }

    private void OnSceneChange(Scene scene, LoadSceneMode loadSceneMode) {
        switch (scene.name) {
            case "Start":
                State = GameState.MainMenu;
                break;
            case "Autoplay":
                State = GameState.PreAutoPlay;
                _gameBoardManager.Initialize(prevBoardState);
                break;
            case "Planning":
                State = GameState.Planning;
                _gameBoardManager.Initialize(prevBoardState);
                _gameBoardManager.LoadPlanningState(prevBoardState);
                break;
            default:
                State = GameState.Unknown;
                break;
        }
    }
}
