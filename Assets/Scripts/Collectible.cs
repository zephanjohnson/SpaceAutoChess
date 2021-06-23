using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum CollectibleType
{
    Ally,
    Item,
    Gold,
    None,
}

public struct CollectibleData
{
    public string Name;
    public int Level;
    public CollectibleType Type;
    public string ResourcePath;
}

public class Collectible : MonoBehaviour
{
    public CollectibleData Data;
    public bool IsInInventory;
    private GamestateManager gm;

    public void Awake()
    {
        // Probably want to assign this later, instead of using FindObjectOfType when these get instantiated during autoplay
        gm = FindObjectOfType<GamestateManager>();
        int rndFloat = Random.Range(0, AllyDataLibrary.Allies.Count);
        string spaceShipName = GetRandomAllyName();
        var allyData = AllyDataLibrary.Allies[spaceShipName];
        PopulateData(allyData.Data);
        var sprite = Resources.Load<Sprite>(allyData.IconResourcePath);
        GetComponent<SpriteRenderer>().sprite = sprite;
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void PopulateData(CollectibleData data) {
        Data = data;
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

    private string GetRandomAllyName() {

        int rndFloat = Random.Range(0, AllyDataLibrary.Allies.Count);
        var allies = AllyDataLibrary.Allies.Values.ToArray();
        return allies[rndFloat].Name;
    }
}
