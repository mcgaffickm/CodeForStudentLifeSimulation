using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The buying class
public class Actions : MonoBehaviour
{

    public Schedule scheduleObject;

    //Declares public objects to connect classes
    public GameObject income;
    public GameObject bars;
    public GameObject time;
    public GameObject main;

    Income incomeManager;
    Health healthManager;
    Time timeManager;
    GPA gpaManager;
    Hunger hungerManager;
    MainController mainManager;


    //Set up the start
    void Start()
    {
        scheduleObject = FindObjectOfType<Schedule>();

        incomeManager = income.GetComponent<Income>();
        healthManager = bars.GetComponent<Health>();
        timeManager = time.GetComponent<Time>();
        gpaManager = bars.GetComponent<GPA>();
        hungerManager = bars.GetComponent<Hunger>();
        mainManager = main.GetComponent<MainController>();
    }

    //Does an action, used by buttons 
    //Rest action, adds health
    public void doSleep()
    {
        timeManager.setSleep(true);
        //Hunger happens due to time
     
        healthManager.addHealth(8 * UnityEngine.Random.Range(3, 5));
        timeManager.PlusHours(8);
        timeManager.setHoursAwake(0);
        timeManager.setSleep(false);
    }

    //Does a leisure action, used by buttons
    //Adds health, shorter time than sleep
    public void doLeisure()
    {
        healthManager.addHealth(2 * UnityEngine.Random.Range(5, 20));
        timeManager.PlusHours(2);
    }

    //Does an action, used by buttons 
    //Homework action, lowers mental health, raises gpa, but rate is based on health
    public void doStudy()
    {
      
        if (!healthManager.subtractHealth(4 * UnityEngine.Random.Range(2, 10)))
        {
            mainManager.loseState(2);
        }

        //Calculation for gpa, less for lower health
        float additionalGpa = .1f * 3f - ((100 - healthManager.getHealth()) / 200);
        if (additionalGpa < 0)
        {
            additionalGpa = 0;
        }
        gpaManager.addGPA(additionalGpa);

        //Hunger happens due to time
        timeManager.PlusHours(4);
    }

    //Does an action, used by buttons 
    //Does work, minimum wage + tips based on health, lowers health
    public void doWork()
    {
        //Hunger happens due to time
        if (scheduleObject.currentEvent != null && scheduleObject.currentEvent.isJob == true)
        {
            timeManager.PlusHours(scheduleObject.currentEvent.hoursLeft);
            if (!healthManager.subtractHealth(scheduleObject.currentEvent.hoursLeft * UnityEngine.Random.Range(2, 5)))
            {
                mainManager.loseState(2);
            }
            //Calculation for money, tips is based on health and $8 is hourly rate
            int tips = UnityEngine.Random.Range(10, healthManager.getHealth() + 10);
            incomeManager.addMoney(tips + scheduleObject.currentEvent.hoursLeft * 9);
        }
    }

    //Does an action, used by buttons 
    //Could possibly raise gpa
    public void doAssignment()
    {
        //Completes the assignment
        timeManager.setAssignmentStatus(false);
        timeManager.btnAssignment.SetActive(false);

        //Hunger happens due to time
        timeManager.PlusHours(5);

        if (!healthManager.subtractHealth(UnityEngine.Random.Range(2, 10)))
        {
            mainManager.loseState(2);
        }
        
        //Fail test if below 50 health, pass if above
        if(healthManager.getHealth() > 50)
        {
            gpaManager.addGPA(UnityEngine.Random.Range(0,.5f));
        }
        else
        {
            if (!gpaManager.subtractGPA(UnityEngine.Random.Range(.3f, .5f)))
            {
                mainManager.loseState(4);
            }
        }
    }

    //Does an action, used by buttons 
    //Raise gpa, lower health
    public void doClass()
    {

        timeManager.wentToClass();

        //Hunger happens due to time
        if (scheduleObject.currentEvent != null && scheduleObject.currentEvent.isClass == true)
        {
           
            if (gpaManager.getGPA() > 3.0)
            {
                if (!healthManager.subtractHealth(UnityEngine.Random.Range(2, 5)))
                {
                    mainManager.loseState(2);
                }
            }
            else
            {
                if (!healthManager.subtractHealth(UnityEngine.Random.Range(10, 20)))
                {
                    mainManager.loseState(2);
                }
            }

            if (healthManager.getHealth() > 40)
            {
                gpaManager.addGPA(UnityEngine.Random.Range(.2f, .5f));
            }
            else
            {
                gpaManager.addGPA(UnityEngine.Random.Range(0f, .1f));
            }

            timeManager.PlusHours(scheduleObject.currentEvent.hoursLeft);

        }
    }
}
