using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class imageHold : MonoBehaviour
{
    public string location;
    public GameObject sphere;
    public GameObject portalContainer;
    public GameObject RoomLoader;

    public void switchRoomByImage()
    {
        for (int i = 0; i < RoomLoader.GetComponent<RoomLoader>().materialCollection.Length; i++)
        {
            if (RoomLoader.GetComponent<RoomLoader>().materialCollection[i].name == location)
            {
                sphere.GetComponent<MeshRenderer>().material = RoomLoader.GetComponent<RoomLoader>().materialCollection[i];
                break;
            }
        }

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
