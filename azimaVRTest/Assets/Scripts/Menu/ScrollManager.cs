using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollManager : MonoBehaviour
{
    public GameObject login; //The loginHouseArea
    public GameObject guest; //The HouseArea, but for guests
    public ScrollRect scrollObjectMainHouses; //The ScrollRect for the guest HouseList
    public ScrollRect scrollObjectLoginHouses; //The ScrollRect for the loginHouseList

    // Update is called once per frame
    void Update()
    {
        //If login is active, scrolling using the thumbstick is active.
        if (login.activeInHierarchy)
        {
            //Get 2D scroll value from thumbstick
            Vector2 scrollVal = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

            //Get only Y
            float range = scrollVal.y;

            //Append value to the scrollRect
            if (range != 0)
            {
                scrollObjectLoginHouses.verticalNormalizedPosition += range * 0.05f;
            }
        }
        
        if (guest.activeInHierarchy)
        {
            Vector2 scrollVal = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

            float range = scrollVal.y;

            if (range != 0)
            {
                scrollObjectMainHouses.verticalNormalizedPosition += range * 0.05f;
            }
        }

    }
}
