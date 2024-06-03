using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Used to return to the main menu
public class ReturnToMenu : MonoBehaviour
{
    //Switches the scene to the main menu
    public void SwitchToMainMenu()
    {
        //Swithc back to Main Menu
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
}
