using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PortalHold : MonoBehaviour
{
    public string destination;
    public Material portalColor;
    public Material portalOpaque;
    public string location;
    public GameObject sphere;
    public GameObject portalContainer;
    public GameObject RoomLoader;

    public void ChangeMaterialToOpaque()
    {
        gameObject.GetComponent<MeshRenderer>().sharedMaterial = portalOpaque;
    }

    public void ChangeMaterialToTransparent()
    {
        gameObject.GetComponent<MeshRenderer>().sharedMaterial = portalColor;   
    }
    
    public void RoomClicked()
    {
        switchRooms(destination);
    }

    void switchRooms(string destination)
    {
        for (int i = 0; i < RoomLoader.GetComponent<RoomLoader>().materialCollection.Length; i++)
        {
            if (RoomLoader.GetComponent<RoomLoader>().materialCollection[i].name == destination)
            {
                sphere.GetComponent<MeshRenderer>().material = RoomLoader.GetComponent<RoomLoader>().materialCollection[i];
                break;
            }
        }

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
