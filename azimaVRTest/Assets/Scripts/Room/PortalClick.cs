using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalClick : MonoBehaviour
{
    public GameObject RoomLoader;
    public GameObject sphere;
    public bool inPortal;
    public GameObject portalContainer;

    public GameObject portalStore;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, Mathf.Infinity))
        {
            Debug.Log(hitInfo.collider.gameObject.name);
            
            if (hitInfo.collider.gameObject.GetComponent<PortalHold>())
            {
                hitInfo.collider.gameObject.GetComponent<PortalHold>().changeMaterialToOpaque();
                portalStore = hitInfo.collider.gameObject;
                inPortal = true;
            }
        }
        else
        {
            if (portalStore != null)
            {
                portalStore.GetComponent<PortalHold>().changeMaterialToTransparent();
                portalStore = null;
                inPortal = false;
            }
        }

        if ((Input.GetButtonDown("Fire1")) && (inPortal))
        {
            switchRooms(portalStore.GetComponent<PortalHold>().destination);
        }

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
