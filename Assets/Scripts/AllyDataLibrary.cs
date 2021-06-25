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
    public float BulletVelocity;
    public int BulletFireRatePerSecond;
    public int BulletDamage;
    public float MovementVelocity;
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
                IconResourcePath = IconResourcePath,
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
            ResourcePath = "Ships/Red Ship",
            IconResourcePath = "AllyIcons/red ally 1 star",
            BaseHealth = 50,
            BulletVelocity = 1,
            BulletFireRatePerSecond = 1,
            BulletDamage = 2,
            MovementVelocity = 0.75f,
            MovementRange = 2,
            Color = Color.red,
        }},
        {"RedSpaceship_l2", new AllyData {
            Key = "RedSpaceship_l2",
            Name = "Red Spaceship 2",
            Level = 2,
            Type = CollectibleType.Ally,
            ResourcePath = "Ships/Red Ship",
            IconResourcePath = "AllyIcons/red ally 2 star",
            BaseHealth = 160,
            BulletVelocity = 1,
            BulletFireRatePerSecond = 2,
            BulletDamage = 2,
            MovementVelocity = 0.85f,
            MovementRange = 2,
            Color = Color.red,
        }},
        {"RedSpaceship_l3", new AllyData {
            Key = "RedSpaceship_l3",
            Name = "Red Spaceship 3",
            Level = 2,
            Type = CollectibleType.Ally,
            ResourcePath = "Ships/Red Ship",
            IconResourcePath = "AllyIcons/red ally 3 star",
            BaseHealth = 500,
            BulletVelocity = 1,
            BulletFireRatePerSecond = 2,
            BulletDamage = 2,
            MovementVelocity = 0.95f,
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
            ResourcePath = "Ships/Orange Ship",
            IconResourcePath = "AllyIcons/orange ally 1 star",
            BaseHealth = 100,
            BulletVelocity = 0.6f,
            BulletFireRatePerSecond = 2,
            BulletDamage = 2,
            MovementVelocity = 0.5f,
            MovementRange = 1,
            Color = new Color(1f, 0.5f, 0),
        }},
        {"OrangeSpaceship_l2", new AllyData {
            Key = "OrangeSpaceship_l2",
            Name = "Orange Spaceship 2",
            Level = 2,
            Type = CollectibleType.Ally,
            ResourcePath = "Ships/Orange Ship",
            IconResourcePath = "AllyIcons/orange ally 2 star",
            BaseHealth = 350,
            BulletVelocity = 0.6f,
            BulletFireRatePerSecond = 2,
            BulletDamage = 2,
            MovementVelocity = 0.5f,
            MovementRange = 1,
            Color = new Color(1f, 0.5f, 0),
        }},
        {"OrangeSpaceship_l3", new AllyData {
            Key = "OrangeSpaceship_l3",
            Name = "Orange Spaceship 3",
            Level = 2,
            Type = CollectibleType.Ally,
            ResourcePath = "Ships/Orange Ship",
            IconResourcePath = "AllyIcons/orange ally 3 star",
            BaseHealth = 1200,
            BulletVelocity = 0.6f,
            BulletFireRatePerSecond = 3,
            BulletDamage = 2,
            MovementVelocity = 0.5f,
            MovementRange = 1,
            Color = new Color(1f, 0.5f, 0),
        }},

        // Yellow Spaceship
        {"YellowSpaceship_l1", new AllyData {
            Key = "YellowSpaceship_l1",
            Name = "Yellow Spaceship 1",
            UpgradeKey = "YellowSpaceship_l2",
            Level = 1,
            Type = CollectibleType.Ally,
            ResourcePath = "Ships/Yellow Ship",
            IconResourcePath = "AllyIcons/yellow ally 1 star",
            BaseHealth = 30,
            BulletVelocity = 1.2f,
            BulletFireRatePerSecond = 1,
            BulletDamage = 2,
            MovementVelocity = 2,
            MovementRange = 3,
            Color = Color.yellow,
        }},
        {"YellowSpaceship_l2", new AllyData {
            Key = "YellowSpaceship_l2",
            Name = "Yellow Spaceship 2",
            Level = 2,
            Type = CollectibleType.Ally,
            ResourcePath = "Ships/Yellow Ship",
            IconResourcePath = "AllyIcons/yellow ally 2 star",
            BaseHealth = 100,
            BulletVelocity = 1.2f,
            BulletFireRatePerSecond = 1,
            BulletDamage = 2,
            MovementVelocity = 2,
            MovementRange = 3,
            Color = Color.yellow,
        }},
        {"YellowSpaceship_l3", new AllyData {
            Key = "YellowSpaceship_l3",
            Name = "Yellow Spaceship 3",
            Level = 2,
            Type = CollectibleType.Ally,
            ResourcePath = "Ships/Yellow Ship",
            IconResourcePath = "AllyIcons/yellow ally 3 star",
            BaseHealth = 320,
            BulletVelocity = 1.2f,
            BulletFireRatePerSecond = 1,
            BulletDamage = 2,
            MovementVelocity = 2,
            MovementRange = 3,
            Color = Color.yellow,
        }},

        // Green Spaceship
        {"GreenSpaceship_l1", new AllyData {
            Key = "GreenSpaceship_l1",
            Name = "Green Spaceship 1",
            UpgradeKey = "GreenSpaceship_l2",
            Level = 1,
            Type = CollectibleType.Ally,
            ResourcePath = "Ships/Green Ship",
            IconResourcePath = "AllyIcons/green ally 1 star",
            BaseHealth = 50,
            BulletVelocity = 1,
            BulletFireRatePerSecond = 1,
            BulletDamage = 2,
            MovementVelocity = 2,
            MovementRange = 4,
            Color = Color.green,
        }},
        {"GreenSpaceship_l2", new AllyData {
            Key = "GreenSpaceship_l2",
            Name = "Green Spaceship 2",
            Level = 2,
            Type = CollectibleType.Ally,
            ResourcePath = "Ships/Green Ship",
            IconResourcePath = "AllyIcons/green ally 2 star",
            BaseHealth = 110,
            BulletVelocity = 1,
            BulletFireRatePerSecond = 1,
            BulletDamage = 2,
            MovementVelocity = 2,
            MovementRange = 4,
            Color = Color.green,
        }},
        {"GreenSpaceship_l3", new AllyData {
            Key = "GreenSpaceship_l3",
            Name = "Green Spaceship 3",
            UpgradeKey = "GreenSpaceship_l3",
            Level = 1,
            Type = CollectibleType.Ally,
            ResourcePath = "Ships/Green Ship",
            IconResourcePath = "AllyIcons/green ally 3 star",
            BaseHealth = 50,
            BulletVelocity = 1,
            BulletFireRatePerSecond = 1,
            BulletDamage = 2,
            MovementVelocity = 2,
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
            ResourcePath = "Ships/Blue Ship",
            IconResourcePath = "AllyIcons/blue ally 1 star",
            BaseHealth = 50,
            BulletVelocity = 1,
            BulletFireRatePerSecond = 1,
            BulletDamage = 2,
            MovementVelocity = 1.1f,
            MovementRange = 2,
            Color = Color.blue,
        }},
        {"BlueSpaceship_l2", new AllyData {
            Key = "BlueSpaceship_l2",
            Name = "Blue Spaceship 2",
            Level = 2,
            Type = CollectibleType.Ally,
            ResourcePath = "Ships/Blue Ship",
            IconResourcePath = "AllyIcons/blue ally 2 star",
            BaseHealth = 110,
            BulletVelocity = 1,
            BulletFireRatePerSecond = 1,
            BulletDamage = 2,
            MovementVelocity = 1.1f,
            MovementRange = 2,
            Color = Color.blue,
        }},
        {"BlueSpaceship_l3", new AllyData {
            Key = "BlueSpaceship_l3",
            Name = "Blue Spaceship 3",
            Level = 2,
            Type = CollectibleType.Ally,
            ResourcePath = "Ships/Blue Ship",
            IconResourcePath = "AllyIcons/blue ally 3 star",
            BaseHealth = 110,
            BulletVelocity = 1,
            BulletFireRatePerSecond = 1,
            BulletDamage = 2,
            MovementVelocity = 1.1f,
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
            ResourcePath = "Ships/Purple Ship",
            IconResourcePath = "AllyIcons/purple ally 1 star",
            BaseHealth = 50,
            BulletVelocity = 1,
            BulletFireRatePerSecond = 1,
            BulletDamage = 2,
            MovementVelocity = 0.3f,
            MovementRange = 2,
            Color = new Color(0.502f, 0, 0.502f),
        }},
        {"PurpleSpaceship_l2", new AllyData {
            Key = "PurpleSpaceship_l2",
            Name = "Purple Spaceship 2",
            Level = 2,
            Type = CollectibleType.Ally,
            ResourcePath = "Ships/Purple Ship",
            IconResourcePath = "AllyIcons/purple ally 2 star",
            BaseHealth = 110,
            BulletVelocity = 1,
            BulletFireRatePerSecond = 1,
            BulletDamage = 2,
            MovementVelocity = 0.3f,
            MovementRange = 2,
            Color = new Color(0.502f, 0, 0.502f),
        }},
        {"PurpleSpaceship_l3", new AllyData {
            Key = "PurpleSpaceship_l3",
            Name = "Purple Spaceship 3",
            Level = 2,
            Type = CollectibleType.Ally,
            ResourcePath = "Ships/Purple Ship",
            IconResourcePath = "AllyIcons/purple ally 3 star",
            BaseHealth = 110,
            BulletVelocity = 1,
            BulletFireRatePerSecond = 1,
            BulletDamage = 2,
            MovementVelocity = 0.3f,
            MovementRange = 2,
            Color = new Color(0.502f, 0, 0.502f),
        }},
    };
}