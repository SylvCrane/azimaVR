using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OVRButtonManager : MonoBehaviour
{
    public ScrollRect imageScroll;
    public GameObject imageButton;
    public GameObject infoButton;
    public GameObject infoPlane;
    public GameObject imagePlane;
    public GameObject imageList;
    public Button currentImage;
    public bool xButtonPressedBool;
    public bool yButtonPressedBool;
    public bool bButtonPressedBool;
    public bool aButtonPressedBool;
    public float timer = 0.0f;
    public int publicImageCount;

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
        timer += Time.deltaTime;
        //Interaction for info Button
        if ((OVRInput.GetDown(OVRInput.RawButton.A)) && (infoButton.activeInHierarchy))
        {
            infoButton.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(false);
            infoButton.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(true);
            aButtonPressedBool = true;
        }

        if ((OVRInput.GetUp(OVRInput.RawButton.A)) && (aButtonPressedBool) && (infoButton.activeInHierarchy))
        {
            infoButton.SetActive(false);
            infoPlane.SetActive(true);
            infoPlane.transform.GetChild(0).transform.GetChild(1).transform.GetChild(20).gameObject.SetActive(true);
            infoPlane.transform.GetChild(0).transform.GetChild(1).transform.GetChild(21).gameObject.SetActive(false);
            infoPlane.transform.GetChild(0).transform.GetChild(1).transform.GetChild(22).gameObject.SetActive(true);
            infoPlane.transform.GetChild(0).transform.GetChild(1).transform.GetChild(23).gameObject.SetActive(false);
            aButtonPressedBool = false;
        }

        //Interaction for exiting info plane
        if ((OVRInput.GetDown(OVRInput.RawButton.B)) && (infoPlane.activeInHierarchy))
        {
            infoPlane.transform.GetChild(0).transform.GetChild(1).transform.GetChild(20).gameObject.SetActive(false);
            infoPlane.transform.GetChild(0).transform.GetChild(1).transform.GetChild(21).gameObject.SetActive(true);
            bButtonPressedBool = true;
        }

        if ((OVRInput.GetUp(OVRInput.RawButton.B)) && (bButtonPressedBool) && (infoPlane.activeInHierarchy))
        {
            infoButton.SetActive(true);
            infoPlane.SetActive(false);
            infoButton.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(true);
            infoButton.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(false);
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

        if ((OVRInput.GetUp(OVRInput.RawButton.X)) && (xButtonPressedBool) && (imageButton.activeInHierarchy))
        {
            imageButton.SetActive(false);
            imagePlane.SetActive(true);
            imagePlane.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(true);
            imagePlane.transform.GetChild(0).transform.GetChild(4).gameObject.SetActive(false);
            xButtonPressedBool = false;
        }

        //Interaction for exiting image plane
        if ((OVRInput.GetDown(OVRInput.RawButton.Y)) && (imagePlane.activeInHierarchy))
        {
            imagePlane.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(false);
            imagePlane.transform.GetChild(0).transform.GetChild(4).gameObject.SetActive(true);
            yButtonPressedBool = true;
        }

        if ((OVRInput.GetUp(OVRInput.RawButton.Y)) && (yButtonPressedBool) && (imagePlane.activeInHierarchy))
        {
            imageButton.SetActive(true);
            imagePlane.SetActive(false);
            imageButton.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(true);
            imageButton.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(false);
            yButtonPressedBool = false;
        }

        //Current Button
        if ((OVRInput.GetDown(OVRInput.RawButton.X)) && (imagePlane.activeInHierarchy))
        {
            currentImage.gameObject.transform.GetChild(2).gameObject.SetActive(false);
            currentImage.gameObject.transform.GetChild(3).gameObject.SetActive(true);
            xButtonPressedBool = true;
        }

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

    public void imageInteractor()
    {
        
        Vector2 scrollVal = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        float range = scrollVal.y;

        float TimerThreshold = 0.0f;

        if (range < 0)
        {
            TimerThreshold = 0.2f / (range * -1);
        }
        else
        {
            TimerThreshold = 0.2f / range;
        }

        if (range < 0 && timer > TimerThreshold)
        {
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
