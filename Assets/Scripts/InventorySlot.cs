using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    [SerializeProperty("IsOccupied")]
    public bool isOccupied;
    public bool IsOccupied { get { return _isOccupied; } }
    private bool _isOccupied;
    public Collectible Collectible;

    public void Assign(Collectible col)
    {
        _isOccupied = true;
        Collectible = col;
        col.transform.SetParent(transform);
        col.transform.SetAsFirstSibling();
        col.transform.localPosition = new Vector3(0,0,0);
    }

    public void Release()
    {
        _isOccupied = false;
        Destroy(Collectible.gameObject);
    }
}
