using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject StartMenu; //The start menu for the canvas
    public GameObject Login; //The login menu for the canvas
    public GameObject HouseArea; //The HouseArea menu for the canvas 
    public GameObject LoginHouseArea; //The LoginHouseArea menu for the canvas
    public GameObject StartMenuTutorial; //Tutorials associated with the StartMenu
    public GameObject LoginTutorial; //Tutorials associated with the Login 
    public GameObject HouseAreaTutorial; //Tutorials associated with the HouseArea
    public GameObject LoginHouseAreaTutorial; //Tutorials associated with the LoginHouseArea


    void Start()
    {
        //At the start, set the tutorials for the Start Menu to true, as this is the starting canvas
        StartMenuTutorial.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        /*
         * For the following four if statements, these state that if the current active canvas is not
         * any of the global variable canvases, set all other canvas tutorials to false.
         */
        if (!StartMenu.activeInHierarchy)
        {
            StartMenuTutorial.SetActive(false);
        }

        if (!Login.activeInHierarchy)
        {
            LoginTutorial.SetActive(false);
        }

        if (!HouseArea.activeInHierarchy)
        {
            HouseAreaTutorial.SetActive(false);
        }

        if (!LoginHouseArea.activeInHierarchy)
        {
            LoginHouseAreaTutorial.SetActive(false);
        }

        //Here, if the grip trigger is pressed, the tutorial for the current active scene is set to either true or false.
        if ((OVRInput.GetUp(OVRInput.RawButton.LHandTrigger) || OVRInput.GetUp(OVRInput.RawButton.RHandTrigger)))
        {
            if (StartMenu.activeInHierarchy)
            {
                if (StartMenuTutorial.activeInHierarchy)
                {
                    StartMenuTutorial.SetActive(false);
                }
                else
                {
                    StartMenuTutorial.SetActive(true);
                }
            }

            if (Login.activeInHierarchy)
            {
                if (LoginTutorial.activeInHierarchy)
                {
                    LoginTutorial.SetActive(false);
                }
                else
                {
                    LoginTutorial.SetActive(true);
                }
            }

            if (HouseArea.activeInHierarchy)
            {
                if (HouseAreaTutorial.activeInHierarchy)
                {
                    HouseAreaTutorial.SetActive(false);
                }
                else
                {
                    HouseAreaTutorial.SetActive(true);
                }
            }

            if (LoginHouseArea.activeInHierarchy)
            {
                if (LoginHouseAreaTutorial.activeInHierarchy)
                {
                    LoginHouseAreaTutorial.SetActive(false);
                }
                else
                {
                    LoginHouseAreaTutorial.SetActive(true);
                }
            }


        }
    }
}
