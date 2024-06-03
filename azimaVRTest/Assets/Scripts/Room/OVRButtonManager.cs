using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Manages the button inputs for the Room scene
public class OVRButtonManager : MonoBehaviour
{
    public ScrollRect imageScroll; //The scrollRect for the images
    public GameObject imageButton; //the imagePlane in its default state
    public GameObject infoButton; //the infoPlane in its default state
    public GameObject infoPlane; //the imagePlane in its active state
    public GameObject imagePlane; //the infoPlane in its active state
    public GameObject imageList; //The list of images in the imageHand
    public Button currentImage; //The currentImage, as a button.
    public bool xButtonPressedBool; //If the x button is pressed
    public bool yButtonPressedBool; //if the y button is pressed
    public bool bButtonPressedBool; //If the b button is pressed
    public bool aButtonPressedBool; //If the a button is pressed
    public float timer = 0.0f; //The timer for the scroll
    public int publicImageCount; //The number of images

    void Start()
    {
        xButtonPressedBool = false;
        yButtonPressedBool = false;
        bButtonPressedBool = false;
        aButtonPressedBool = false;
        currentImage = imageList.transform.GetChild(0).gameObject.GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
       //Time is upadtes for real time
        timer += Time.deltaTime;

        //Interaction for info Button, if pressed then turn PressedButton image on
        if ((OVRInput.GetDown(OVRInput.RawButton.A)) && (infoButton.activeInHierarchy))
        {
            infoButton.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(false);
            infoButton.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(true);
            aButtonPressedBool = true;
        }

        //If pressed, switch infoPlane to active, set infoButton to inactive
        if ((OVRInput.GetUp(OVRInput.RawButton.A)) && (aButtonPressedBool) && (infoButton.activeInHierarchy))
        {
            infoButton.SetActive(false);
            infoPlane.SetActive(true);
            infoPlane.transform.GetChild(0).transform.GetChild(1).transform.GetChild(20).gameObject.SetActive(true);
            infoPlane.transform.GetChild(0).transform.GetChild(1).transform.GetChild(21).gameObject.SetActive(false);
            infoPlane.transform.GetChild(0).transform.GetChild(1).transform.GetChild(22).gameObject.SetActive(true);
            infoPlane.transform.GetChild(0).transform.GetChild(1).transform.GetChild(23).gameObject.SetActive(false);
            infoPlane.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(false);
            aButtonPressedBool = false;
        }

        //Interaction for exiting info plane
        if ((OVRInput.GetDown(OVRInput.RawButton.B)) && (infoPlane.activeInHierarchy))
        {
            infoPlane.transform.GetChild(0).transform.GetChild(1).transform.GetChild(20).gameObject.SetActive(false);
            infoPlane.transform.GetChild(0).transform.GetChild(1).transform.GetChild(21).gameObject.SetActive(true);
            bButtonPressedBool = true;
        }

        //If pressed, switch infoButton to active and switch infoPlane to inactive.
        if ((OVRInput.GetUp(OVRInput.RawButton.B)) && (bButtonPressedBool) && (infoPlane.activeInHierarchy))
        {
            infoButton.SetActive(true);
            infoPlane.SetActive(false);
            infoButton.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(true);
            infoButton.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(false);
            infoButton.transform.GetChild(0).transform.GetChild(4).gameObject.SetActive(false);
            infoButton.transform.GetChild(0).transform.GetChild(5).gameObject.SetActive(false);
            bButtonPressedBool = false;
        }

        //Interaction for returning to main menu
        if ((OVRInput.GetDown(OVRInput.RawButton.A)) && (infoPlane.activeInHierarchy))
        {
            infoPlane.transform.GetChild(0).transform.GetChild(1).transform.GetChild(22).gameObject.SetActive(false);
            infoPlane.transform.GetChild(0).transform.GetChild(1).transform.GetChild(23).gameObject.SetActive(true);
            infoPlane.transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().color = new Color(255, 79, 79);
            aButtonPressedBool = true;
        }

        //exit room
        if ((OVRInput.GetUp(OVRInput.RawButton.A)) && (aButtonPressedBool) && (infoPlane.activeInHierarchy))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
        }

        //Interaction for entering image plane
        if ((OVRInput.GetDown(OVRInput.RawButton.X)) && (imageButton.activeInHierarchy))
        {
            imageButton.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(false);
            imageButton.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(true);
            xButtonPressedBool = true;
        }

        //If released, switch imagePlane to active and imageButton to inactive.
        if ((OVRInput.GetUp(OVRInput.RawButton.X)) && (xButtonPressedBool) && (imageButton.activeInHierarchy))
        {
            imageButton.SetActive(false);
            imagePlane.SetActive(true);
            imagePlane.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(true);
            imagePlane.transform.GetChild(0).transform.GetChild(4).gameObject.SetActive(false);
            imagePlane.transform.GetChild(0).transform.GetChild(5).gameObject.SetActive(false);

            xButtonPressedBool = false;
        }

        //Interaction for exiting image plane
        if ((OVRInput.GetDown(OVRInput.RawButton.Y)) && (imagePlane.activeInHierarchy))
        {
            imagePlane.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(false);
            imagePlane.transform.GetChild(0).transform.GetChild(4).gameObject.SetActive(true);
            yButtonPressedBool = true;
        }

        //If released, set imageButton to active and imagePlane to inactive.
        if ((OVRInput.GetUp(OVRInput.RawButton.Y)) && (yButtonPressedBool) && (imagePlane.activeInHierarchy))
        {
            imageButton.SetActive(true);
            imagePlane.SetActive(false);
            imageButton.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(true);
            imageButton.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(false);
            imageButton.transform.GetChild(0).transform.GetChild(4).gameObject.SetActive(false);
            yButtonPressedBool = false;
        }

        //Current Button
        if ((OVRInput.GetDown(OVRInput.RawButton.X)) && (imagePlane.activeInHierarchy))
        {
            currentImage.gameObject.transform.GetChild(2).gameObject.SetActive(false);
            currentImage.gameObject.transform.GetChild(3).gameObject.SetActive(true);
            xButtonPressedBool = true;
        }

        //If released, invoke onClick() function on current image, which is SwitchRoomByImage().
        if ((OVRInput.GetUp(OVRInput.RawButton.X)) && (xButtonPressedBool) && (imagePlane.activeInHierarchy))
        {
            currentImage.gameObject.transform.GetChild(2).gameObject.SetActive(true);
            currentImage.gameObject.transform.GetChild(3).gameObject.SetActive(false);
            xButtonPressedBool = false;
            currentImage.onClick.Invoke();
        }

        //Scroll on images
        if ((imagePlane.activeInHierarchy))
        {
            Vector2 scrollVal = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

            float range = scrollVal.y;
            Debug.Log(range);
            if (range != 0)
            {
                imageScroll.verticalNormalizedPosition += range * 0.02f;
            }
        }

        imageInteractor();
    }

    /*
     * Switches the currently selected image when scrolling.
     */
    public void imageInteractor()
    {
        
        Vector2 scrollVal = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        float range = scrollVal.y;

        //TimerThreshold is the amount of time that has to pass before the selected image switches
        float TimerThreshold = 0.0f;
        

        //Threshold dynamically changes based on how fast the user is scrolling
        if (range < 0)
        {
            TimerThreshold = 0.2f / (range * -1);
        }
        else
        {
            TimerThreshold = 0.2f / range;
        }

        //If ThresHold is met and user is scrolling up or down, set the currentImage to the next image in the imageList.
        if (range < 0 && timer > TimerThreshold)
        {
            //If the publicImageCount is not the amount of images in the imageList
            if (publicImageCount != (imageList.transform.childCount - 1))
            {
                Button nextImage = imageList.transform.GetChild(publicImageCount + 1).gameObject.GetComponent<Button>();
                nextImage.transform.GetChild(2).gameObject.SetActive(true);
                nextImage.gameObject.GetComponent<Image>().color = new Color(255, 0, 0);
                currentImage.transform.GetChild(2).gameObject.SetActive(false);
                currentImage.gameObject.GetComponent<Image>().color = new Color(255, 255, 255);
                currentImage = nextImage;
                publicImageCount += 1;
                timer = 0.0f;
            }
        }
        else if (range > 0 && timer > TimerThreshold)
        {
            //If the publicImageCount is not 0
            if (publicImageCount != 0)
            {
                Button nextImage = imageList.transform.GetChild(publicImageCount - 1).gameObject.GetComponent<Button>();
                nextImage.transform.GetChild(2).gameObject.SetActive(true);
                nextImage.gameObject.GetComponent<Image>().color = new Color(255, 0, 0);
                currentImage.transform.GetChild(2).gameObject.SetActive(false);
                currentImage.gameObject.GetComponent<Image>().color = new Color(255, 255, 255);
                currentImage = nextImage;
                publicImageCount -= 1;
                timer = 0.0f;
            }
        }
    }
}
