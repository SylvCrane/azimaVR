using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//The store for the majority of the portal's data, and is also in charge of switching rooms.
public class PortalHold : MonoBehaviour
{
    public string destination; //The room where the portal is going
    public Material portalColor; //The portal's color, represented with a material
    public Material portalOpaque; //The portal's opaque color, represented with a material.
    public string location; //The portal's location.
    public GameObject sphere; //The sphere that shows the 360 images.
    public GameObject portalContainer; //The container for the portals
    public GameObject RoomLoader; //The RoomLoader containing the materials

    //Changes the portal material from its default transparent state to Opaque.
    public void ChangeMaterialToOpaque()
    {
        gameObject.GetComponent<MeshRenderer>().sharedMaterial = portalOpaque;
    }

    //Changes the portal material from its opqaue transparent state to transparent.
    public void ChangeMaterialToTransparent()
    {
        gameObject.GetComponent<MeshRenderer>().sharedMaterial = portalColor;   
    }
    

    //Called when the portal is interacted with (The user presses the trigger button when hovering over a portal)
    public void RoomClicked()
    {
        switchRooms(destination);
    }

    /*
     * Switches the 'room' by changing the image on the sphere, and appending the portals so the portals that belong
     * in the new room are set to active, and the portals that no longer belong to the room are set to inactive.
     * 
     * params)
     * - destination) Where the portal is going
     */
    void switchRooms(string destination)
    {
        //Loop through each of the images
        for (int i = 0; i < RoomLoader.GetComponent<RoomLoader>().materialCollection.Length; i++)
        {
            //If the image name matches the destination, set the sphere material to the image.
            if (RoomLoader.GetComponent<RoomLoader>().materialCollection[i].name == destination)
            {
                sphere.GetComponent<MeshRenderer>().material = RoomLoader.GetComponent<RoomLoader>().materialCollection[i];
                break;
            }
        }

        //Make every portal in destination image to active and every other portal to inactive.
        foreach (Transform portal in portalContainer.transform)
        {
            if (portal.name == destination)
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
