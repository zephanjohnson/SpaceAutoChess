using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class BoardManager : MonoBehaviour
{
    public static int NUM_COLUMNS = 3;
    public static int NUM_ROWS = 6;

    private BoardSlot[,] _boardSlots = new BoardSlot[NUM_COLUMNS, NUM_ROWS];

    public void Awake()
    {
        for (int column = 0; column < NUM_COLUMNS; column++) {
            for (int row = 0; row < NUM_ROWS; row++) {
                var go = Instantiate(Resources.Load("BoardSlot"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                go.transform.SetParent(this.transform);
                go.transform.localPosition = new Vector3(column * 1.2f, row * 1.2f, 0);
                var boardSlot = go.GetComponent<BoardSlot>();
                boardSlot.Coordinate = new Vector2(column, row);
                _boardSlots[column, row] = boardSlot;
            }
        }
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
}
