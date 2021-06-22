using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpaceObject : MonoBehaviour
{
    public Vector2 GridPosition = new Vector2();
    public int MovementVelocity =1;
    public int MovementRange = 1;

    // Current Health
    private int _currentHealth;
    public UnityAction<int, int> OnCurrentHealthChange;
    public int CurrentHealth
    {
        get
        {
            return _currentHealth;
        }
        set
        {
            if (value != _currentHealth)
            {
                int oldValue = _currentHealth;
                _currentHealth = value >= 0 ? value : 0;
                OnCurrentHealthChange?.Invoke(oldValue, _currentHealth);
            }
        }
    }


    // Max Health
    private int _maxHealth;
    public UnityAction<int, int> OnMaxHealthChange;
    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            if (value != _maxHealth)
            {
                int oldValue = _maxHealth;
                _maxHealth = value;
                OnMaxHealthChange?.Invoke(oldValue, _maxHealth);
            }
        }
    }
}
