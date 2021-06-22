using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public string Name;
    public int Level;
    public bool IsInInventory;

    private GamestateManager gm;

    public void Awake()
    {
        gm = FindObjectOfType<GamestateManager>();
    }

    public void OnMouseDown()
    {
        if (IsInInventory)
        {
            gm.SelectCollectible(this);
        }
        else {
            gm.AddToInventory(this);
            IsInInventory = true;
        }
    }
}
