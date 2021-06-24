using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    [SerializeProperty("IsOccupied")] 
    public bool IsOccupied;
    public Collectible Collectible;
    public int Index;

    public void Assign(Collectible col)
    {
        IsOccupied = true;
        Collectible = col;
        col.transform.SetParent(transform);
        col.transform.SetAsFirstSibling();
        col.transform.localPosition = new Vector3(0,0,0);
    }

    public void Release()
    {
        IsOccupied = false;
        Destroy(Collectible.gameObject);
    }
}
