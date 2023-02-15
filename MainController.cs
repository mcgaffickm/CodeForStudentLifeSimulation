using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
    public GameObject time;
    public GameObject healthBars;
    public GameObject income;
    public GameObject level;
    public Text scoreText;
    public GameObject bill;
    public Slider stress;
 
    Income incomeManager;
    GPA gpaManager;
    Time timeManager;
    Hunger hungerManager;
    Health healthManager;
    LevelManager levelManager;

    int score;
    public float stressLevel;
    

    // Start is called before the first frame update
    void Start()
    {
        incomeManager = income.GetComponent<Income>();
        gpaManager = healthBars.GetComponent<GPA>();
        hungerManager = healthBars.GetComponent<Hunger>();
        healthManager = healthBars.GetComponent<Health>();
        timeManager = time.GetComponent<Time>();
        levelManager = level.GetComponent<LevelManager>();
        score = 0;
        stressLevel = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Way for the user to go forward an hour manually
        //Brian: I originally put this here just for testing purposes. I don't think it would benefit the player, so it has no reason to be available to them.
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            timeManager.PlusHours(1);
        }*/

        ///Turns bill on and off as needed
        bill.SetActive(timeManager.getBill());

        calculateScore();
        scoreText.text = "Total Score: " + score;

        manageStress();
    }

    //Calculates the score in a way that encourages skipping meals and not focusing on health
    private int calculateScore()
    {
        score = 0;
        score += timeManager.getCurrentDay() * 1000; //time points
        score += hungerManager.getHunger() / 5; //Less points for hunger
        score += healthManager.getHealth(); //Not much for health
        score += incomeManager.getIncome() * 5; //More for income
        score += (int)(gpaManager.getGPA() * 50); //Gpa is a small number, so needs a big multiplier
        return score;
    }

    //Sets the lose state and ends the game
    //1 = hunger runs out
    //2 = health runs out
    //3 = debt over 100
    //4 = gpa below 2.0
    //5 = too much stress
    public void loseState(int stateCount)
    {
        calculateScore();
        LoseCondition.score = score;
        LoseCondition.loseState = stateCount;
        levelManager.LoadLevel("2_EndScreen");
    }

    private void manageStress()
    {
        stressLevel += .00001f * (100 - healthManager.getHealth());
        stress.value = stressLevel;
        if(stressLevel > 1)
        {
            loseState(5);
        }
    }
}
