using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class GamestateManager : MonoBehaviour
{
    //internal representation of the inventory and board
    private Inventory _inventory;
    private Board _board;
    
    //This is the currently selected ship that is waiting to be placed.
    private GameObject selectedShip;

    //call at the end of every planning phase so we can rollback if the player loses
    void SaveCurrentGameState()
    {
        //save all current ship positions on the board
        //save state of the inventory
        //save current level (to be used in the loader to load the correct scene)
    }

    //call if the player loses
    void LoadPreviousGameState()
    {
        //load all current ship positions on the board
        //load state of the inventory
        //load the correct level's planning phase
    }

    //called when you pick up a new ship
    void AddToInventory(GameObject ship)
    {
        
    }

    //called when ship is removed
    void RemoveFrominventory(GameObject ship)
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

    //can be called for a ship either in the inventory or on the map, gets the ship nearest to the mouse click
    void SelectShip()
    {
        
    }

    //places the selected ship in the slot closest to the click.
    void PlaceShip()
    {
        
    }


    public class Inventory
    {
        //a data structure containing all of the ships currently in the inventory
    }

    public class Board
    {
        //a data structure containing the current layout of ships on the grid
    }
}
