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

public enum CollectibleLocation
{
    None,
    Field,
    Inventory,
    Board,
}

public struct CollectibleData
{
    public string Name;
    public string Key;
    public string UpgradeKey;
    public int Level;
    public CollectibleType Type;
    public string IconResourcePath;
    public string ResourcePath;

    public CollectibleData GetUpgrade()
    {
        if(UpgradeKey == null || UpgradeKey.Length == 0){ return this; }

        var allyData = AllyDataLibrary.Allies[UpgradeKey];
        return allyData.Data;
    }
}

public class Collectible : MonoBehaviour
{
    [SerializeProperty("CollectibleName")]
    public string collectibleName;
    public string CollectibleName { get { return Data.Name; } }
    public CollectibleData Data;
    public CollectibleLocation Location;
    private GamestateManager gm;

    public void Awake()
    {
        // Probably want to assign this later, instead of using FindObjectOfType when these get instantiated during autoplay
        gm = FindObjectOfType<GamestateManager>();
        int rndFloat = Random.Range(0, AllyDataLibrary.Allies.Count);
        AllyData allyData = GetRandomAllyData();
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
        if (Location == CollectibleLocation.Field || Location == CollectibleLocation.None)
        {
            gm.AddToInventory(this);
            Destroy(this.gameObject);
        }
    }

    public static AllyData GetRandomAllyData() {

        int rndFloat = Random.Range(0, AllyDataLibrary.LevelOneAllyKeys.Length);
        var allieKeys = AllyDataLibrary.LevelOneAllyKeys;
        var allyKey = allieKeys[rndFloat];
        var allyData = AllyDataLibrary.Allies[allyKey];

        return allyData;
    }
}
