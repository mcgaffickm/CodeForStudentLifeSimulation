using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Used to manage the income and its label
//Methods to add, remove, get income
//Each of above will automatically call set text method
public class Income : MonoBehaviour
{
    //Label
    public Text incomeText;
    public int startingAmount;

    //Simplified to an int
    private int amount;

    //Used for the income label, easily changable to other units
    public string units = "";
    public bool unitsBefore = false;

    void Start()
    {
        //Starting amount
        amount = startingAmount;
        setText();
    }

    //Adds the specified amount of money
    public void addMoney(int money)
    {
        amount += money;
        setText();
    }

    //Removes the specified amount of money
    public bool removeMoney(int money)
    {
        amount -= money;
        if (amount < -100)
        {
            return false;
        }
        else
        {
            setText();
            return true;
        }
    }
    
    //Gets the current income
    public int getIncome()
    {
        return amount;
    }

    //Sets the income to the given amount
    public void setIncome(int newAmount)
    {
        amount = newAmount;
        setText();
    }
    //Sets the text label for the income
    private void setText()
    {
        if(unitsBefore)
            incomeText.text = "Income: " + units + amount;
        else
            incomeText.text = "Income: " + amount + units;
    }
}
