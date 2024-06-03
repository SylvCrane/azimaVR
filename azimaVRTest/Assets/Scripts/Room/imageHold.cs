using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Holds the image data, and switches the room by an image
public class imageHold : MonoBehaviour
{
    public string location; //Where the image is located
    public GameObject sphere; //The sphere on which the 360 images are shown
    public GameObject portalContainer; //The container for the portals of the scene
    public GameObject RoomLoader; //The GameObject representation of the RoomLoader

    /*
     * Switches the room by the image as opposed to using portals. 
     */
    public void switchRoomByImage()
    {
        for (int i = 0; i < RoomLoader.GetComponent<RoomLoader>().materialCollection.Length; i++)
        {
            //Switches room by the location as opposed to destination
            if (RoomLoader.GetComponent<RoomLoader>().materialCollection[i].name == location)
            {
                sphere.GetComponent<MeshRenderer>().material = RoomLoader.GetComponent<RoomLoader>().materialCollection[i];
                break;
            }
        }

        //Set all portals in location to active, and portals not in location to false.
        foreach (Transform portal in portalContainer.transform)
        {
            if (portal.name == location)
            {
                portal.gameObject.SetActive(true);
            }
            else
            {
                portal.gameObject.SetActive(false);
            }
        }
    }
}
