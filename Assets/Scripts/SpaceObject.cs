using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpaceObject : MonoBehaviour
{
    // Current Health
    private int _currentHealth;
    public UnityAction<int> OnCurrentHealthChange;
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
                _currentHealth = value >= 0 ? value : 0;
                OnCurrentHealthChange?.Invoke(_currentHealth);
                Debug.Log($"CurrentHealth {_currentHealth}");
            }
        }
    }


    // Max Health
    private int _maxHealth;
    public UnityAction<int> OnMaxHealthChange;
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
                _maxHealth = value;
                OnMaxHealthChange?.Invoke(_maxHealth);
            }
        }
    }


    private void Awake()
    {
        Debug.Log($"setting OnCurrentHealthChange");
        OnCurrentHealthChange += (int current) =>
        {
            Debug.Log($"OnCurrentHealthChange {current}");
            if (current <= 0)
            {
                Debug.Log("Boom");
                Destroy(this.gameObject);
            }
        };
    }

}
