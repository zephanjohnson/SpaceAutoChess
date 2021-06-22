using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public AllySpaceObject AllySpaceObject;

    private void Start()
    {
        healthBar = GetComponent<Slider>();
        healthBar.maxValue = AllySpaceObject.MaxHealth;
        healthBar.value = AllySpaceObject.CurrentHealth;
        AllySpaceObject.OnCurrentHealthChange += SetHealth;
        AllySpaceObject.OnMaxHealthChange += (int value) => { healthBar.maxValue = value; };
    }

    public void SetHealth(int hp)
    {
        healthBar.value = hp;
    }
}
