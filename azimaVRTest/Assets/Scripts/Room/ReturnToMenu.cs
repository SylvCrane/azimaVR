using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ReturnToMenu : MonoBehaviour
{
    public void SwitchToMainMenu()
    {
        //Swithc back to Main Menu
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
}
