using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AllySpaceObject : SpaceObject
{
    public static int BASE_HEALTH = 50;
    public static int BULLET_VELOCITY = 1;
    public static int BULLET_FIRE_RATE_PER_SECOND = 2;
    public static int BULLET_DAMAGE = 3;
    public static int MOVEMENT_VELOCITY = 1;
    public static int MOVEMENT_RANGE = 2;

    public int BulletVelocity = BULLET_VELOCITY;
    public int BulletFireRate = BULLET_FIRE_RATE_PER_SECOND;
    public int BulletDamage = BULLET_DAMAGE;

    void Awake()
    {
        MaxHealth = BASE_HEALTH;
        CurrentHealth = MaxHealth;
        GridPosition = new Vector2(0,0);
        MovementVelocity = MOVEMENT_VELOCITY;
        MovementRange = MOVEMENT_RANGE;

        OnCurrentHealthChange += (int oldValue, int current) =>
        {
            if (current <= 0)
            {
                Destroy(this.gameObject);
            }
        };
    }

    void Start()
    {
    }

    private void OnMouseDown()
    {
        CurrentHealth -= 5;
    }
}
