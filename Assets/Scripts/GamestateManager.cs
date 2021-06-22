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

    //internal representation of the inventory and board
    private Inventory _inventory = new Inventory();
    private Board _board = new Board();
    
    //This is the currently selected collectible that is waiting to be placed.
    private Collectible selectedCollectible;

    private void Awake()
    {
        _inventoryManager = GetComponent<InventoryManager>();
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

    //can be called for a collectible either in the inventory or on the map, gets the ship nearest to the mouse click
    public void SelectCollectible(Collectible collectible)
    {
        Debug.Log($"Selected {collectible.Name}");
        selectedCollectible = collectible;
    }

    //places the selected ship in the slot closest to the click.
    public void PlaceCollectible()
    {
        //When a collectible moves from inventory to the board it changes to the model of the ship instead of an icon,
        //It disapears if it is an item because it's attached to a ship
        //It changes back to an icon in the inventory if it was on the board before
    }


    public class Inventory
    {
        public List<Collectible> Collectibles = new List<Collectible>();
    }

    public class Board
    {
        //a data structure containing the current layout of ships on the grid
    }
}
