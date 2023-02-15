using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Health class to control the health bar and the 
public class Health : MonoBehaviour
{
    //Declare variables
    public Slider healthBar;

    int totalHealth;

    void Start()
    {
        totalHealth = 100;
        healthBar.maxValue = 100;
        setHealthBar();
    }

    //Gets the current health value
    public int getHealth()
    {
        return totalHealth;
    }

    //Adds health
    public void addHealth(int health)
    {
        totalHealth += health;
        if(totalHealth > 100)
        {
            totalHealth = 100; //Max health
        }
        setHealthBar();
    }

    //Returns true if still above 0, false if below
    public bool subtractHealth(int health)
    {
        totalHealth -= health;
        if (totalHealth <= 0)
        {
            return false;
        }
        else
        {
            setHealthBar();
            return true;
        }
    }

    
    //Sets the current health bar
    private void setHealthBar()
    {
        healthBar.value = totalHealth;
    }
}
