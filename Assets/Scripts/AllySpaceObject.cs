using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AllySpaceObject : SpaceObject
{
    static int BASE_HEALTH = 50;


    void Awake()
    {
        MaxHealth = BASE_HEALTH;
        CurrentHealth = MaxHealth;

        OnCurrentHealthChange += (int current) =>
        {
            if (current <= 0)
            {
                Destroy(this.gameObject);
            }
        };
    }

    void Start()
    {
        Debug.Log("AllySpaceStart");
    }

    private void OnMouseDown()
    {
        Debug.Log("Ally Click");
        CurrentHealth -= 5;
    }
}
