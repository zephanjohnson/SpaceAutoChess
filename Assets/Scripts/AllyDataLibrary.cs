using System.Collections.Generic;
using UnityEngine;

public struct AllyData
{
    public string Name;
    public string Key;
    public string UpgradeKey;
    public int Level;
    public CollectibleType Type;
    public string ResourcePath;
    public string IconResourcePath;
    public int BaseHealth;
    public int BulletVelocity;
    public int BulletFireRatePerSecond;
    public int BulletDamage;
    public int MovementVelocity;
    public int MovementRange;
    public Color Color;

    public CollectibleData Data
    {
        get {
            return new CollectibleData {
                Name = Name,
                Key = Key,
                UpgradeKey = UpgradeKey,
                Level = Level,
                Type = Type,
                ResourcePath = ResourcePath,
            };
        }
    }
}

public class AllyDataLibrary
{
    public static string[] LevelOneAllyKeys = new string[] {
        "RedSpaceship_l1",
        "OrangeSpaceship_l1",
        "YellowSpaceship_l1",
        "GreenSpaceship_l1",
        "BlueSpaceship_l1",
        "PurpleSpaceship_l1",
    };

    public static string[] LevelTwoAllyKeys = new string[] {
        "RedSpaceship_l2",
        "OrangeSpaceship_l2",
        "YellowSpaceship_l2",
        "GreenSpaceship_l2",
        "BlueSpaceship_l2",
        "PurpleSpaceship_l2",
    };

    public static Dictionary<string, AllyData> Allies = new Dictionary<string, AllyData>
    {
        // Red Spaceship
        {"RedSpaceship_l1", new AllyData {
            Key = "RedSpaceship_l1",
            Name = "Red Spaceship 1",
            UpgradeKey = "RedSpaceship_l2",
            Level = 1,
            Type = CollectibleType.Ally,
            ResourcePath = "Red Spaceship",
            IconResourcePath = "AllyIcons/Red Ally",
            BaseHealth = 50,
            BulletVelocity = 1,
            BulletFireRatePerSecond = 3,
            BulletDamage = 2,
            MovementVelocity = 1,
            MovementRange = 2,
            Color = Color.red,
        }},
        {"RedSpaceship_l2", new AllyData {
            Key = "RedSpaceship_l2",
            Name = "Red Spaceship 2",
            Level = 2,
            Type = CollectibleType.Ally,
            ResourcePath = "Red Spaceship",
            IconResourcePath = "AllyIcons/Red Ally",
            BaseHealth = 110,
            BulletVelocity = 1,
            BulletFireRatePerSecond = 3,
            BulletDamage = 2,
            MovementVelocity = 1,
            MovementRange = 2,
            Color = Color.red,
        }},

        // Orange Spaceship
        {"OrangeSpaceship_l1", new AllyData {
            Key = "OrangeSpaceship_l1",
            Name = "Orange Spaceship 1",
            UpgradeKey = "OrangeSpaceship_l2",
            Level = 1,
            Type = CollectibleType.Ally,
            ResourcePath = "Orange Spaceship",
            IconResourcePath = "AllyIcons/Orange Ally",
            BaseHealth = 50,
            BulletVelocity = 1,
            BulletFireRatePerSecond = 3,
            BulletDamage = 2,
            MovementVelocity = 1,
            MovementRange = 2,
            Color = new Color(1f, 0.5f, 0),
        }},
        {"OrangeSpaceship_l2", new AllyData {
            Key = "OrangeSpaceship_l2",
            Name = "Orange Spaceship 2",
            Level = 2,
            Type = CollectibleType.Ally,
            ResourcePath = "Orange Spaceship",
            IconResourcePath = "AllyIcons/Orange Ally",
            BaseHealth = 110,
            BulletVelocity = 1,
            BulletFireRatePerSecond = 3,
            BulletDamage = 2,
            MovementVelocity = 1,
            MovementRange = 2,
            Color = new Color(1f, 0.5f, 0),
        }},

        // Yellow Spaceship
        {"YellowSpaceship_l1", new AllyData {
            Key = "YellowSpaceship_l1",
            Name = "Yellow Spaceship 1",
            UpgradeKey = "YellowSpaceship_l2",
            Level = 1,
            Type = CollectibleType.Ally,
            ResourcePath = "Yellow Spaceship",
            IconResourcePath = "AllyIcons/Yellow Ally",
            BaseHealth = 50,
            BulletVelocity = 1,
            BulletFireRatePerSecond = 3,
            BulletDamage = 2,
            MovementVelocity = 1,
            MovementRange = 1,
            Color = Color.yellow,
        }},
        {"YellowSpaceship_l2", new AllyData {
            Key = "YellowSpaceship_l2",
            Name = "Yellow Spaceship 2",
            Level = 2,
            Type = CollectibleType.Ally,
            ResourcePath = "Yellow Spaceship",
            IconResourcePath = "AllyIcons/Yellow Ally",
            BaseHealth = 110,
            BulletVelocity = 1,
            BulletFireRatePerSecond = 3,
            BulletDamage = 2,
            MovementVelocity = 1,
            MovementRange = 1,
            Color = Color.yellow,
        }},

        // Green Spaceship
        {"GreenSpaceship_l1", new AllyData {
            Key = "GreenSpaceship_l1",
            Name = "Green Spaceship 1",
            UpgradeKey = "GreenSpaceship_l2",
            Level = 1,
            Type = CollectibleType.Ally,
            ResourcePath = "Green Spaceship",
            IconResourcePath = "AllyIcons/Green Ally",
            BaseHealth = 50,
            BulletVelocity = 1,
            BulletFireRatePerSecond = 3,
            BulletDamage = 2,
            MovementVelocity = 1,
            MovementRange = 4,
            Color = Color.green,
        }},
        {"GreenSpaceship_l2", new AllyData {
            Key = "GreenSpaceship_l2",
            Name = "Green Spaceship 2",
            Level = 2,
            Type = CollectibleType.Ally,
            ResourcePath = "Green Spaceship",
            IconResourcePath = "AllyIcons/Green Ally",
            BaseHealth = 110,
            BulletVelocity = 1,
            BulletFireRatePerSecond = 3,
            BulletDamage = 2,
            MovementVelocity = 1,
            MovementRange = 4,
            Color = Color.green,
        }},

        // Blue Spaceship
        {"BlueSpaceship_l1", new AllyData {
            Key = "BlueSpaceship_l1",
            Name = "Blue Spaceship 1",
            UpgradeKey = "BlueSpaceship_l2",
            Level = 1,
            Type = CollectibleType.Ally,
            ResourcePath = "Blue Spaceship",
            IconResourcePath = "AllyIcons/Blue Ally",
            BaseHealth = 50,
            BulletVelocity = 1,
            BulletFireRatePerSecond = 3,
            BulletDamage = 2,
            MovementVelocity = 1,
            MovementRange = 2,
            Color = Color.blue,
        }},
        {"BlueSpaceship_l2", new AllyData {
            Key = "BlueSpaceship_l2",
            Name = "Blue Spaceship 2",
            Level = 2,
            Type = CollectibleType.Ally,
            ResourcePath = "Blue Spaceship",
            IconResourcePath = "AllyIcons/Blue Ally",
            BaseHealth = 110,
            BulletVelocity = 1,
            BulletFireRatePerSecond = 3,
            BulletDamage = 2,
            MovementVelocity = 1,
            MovementRange = 2,
            Color = Color.blue,
        }},

        // Purple Spaceship
        {"PurpleSpaceship_l1", new AllyData {
            Key = "PurpleSpaceship_l1",
            Name = "Purple Spaceship 1",
            UpgradeKey = "PurpleSpaceship_l2",
            Level = 1,
            Type = CollectibleType.Ally,
            ResourcePath = "Purple Spaceship",
            IconResourcePath = "AllyIcons/Purple Ally",
            BaseHealth = 50,
            BulletVelocity = 1,
            BulletFireRatePerSecond = 3,
            BulletDamage = 2,
            MovementVelocity = 1,
            MovementRange = 1,
            Color = new Color(0.502f, 0, 0.502f),
        }},
        {"PurpleSpaceship_l2", new AllyData {
            Key = "PurpleSpaceship_l2",
            Name = "Purple Spaceship 2",
            Level = 2,
            Type = CollectibleType.Ally,
            ResourcePath = "Purple Spaceship",
            IconResourcePath = "AllyIcons/Purple Ally",
            BaseHealth = 110,
            BulletVelocity = 1,
            BulletFireRatePerSecond = 3,
            BulletDamage = 2,
            MovementVelocity = 1,
            MovementRange = 1,
            Color = new Color(0.502f, 0, 0.502f),
        }},
    };
}