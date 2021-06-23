using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class GamestateManager : MonoBehaviour
{
    /// The inventory manager for the game objects
    private InventoryManager _inventoryManager;
    private BoardManager _boardManager;

    //internal representation of the inventory and board
    private Inventory _inventory = new Inventory();
    private Board _board = new Board();
    
    //This is the currently selected collectible that is waiting to be placed.
    private Collectible selectedCollectible;
    private List<Vector2> highlightedBoardSlots;

    private void Awake()
    {
        _inventoryManager = FindObjectOfType<InventoryManager>();
        _boardManager = FindObjectOfType<BoardManager>();
    }

    //call at the end of every planning phase so we can rollback if the player loses
    public void SaveCurrentGameState()
    {
        //save all current ship positions on the board
        //save state of the inventory
        //save current level (to be used in the loader to load the correct scene)
    }

    //call if the player loses
    public void LoadPreviousGameState()
    {
        //load all current ship positions on the board
        //restore the health of all ships
        //load state of the inventory
        //load the correct level's planning phase
    }

    //called when you pick up a new collectible
    public void AddToInventory(Collectible collectible)
    {
        _inventory.Collectibles.Add(collectible);
        _inventoryManager.AddToInventorySlot(collectible);
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
        //When a collectible moves from inventory to the board it changes to the model of the ship instead of an icon,
        //It disapears if it is an item because it's attached to a ship
        //It changes back to an icon in the inventory if it was on the board before
    }

    public void HighlightSlots(Vector2 slotCoordinate)
    {
        // Don't highlight unless something is selected
        if (selectedCollectible == null) { return; }

        var data = selectedCollectible.Data;

        if (data.Type == CollectibleType.Ally)
        {
            var allyData = AllyDataLibrary.Allies[data.Name];
            var range = allyData.MovementRange;
            highlightedBoardSlots = new List<Vector2>();
            bool canPlaceAllyShip = true;

            // go through each slot in range and see if it is occupied
            for (int y = (int)slotCoordinate.y; y < slotCoordinate.y + range; y++)
            {
                var potentialCoordinate = new Vector2(slotCoordinate.x, y);
                highlightedBoardSlots.Add(potentialCoordinate);
                canPlaceAllyShip = canPlaceAllyShip && !_boardManager.IsSlotOccupied(potentialCoordinate);
            }

            // Color each slot
            var color = canPlaceAllyShip ? Color.green : Color.red;
            foreach (var coordinate in highlightedBoardSlots)
            {
                _boardManager.Highlight(coordinate, color);

            }
        }
        else {
            // Non Ally highlights
            switch(data.Type)
            {
                case CollectibleType.Item:
                    _boardManager.Highlight(slotCoordinate, Color.blue);
                    break;
                default:
                    _boardManager.Highlight(slotCoordinate, Color.white);
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
            _boardManager.Highlight(coordinate, color);
        }
    }


    public class Inventory
    {
        public List<Collectible> Collectibles = new List<Collectible>();
    }

    public class Board
    {
        private AllySpaceObject[,] Ships = new AllySpaceObject[BoardManager.NUM_COLUMNS, BoardManager.NUM_ROWS];
    }
}
