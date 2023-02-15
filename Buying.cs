using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//The buying class
public class Buying : MonoBehaviour
{

    //Declares public objects to connect classes
    public GameObject income;
    public GameObject bars;
    public GameObject main;
    public GameObject time;
    public Slider size;
    public Toggle healthy;

    Income incomeManager;
    Health healthManager;
    Hunger hungerManager;
    MainController mainManager;
    Time timeManager;
 
    //Set up the start
    void Start()
    {
        incomeManager = income.GetComponent<Income>();
        healthManager = bars.GetComponent<Health>();
        hungerManager = bars.GetComponent<Hunger>();
        mainManager = main.GetComponent<MainController>();
        timeManager = time.GetComponent<Time>();
    }

    //Makes a purchase, used by buttons 
    public void payBill()
    {
        if (!incomeManager.removeMoney(400))
        {
            mainManager.loseState(3);
        }

        timeManager.setBillStatus(false);
        timeManager.PlusHours(1);
    }
   
    //Makes a purchase, used by buttons 
    public void buyFood()
    {
        int money;
        int healthChange;
        
        //Health option
        if (healthy.isOn)
        {
            money = 3 * 6 * (int) size.value;
            healthChange = 3 * 2 * (int)size.value;
        }
        else
        {
            money = 3 * 4 * (int)size.value;
            healthChange = 3 * 1;
        }

        //Changes to income and health
        if (!incomeManager.removeMoney(money))
        {
            mainManager.loseState(3);
        }

        timeManager.PlusHours(3);
        healthManager.addHealth(Random.Range(2, 6) * healthChange);
        hungerManager.addHunger(Random.Range(5, 20) * (int)size.value * 3);
    }
}
