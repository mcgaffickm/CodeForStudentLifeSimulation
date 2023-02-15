using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndController : MonoBehaviour
{
    public Text score;
    public Text ending;

    void Start()
    {
        score.text = "Your Score: " + LoseCondition.score;
        ending.text = "Ending: ";
        switch(LoseCondition.loseState)
        {
            case 1:
                ending.text += "You skipped one too many meals and have been hospitalized due to your condition.";
                break;
            case 2:
                ending.text += "You let your mental health worsen and were hospitalized after having a nervous breakdown.";
                break;
            case 3:
                ending.text += "You acquired too much debt that you will not be able to pay off.  You could not afford your housing anymore.";
                break;
            case 4:
                ending.text += "You neglected your schoolwork and have been put on academic probation.";
                break;
            case 5:
                ending.text += "You were overwhelmed with the stress of your life and had a nervous break down.";
                break;
        }
    }
}
