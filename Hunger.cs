using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This will attack the health every turn if not satisified
public class Hunger : MonoBehaviour
{

    //Declares the variables
    public Slider hungerBar;

    public GameObject health;
    public GameObject main;


    int currentHunger;
    Health healthManager;
    MainController mainManager;

    void Start()
    {
        currentHunger = Random.Range(80, 100);
        healthManager = health.GetComponent<Health>();
        mainManager = main.GetComponent<MainController>();
        hungerBar.maxValue = 100;
        setHungerBar();
    }

    //Called at the end of a turn
    public void checkHunger()
    {
        int hunger = 100 - currentHunger;
        attackHealth(hunger); //Attacks health based on a total out of 100, so pure starvation kills health
    }

    //returns current hunger
    public int getHunger()
    {
        return currentHunger;
    }


    //Attacks the health based on the hunger level
    void attackHealth(int healthRemoved)
    {
        if(!healthManager.subtractHealth(Random.Range(0, healthRemoved) / 3))
        {
            mainManager.loseState(2);
        }
    }

    //Adds hunger, good thing, when eating
    public void addHunger(int hunger)
    {
        currentHunger += hunger;
        if (currentHunger > 100)
        {
            currentHunger = 100; //Max health
        }
        setHungerBar();
    }

    //Called during actions
    public bool subtractHunger(int hunger)
    {
        currentHunger -= hunger;
        if (currentHunger <= 0)
        {
            return false;
        }
        else
        {
            setHungerBar();
            return true;
        }
    }

    //Sets the hunger bar
    private void setHungerBar()
    {
        hungerBar.value = currentHunger;
    }
}
