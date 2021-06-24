using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class BoardManager : MonoBehaviour
{
    public float ColumnSpacing = 3f;
    public float RowSpacing = 1.2f;

    public static int NUM_COLUMNS = 2;
    public static int NUM_ROWS = 6;

    private BoardSlot[,] _boardSlots = new BoardSlot[NUM_COLUMNS, NUM_ROWS];

    public Vector3 SpawnLocation = new Vector3(-8, -2, 0);
    private Transform _slotsTransform;

    public void InitializeBoard()
    {

        var boardSlots = new GameObject("BoardSlots");
        _slotsTransform = boardSlots.transform;

        for (int column = 0; column < NUM_COLUMNS; column++) {
            for (int row = 0; row < NUM_ROWS; row++) {
                var go = Instantiate(Resources.Load("BoardSlot"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                go.transform.SetParent(_slotsTransform);
                go.transform.localPosition = new Vector3(column * ColumnSpacing, row * RowSpacing, 0) + SpawnLocation;
                var boardSlot = go.GetComponent<BoardSlot>();
                boardSlot.Coordinate = new Vector2(column, row);
                _boardSlots[column, row] = boardSlot;
            }
        }
    }

    public BoardSlot[,] GetBoardState()
    {
        return _boardSlots;
    }
    
    public void LoadBoardState(BoardSlot[,] boardSlotsToLoad)
    {
        _boardSlots = boardSlotsToLoad;
    }

    public void Highlight(Vector2 coordinate, Color color) {
        if (coordinate.x >= _boardSlots.GetLength(0) ||
            coordinate.y >= _boardSlots.GetLength(1)) { return; }

        var boardSlot = _boardSlots[(int) coordinate.x, (int) coordinate.y];
        if (boardSlot != null) {
            boardSlot.Highlight(color);
        }
    }

    public bool IsSlotOccupied(Vector2 coordinate) {
        // If the coordinate is out of bounds, return true
        if (coordinate.x >= _boardSlots.GetLength(0) ||
            coordinate.y >= _boardSlots.GetLength(1)) { return true; }

        var boardSlot = _boardSlots[(int)coordinate.x, (int)coordinate.y];
        if (boardSlot != null)
        {
            return boardSlot.IsOccupied;
        }
        return true;
    }

    public void AddToBoardSlot(List<Vector2> coordinateRange, Collectible col)
    {
        var spaceshipLocation = coordinateRange.First();
        var boardSlot = _boardSlots[(int)spaceshipLocation.x, (int)spaceshipLocation.y];
        bool instantiatedShip = false;
        foreach (var coordinate in coordinateRange)
        {
            var occupiedSlot = _boardSlots[(int)coordinate.x, (int)coordinate.y];
            if (!instantiatedShip)
            {
                occupiedSlot.Assign(col, true);
                instantiatedShip = true;
            }
            else
            {
                occupiedSlot.Assign(col);
            }
        }
    }
}
