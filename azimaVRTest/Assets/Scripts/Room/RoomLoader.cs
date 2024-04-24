using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

//Class loads images for room as materials and assigns them to the materialCollection array
public class RoomLoader : MonoBehaviour
{
    public GameObject sphere;
    public Material[] materialCollection;
    public GameObject miniSphere;
    public bool startImageAssigned;
    public Slider slider;
    public GameObject loadingScreen;
    public GameObject loadingImages;
    public GameObject loadingPortals;
    public GameObject cam;
    public GameObject portalContainer;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        materialCollection = new Material[HouseData.selectedHouse.images.Length];

        //For each image, a separate pull must occur. 
        for (int i = 0; i < HouseData.selectedHouse.images.Length; i++)
        {
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(HouseData.selectedHouse.images[i].imageURL);
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(request.error);
            }
            else
            {
                //Declaring the current indexed material and assigning it to a Skybox for use in the sphere. Then, get the image texture and assign it to the material.
                materialCollection[i] = new Material(Shader.Find("Skybox/Panoramic"));
                materialCollection[i].name = HouseData.selectedHouse.images[i].name;
                Texture2D imageTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;
                Texture2D imageTextureMipMap = new Texture2D(imageTexture.width, imageTexture.height, imageTexture.format, false);

                Color[] pixerls = imageTexture.GetPixels();
                imageTextureMipMap.SetPixels(pixerls);
                imageTextureMipMap.Apply();
                imageTextureMipMap.filterMode = FilterMode.Point;
                materialCollection[i].SetTexture("_MainTex", imageTextureMipMap);

                //Slider for loading screen
                float slideValueDefault = ((float)(i + 1) / HouseData.selectedHouse.images.Length);
                float slideValueHalf = (float)(slideValueDefault / 2);
                Debug.Log(slideValueHalf);
                slider.value = Mathf.Clamp01(slideValueHalf);
                if (slider.value == 0.5)
                {
                    loadingImages.SetActive(false);
                    loadingPortals.SetActive(true);
                    yield return new WaitForSeconds(0.5f);
                }                
            }   
        }

        //Portal Loader
        for (int i = 0; i < HouseData.selectedHouse.portals.Length; i++)
        {
            Portal newPortal = new Portal();
            string color = HouseData.selectedHouse.portals[i].triangles[0].color.Substring(1);

            Vector3 bottomLeft = newPortal.splitVertex(HouseData.selectedHouse.portals[i].triangles[0].vertexA);
            Vector3 bottomRight = newPortal.splitVertex(HouseData.selectedHouse.portals[i].triangles[0].vertexB);
            Vector3 topLeft = newPortal.splitVertex(HouseData.selectedHouse.portals[i].triangles[0].vertexC);
            Vector3 topRight = newPortal.splitVertex(HouseData.selectedHouse.portals[i].triangles[2].vertexB);

            newPortal.assignVertices(bottomLeft, bottomRight, topLeft, topRight);
            newPortal.destination = HouseData.selectedHouse.portals[i].destination;

            newPortal.GeneratePortal(HouseData.selectedHouse.portals[i].location, color);

            Vector3 textLocation = newPortal.splitVertex(HouseData.selectedHouse.portals[i].textData.position);
            newPortal.GenerateText(textLocation, HouseData.selectedHouse.portals[i].textData.rotation.x, -HouseData.selectedHouse.portals[i].textData.rotation.y, HouseData.selectedHouse.portals[i].textData.rotation.z);

            newPortal.setParentOfPortal(portalContainer);

            float slideValueDefault = ((float)(i + 1) / HouseData.selectedHouse.portals.Length);
            float slideValueHalf = (float)(slideValueDefault / 2);
            float slideValueHalfTotal = (float)(slideValueHalf + 0.5);
            
            slider.value = Mathf.Clamp01(slideValueHalfTotal);
            if (slider.value == 1.0)
            {
                yield return new WaitForSeconds(0.5f);
            }

        }

        sphere.GetComponent<MeshRenderer>().material = materialCollection[0];
        loadingScreen.SetActive(false);
        cam.GetComponent<PortalClick>().enabled = true;
    }  
}
