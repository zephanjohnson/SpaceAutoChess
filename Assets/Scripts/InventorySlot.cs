using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public bool IsOccupied;
    public Collectible Collectible;

    public void Assign(Collectible col)
    {
        IsOccupied = true;
        Collectible = col;
        col.transform.SetParent(transform);
        col.transform.localPosition = new Vector3(0,0,0);
    }
}
