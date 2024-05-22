using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollManager : MonoBehaviour
{
    public GameObject login;
    public GameObject guest;
    public ScrollRect scrollObjectMainHouses;
    public ScrollRect scrollObjectLoginHouses;

    // Update is called once per frame
    void Update()
    {
        if (login.activeInHierarchy)
        {
            Vector2 scrollVal = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

            float range = scrollVal.y;

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
