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
                materialCollection[i].name = HouseData.selectedHouse.houseID;
                Texture2D imageTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;
                Texture2D imageTextureMipMap = new Texture2D(imageTexture.width, imageTexture.height, imageTexture.format, false);

                Color[] pixerls = imageTexture.GetPixels();
                imageTextureMipMap.SetPixels(pixerls);
                imageTextureMipMap.Apply();
                
                
                //Graphics.CopyTexture(imageTexture, imageTextureMipMap);
                
                
                
                imageTexture.filterMode = FilterMode.Point;
                

                //miniSphere.GetComponent<MeshRenderer>().material.mainTexture = imageTexture;
              //  materialCollection[i].SetTexture("_MainTex", imageTexture);
                materialCollection[i].SetTexture("_MainTex", imageTextureMipMap);

                
                slider.value = Mathf.Clamp01((float)(i + 1) / HouseData.selectedHouse.images.Length);
                Debug.Log(slider.value);
                Debug.Log("Breakpoint");
                
            }

                
        }
        sphere.GetComponent<MeshRenderer>().material = materialCollection[HouseData.selectedHouse.images.Length - 1];
    }  
}
