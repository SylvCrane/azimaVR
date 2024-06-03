using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Used to handle the tutorials for the Room scene, similar to in the MainMenu scene
public class RoomTutorialManager : MonoBehaviour
{
    public GameObject imageButton; //The imagePlane in its default state
    public GameObject infoButton; //The infoPlane in its default state
    public GameObject imagePlane; //The imagePlane in its active state
    public GameObject infoPlane; //The infoPlane in its active state

    public GameObject portalTutorial; //The tutorials associated with portals, connected to the infoButton
    public GameObject imageButtonTutorial; //The tutorials associated with the imageButton
    public GameObject imagePlaneTutorial; //The tutorials associated with the imagePlane
    public GameObject infoButtonTutorial; //The tutorials associated with the infoButton
    public GameObject infoPlaneTutorial; //The tutorials associated with the infoPlane

    void Start()
    {
        //Set the tutorials currently not active in the scene to false.
        infoButtonTutorial.SetActive(false);
        portalTutorial.SetActive(false);
        imageButtonTutorial.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        //Same as in TutorialManager, The tutorials for inactive GameObjects are set to false.
         
        if (!imageButton.activeInHierarchy)
        {
            imageButtonTutorial.SetActive(false);
        }

        if (!infoButton.activeInHierarchy)
        {
            infoButtonTutorial.SetActive(false);
            portalTutorial.SetActive(false);
        }

        if (!imagePlane.activeInHierarchy)
        {
            imagePlaneTutorial.SetActive(false);
        }

        if (!infoPlane.activeInHierarchy)
        {
            infoPlaneTutorial.SetActive(false);
        }

        //When the grip button is pressed, set the totorials of the active GameObject(Planes, in this case) to either active or inactive.
        if ((OVRInput.GetUp(OVRInput.RawButton.LHandTrigger) || OVRInput.GetUp(OVRInput.RawButton.RHandTrigger)))
        {
            if (imageButton.activeInHierarchy)
            {
                if (imageButtonTutorial.activeInHierarchy)
                {
                    imageButtonTutorial.SetActive(false);
                }
                else
                {
                    imageButtonTutorial.SetActive(true);
                }
            }

            if (infoButton.activeInHierarchy)
            {
                if (infoButtonTutorial.activeInHierarchy)
                {
                    infoButtonTutorial.SetActive(false);
                    portalTutorial.SetActive(false);
                }
                else
                {
                    infoButtonTutorial.SetActive(true);
                    portalTutorial.SetActive(true);
                }
            }

            if (imagePlane.activeInHierarchy)
            {
                if (imagePlaneTutorial.activeInHierarchy)
                {
                    imagePlaneTutorial.SetActive(false);
                }
                else
                {
                    imagePlaneTutorial.SetActive(true);
                }
            }

            if (infoPlane.activeInHierarchy)
            {
                if (infoPlaneTutorial.activeInHierarchy)
                {
                    infoPlaneTutorial.SetActive(false);
                }
                else
                {
                    infoPlaneTutorial.SetActive(true);
                }
            }
        }
    }
}
