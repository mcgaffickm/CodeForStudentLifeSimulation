using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpMenu : MonoBehaviour
{
    public GameObject next, previous;

    public List <GameObject> panels;

    int current = 0;


    public void previousPage()
    {
        panels[current].SetActive(false);
        current--;
        next.SetActive(true);
        if(current == 0)
        {
            previous.SetActive(false);
        }
        panels[current].SetActive(true);
    }

    public void nextPage()
    {
        panels[current].SetActive(false);
        current++;
        previous.SetActive(true);
        if (current == panels.Count-1)
        {
            next.SetActive(false);
        }
        panels[current].SetActive(true);
    }
}
