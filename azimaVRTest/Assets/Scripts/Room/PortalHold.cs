using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalHold : MonoBehaviour
{
    public string destination;
    public Material portalColor;
    public Material portalOpaque;
    public string location;

    public void changeMaterialToOpaque()
    {
        gameObject.GetComponent<MeshRenderer>().sharedMaterial = portalOpaque;
    }

    public void changeMaterialToTransparent()
    {
        gameObject.GetComponent<MeshRenderer>().sharedMaterial = portalColor;
    }
}
