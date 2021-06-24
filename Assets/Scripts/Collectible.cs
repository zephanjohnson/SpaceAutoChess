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
    public string Key;
    public string UpgradeKey;
    public int Level;
    public CollectibleType Type;
    public string IconResourcePath;
}

public class Collectible : MonoBehaviour
{
    [SerializeProperty("CollectibleName")]
    public string collectibleName;
    public string CollectibleName { get { return Data.Name; } }
    public CollectibleData Data;
    public bool IsInInventory;
    private GamestateManager gm;

    public void Awake()
    {
        // Probably want to assign this later, instead of using FindObjectOfType when these get instantiated during autoplay
        gm = FindObjectOfType<GamestateManager>();
        int rndFloat = Random.Range(0, AllyDataLibrary.Allies.Count);
        string allyKey = GetRandomAllyKey();
        var allyData = AllyDataLibrary.Allies[allyKey];
        PopulateData(allyData.Data);
        var sprite = Resources.Load<Sprite>(allyData.IconResourcePath);
        GetComponent<SpriteRenderer>().sprite = sprite;
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void PopulateData(CollectibleData data) {
        Data = data;
    }

    public void Upgrade()
    {
        var allyData = AllyDataLibrary.Allies[Data.UpgradeKey];
        Data = allyData.Data;
        var sprite = Resources.Load<Sprite>(allyData.IconResourcePath);
        GetComponent<SpriteRenderer>().sprite = sprite;
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

    private string GetRandomAllyKey() {

        int rndFloat = Random.Range(0, AllyDataLibrary.LevelOneAllyKeys.Length);
        var allieKeys = AllyDataLibrary.LevelOneAllyKeys;
        return allieKeys[rndFloat];
    }
}
