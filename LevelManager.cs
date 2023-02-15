using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Level manager to control the scenes
public class LevelManager : MonoBehaviour {



    //This will allow us to have a splash screen in final product

    //public float autoLoadNextLevelAfter;
    //void Start ()
    //{
    //	if (autoLoadNextLevelAfter <= 0) {
    //Debug.Log("Auto Load Disabled; Use positive number in seconds.");
    //} else {
    //Invoke ("LoadNextLevel", autoLoadNextLevelAfter);
    //}
    //}

    //Loads the specified level
    public void LoadLevel (string name)
	{
		Debug.Log("Level Load Requested for " + name);
		SceneManager.LoadScene(name);
	}

    //Quits for the main menu
	public void QuitRequest ()
	{
		Debug.Log("Quit Requested");
		Application.Quit();
	}

    //Autoload, if we use splash screen
	public void LoadNextLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
	}
}
