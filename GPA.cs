using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Health class to control the health bar and the 
public class GPA : MonoBehaviour
{
    //Declare variables
    public Text gpaText;

    public float startingGpa = 4.00f;
    float currentGpa;

    void Start()
    {
        currentGpa = startingGpa;
        setGpaText();
    }

    //Gets the current gpa
    public float getGPA()
    {
        return currentGpa;
    }

    //Sets the current gpa
    public void setGPA(float GPA)
    {
        currentGpa = GPA;
    }


    //Adds health
    public void addGPA(float gpa)
    {
        currentGpa += gpa;
        if (currentGpa > 4.00f)
        {
            currentGpa = 4.00f; //Max gpa
        }
        setGpaText();
    }

    //Returns true if still above 2.00, false if below
    public bool subtractGPA(float gpa)
    {
        currentGpa -= gpa;
        if (currentGpa <= 2.0f)
        {
            return false;
        }
        else
        {
            setGpaText();
            return true;
        }
    }


    //Sets the current health bar
    private void setGpaText()
    {
        gpaText.text = "GPA: " + System.Math.Round(currentGpa, 4);
    }
}
