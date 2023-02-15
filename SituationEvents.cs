using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SituationEvents : MonoBehaviour
{
    public Text eventText;
    public GameObject status;
    public GameObject income;

    Hunger hungerManager;
    Health healthManager;
    Income incomeManager;
    GPA gpaManager;

    int numberEvent;

    private void Start()
    {
        numberEvent = 0;
        hungerManager = status.GetComponent<Hunger>();
        healthManager = status.GetComponent<Health>();
        incomeManager = income.GetComponent<Income>();
        gpaManager = status.GetComponent<GPA>();
    }

    //Called when an event occurs
    public void callEvent()
    {
        numberEvent++;
        int playerEvent = Random.Range(1,7);
        switch(playerEvent)
        {
            case 1:
                sick();
                break;
            case 2:
                carAccident();
                break;
            case 3:
                mugging();
                break;
            case 4:
                missAssignment();
                break;
            case 5:
                gasBill();
                break;
            case 6:
                panicAttack();
                break;
        }
    }

    //The three different events so far
    private void sick()
    {
        eventText.text = "Latest Event " + numberEvent + ": You caught a cold.  Your health is 1/4 and hunger is 1/2.";
        healthManager.subtractHealth(healthManager.getHealth() / 4);
        hungerManager.subtractHunger(hungerManager.getHunger() / 2);
    }

    //The three different events so far
    private void carAccident()
    {
        eventText.text = "Latest Event " + numberEvent + ": You were in a not-at-fault car accident, health is 3/4!";
        healthManager.subtractHealth((int)(healthManager.getHealth() * .75));
    }

    //The three different events so far
    //Lose income
    private void mugging()
    {
        eventText.text = "Latest Event " + numberEvent + ": You have been mugged, income is halved!";

        if(incomeManager.getIncome() > 0)
        {
            incomeManager.removeMoney(incomeManager.getIncome() / 2);
        }
    }

    //Additional events
    //Lose gpa
    private void missAssignment()
    {
        eventText.text = "Latest Event " + numberEvent + ": You forget to write in your schedule and missed an important assignment!  Your gpa is close to failing.";
        gpaManager.setGPA(2.01f);
    }

    //Additional events
    //Lose money
    private void gasBill()
    {
        eventText.text = "Latest Event " + numberEvent + ": You discover an unpaid bill and must pay it immediately.";
        incomeManager.setIncome(-90);
    }

    //Additional events
    //Lose health
    private void panicAttack()
    {
        eventText.text = "Latest Event " + numberEvent + ": The stress of school and work overwhelmed you.  You had a panic attack!";
        healthManager.subtractHealth(100 - healthManager.getHealth() + 2);
    }
}
