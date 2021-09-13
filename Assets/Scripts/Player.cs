using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(TankController))]
public class Player : MonoBehaviour
{
    [SerializeField] Text healthText;
    //[SerializeField] Text treasureText;
    [SerializeField] int _maxHealth = 3;
    private int treasureCount = 0;
    int _currentHealth;
    private bool isInvincible;
    

    public void setInvincible(bool b)
    {
        isInvincible = b;
    }

    TankController _tankController;
    private void Awake()
    {
        _tankController = GetComponent<TankController>();
        isInvincible = false;
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void IncreaseTreasure()
    {
        //treasureCount += 1;
        //treasureText.text = "Treasure: " + treasureCount;
    }
    public void IncreaseHealth(int amount)
    {
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        _currentHealth += amount;
        healthText.text = "Health: " + _currentHealth;
    }

    public void DecreaseHealth(int amount)
    {
        if (isInvincible == false)
        {
            _currentHealth -= amount;
            healthText.text = "Health: " + _currentHealth;

            if (_currentHealth <= 0)
            {
                Kill();
            }
        }
    }

    public void Kill()
    {
        _currentHealth = 0;
        healthText.text = "Health: " + _currentHealth;
        gameObject.SetActive(false);
        //play partciles & sounds
    }
}
