using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Time : MonoBehaviour
{


    public int currentHour;
    public Text timeText;
    public GameObject clockHand;
    private int dayNumber;
    public Text dayText;
    public string weekDay;
    public int weekDayInt;
    public List<string> WeekDayList = new List<string>();
    public Text weekDayText;
    public GameObject hunger;
    public GameObject main;
    public GameObject income;
    public GameObject schedule;

    private bool assignmentScheduled;
    private bool billScheduled;
    private int hoursAwake;

    public Text notify;

    MainController mainManager;
    Schedule scheduleManager;

    public GameObject btnAssignment;

    private int classesGoneToo;

    private bool sleep;

    Hunger hungerManager;
    Income incomeManager;
    Health healthManager;
    GPA gpaManager;
    SituationEvents seManager;

    private int dayLastEvent;
    private bool lastEvent;


    // Start is called before the first frame update
    void Start()
    {
        classesGoneToo = 0;

        dayLastEvent = 0;
        lastEvent = false;

        //Starting position of the clock
        currentHour = 9;
        clockHand.transform.Rotate(0, 0, -30 * currentHour);
        dayNumber = 1;
        assignmentScheduled = false;
        billScheduled = true;

        //Sets up a week array list
        /*WeekDayList.Add("Sunday");
        WeekDayList.Add("Monday");
        WeekDayList.Add("Tuesday");
        WeekDayList.Add("Wednesday");
        WeekDayList.Add("Thursday");
        WeekDayList.Add("Friday");
        WeekDayList.Add("Saturday");*/
        weekDayInt = 0;

        hungerManager = hunger.GetComponent<Hunger>();
        gpaManager = hunger.GetComponent<GPA>();
        healthManager = hunger.GetComponent<Health>();
        incomeManager = income.GetComponent<Income>();
        mainManager = main.GetComponent<MainController>();
        scheduleManager = schedule.GetComponent<Schedule>();
        seManager = schedule.GetComponent<SituationEvents>();
    }

    // Update is called once per frame
    void Update()
    {
        //Changes the clock and text files as needed
        dayText.text = ("Day " + dayNumber);
        weekDay = WeekDayList[weekDayInt];
        weekDayText.text = weekDay;

        //Clock settings
        if (currentHour == 0)
        {
            timeText.text = ("12:00 AM");
        }
        else if (currentHour > 0 && currentHour < 12)
        {
            timeText.text = (currentHour + ":00 AM");
        }
        else if (currentHour == 12)
        {
            timeText.text = ("12:00 PM");
        }
        else if (currentHour > 12)
        {
            timeText.text = (currentHour - 12 + ":00 PM");
        }
    }

    //Let's you loop to add hours
    public void PlusHours(int hours)
    {
        for (int j = 0; j < hours; j++)
        {
            PlusOneHour();

            if(!sleep)
            {
                hoursAwake++;
            }
        }

        //Punishes for missing sleep
        if (!sleep)
        {
            if (hoursAwake > 20)
            {
                notify.text = "Last Notification(Hour: " + currentHour + ":00): You need sleep, now!";
                if (!healthManager.subtractHealth(UnityEngine.Random.Range(20, 50)))
                {
                    mainManager.loseState(2);
                }
            }
        }

        mainManager.stressLevel = 0;
    }

    //Auto updates the clock
    private void PlusOneHour()
    {
        //Subtracts hunger per hour
        if (!hungerManager.subtractHunger(Random.Range(1, 3)))
        {
            mainManager.loseState(1);
        }


        if (!gpaManager.subtractGPA(Random.Range(0, .05f)))
        {
            mainManager.loseState(4);
        }

        //Intial changes
        currentHour += 1;
        clockHand.transform.Rotate(0, 0, -30);

        //Checks if assignment should dissapear
        if (weekDayInt == 6)
        {
            if (currentHour == 9)
            {
                btnAssignment.SetActive(false);
            }
        }

        //Goes to the next day if needed
        if (currentHour >= 24)
        {
            dayNumber += 1;
            weekDayInt += 1;
            currentHour = 0;


            //Calls events only after the first week
            if (dayNumber > 6)
            {
                if (!lastEvent)
                {
                    //Chance increases with more time
                    if (Random.Range(1 * dayNumber, 100) >= 50)
                    {
                        seManager.callEvent();
                        dayLastEvent = dayNumber;
                        lastEvent = true;
                    }
                }
                else
                {
                    if(dayNumber - dayLastEvent == 3)
                    {
                        lastEvent = false;
                        dayLastEvent = 0;
                    }
                }
            }

            //Goes to the next week if needed
            if (weekDayInt > 6)
            {
                weekDayInt = 0;

                btnAssignment.SetActive(true);

                //Lose points for skipping an assignment
                if (assignmentScheduled)
                {
                    notify.text = "Last Notification(Hour: " + currentHour + ":00): You skipped your assignment this week.  Minus .5 GPA!";
                    if (!gpaManager.subtractGPA(.5f))
                    {
                        mainManager.loseState(4);
                    }
                }

                assignmentScheduled = true;

                //Lose income for bill
                if (billScheduled)
                {
                    notify.text = "Last Notification(Hour: " + currentHour + ":00): You skipped your bill this week.  $650 charge!";
                    if (!incomeManager.removeMoney(400 + 250)) //Late payment
                    {
                        mainManager.loseState(3);
                    }
                }
                billScheduled = true;

                //Punishment for skipping classes in a week
                if(classesGoneToo != 4)
                {
                    notify.text = "Last Notification(Hour: " + currentHour + ":00): Your gpa suffered from the " + (4 - classesGoneToo) + " skipped class(es).";
                    if (!gpaManager.subtractGPA(.1f * (4 - classesGoneToo)))
                    {
                        mainManager.loseState(4);
                    }
                }
                classesGoneToo = 0;
            }
        }
    }

    //GETTERS AND SETTERS
    public int getCurrentDay()
    {
        return dayNumber;
    }

    public bool getBill()
    {
        return billScheduled;
    }

    public void setAssignmentStatus(bool status)
    {
        assignmentScheduled = status;
    }

    public void setBillStatus(bool status)
    {
        billScheduled = status;
    }

    public void setHoursAwake(int hours)
    {
        hoursAwake = hours;
    }

    public void setSleep(bool sleeping)
    {
        sleep = sleeping;
    }

    public void wentToClass()
    {
        classesGoneToo++;
    }
}
