using System.Collections.Generic;
using UnityEngine;

public struct AllyData
{
    public string Name;
    public int Level;
    public CollectibleType Type;
    public string ResourcePath;
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
                Level = Level,
                Type = Type,
                ResourcePath = ResourcePath,
            };
        }
    }
}

public class AllyDataLibrary
{
    public static Dictionary<string, AllyData> Allies = new Dictionary<string, AllyData>
    {
        {"Red Spaceship", new AllyData {
            Name = "Red Spaceship",
            Level = 1,
            Type = CollectibleType.Ally,
            ResourcePath = "Red Spaceship",
            BaseHealth = 50,
            BulletVelocity = 1,
            BulletFireRatePerSecond = 3,
            BulletDamage = 2,
            MovementVelocity = 1,
            MovementRange = 2,
            Color = Color.red,
        }},
        {"Orange Spaceship", new AllyData {
            Name = "Orange Spaceship",
            Level = 1,
            Type = CollectibleType.Ally,
            ResourcePath = "Orange Spaceship",
            BaseHealth = 50,
            BulletVelocity = 1,
            BulletFireRatePerSecond = 3,
            BulletDamage = 2,
            MovementVelocity = 1,
            MovementRange = 2,
            Color = new Color(1f, 0.5f, 0),
        }},
        {"Yellow Spaceship", new AllyData {
            Name = "Yellow Spaceship",
            Level = 1,
            Type = CollectibleType.Ally,
            ResourcePath = "Yellow Spaceship",
            BaseHealth = 50,
            BulletVelocity = 1,
            BulletFireRatePerSecond = 3,
            BulletDamage = 2,
            MovementVelocity = 1,
            MovementRange = 1,
            Color = Color.yellow,
        }},
        {"Green Spaceship", new AllyData {
            Name = "Green Spaceship",
            Level = 1,
            Type = CollectibleType.Ally,
            ResourcePath = "Green Spaceship",
            BaseHealth = 50,
            BulletVelocity = 1,
            BulletFireRatePerSecond = 3,
            BulletDamage = 2,
            MovementVelocity = 1,
            MovementRange = 4,
            Color = Color.green,
        }},
        {"Blue Spaceship", new AllyData {
            Name = "Blue Spaceship",
            Level = 1,
            Type = CollectibleType.Ally,
            ResourcePath = "Blue Spaceship",
            BaseHealth = 50,
            BulletVelocity = 1,
            BulletFireRatePerSecond = 3,
            BulletDamage = 2,
            MovementVelocity = 1,
            MovementRange = 2,
            Color = Color.blue,
        }},
        {"Purple Spaceship", new AllyData {
            Name = "Purple Spaceship",
            Level = 1,
            Type = CollectibleType.Ally,
            ResourcePath = "Purple Spaceship",
            BaseHealth = 50,
            BulletVelocity = 1,
            BulletFireRatePerSecond = 3,
            BulletDamage = 2,
            MovementVelocity = 1,
            MovementRange = 1,
            Color = new Color(0.502f, 0, 0.502f),
        }},
    };
}