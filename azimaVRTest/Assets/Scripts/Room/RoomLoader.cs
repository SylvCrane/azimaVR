using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Oculus.Interaction;
using UnityEditor;


//Class loads images for room as materials and assigns them to the materialCollection array
public class RoomLoader : MonoBehaviour
{
    public GameObject sphere; //The sphere the images are applied to
    public Material[] materialCollection; //The collection of materials that contain the images
    public bool startImageAssigned; //If the startImage is assigned or not
    public Slider slider; //The slider for the loading screen
    public GameObject loadingScreen; //The loading screen canvas itself
    public GameObject loadingImages; //The text object that states 'Loading images..'
    public GameObject loadingPortals; //The text object that states 'Loading portlas..'
    public GameObject cam; //The camera, at this point deprecated but used for debugging in Mouse+Keyboard scene
    public GameObject portalContainer; //The container the portals will go
    public GameObject portalReference; //The portal that all new portals will use as a reference
    public GameObject menuPlane; //The infoPlane, where houseData is appended to 
    public ImageLoader imageLoader; //Loads the images onto the imagePlane
    public GameObject imageHand; //The imagePlane in its default state
    public GameObject imageList; //The list of images on teh imagePlane

    /*
     * Normally, the Start() function would not need comments. In this case it is necessary as the start() function itself
     * performs the WebRequest to get the images in order to potentially decrease the time taken and load. 
     * 
     * The function, in this case, loads each image from the houseData into a collection of materials that contain the 
     * pulled image in the form of a material. 
     */ 
    IEnumerator Start()
    {
        //Set array of materials to number of images
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

                //Set this way to mitigate the line seen when the two sides of the image meet in 360.
                Texture2D imageTextureMipMap = new Texture2D(imageTexture.width, imageTexture.height, imageTexture.format, false);
                

                //Transfers image's pure pixel data to new imageTexture. 
                Color[] pixerls = imageTexture.GetPixels();
                imageTextureMipMap.SetPixels(pixerls);
                imageTextureMipMap.Apply();
                imageTextureMipMap.filterMode = FilterMode.Point;
  
                //Sets main texture of image at this point to the current material in materialCollection.
                materialCollection[i].SetTexture("_MainTex", imageTextureMipMap);

                //Slider for loading screen, increases with each imagePull
                float slideValueDefault = ((float)(i + 1) / HouseData.selectedHouse.images.Length);
                slider.value = Mathf.Clamp01(slideValueDefault);

                if (slider.value == 1.0)
                {
                    //When images are loade, change text and wait for half a second.
                    loadingImages.SetActive(false);
                    loadingPortals.SetActive(true);
                    yield return new WaitForSeconds(0.5f);
                }                
            }   
        }
        //Set sphere material to first image
        sphere.GetComponent<MeshRenderer>().material = materialCollection[0];

        //Portal Loader
        loadPortals();

        //Setting the images to the hand and making sure the right gameObjects are active
        setImageHandAndmenuPlane();
    }

    /*
     * loads the portals into the scene by getting the houseData portals and using the portalReference
     * to create new portals.
     */
    public void loadPortals()
    {
        for (int i = 0; i < HouseData.selectedHouse.portals.Length; i++)
        {

#pragma warning disable CS0436 // Type conflicts with imported type
            Portal newPortal = new Portal(); //Declares Portal script to assign data
#pragma warning restore CS0436 // Type conflicts with imported type
            string color = HouseData.selectedHouse.portals[i].triangles[0].color.Substring(1);

            //Split each vertex into x, y and z data. Only four are used, as only four are necessary. These are VertexA, B and C of the first triangle and vertexB of the third triangle.
            Vector3 bottomLeft = newPortal.splitVertex(HouseData.selectedHouse.portals[i].triangles[0].vertexA);
            Vector3 bottomRight = newPortal.splitVertex(HouseData.selectedHouse.portals[i].triangles[0].vertexB);
            Vector3 topLeft = newPortal.splitVertex(HouseData.selectedHouse.portals[i].triangles[0].vertexC);
            Vector3 topRight = newPortal.splitVertex(HouseData.selectedHouse.portals[i].triangles[2].vertexB);

            //Assigns the vertices to the portal
            newPortal.assignVertices(bottomLeft, bottomRight, topLeft, topRight);
            newPortal.destination = HouseData.selectedHouse.portals[i].destination;

            //Create portal GameObject
            GameObject portalRef = Instantiate(portalReference);
            portalRef.SetActive(false);
            newPortal.portal = portalRef;

            //Generates the portal in the scene.
            newPortal.GeneratePortal(HouseData.selectedHouse.portals[i].location, color);

            //In if statement as some portals are in the same room, therefore do not need text tags
            if (HouseData.selectedHouse.portals[i].textData.position != null)
            {
                Vector3 textLocation = newPortal.splitVertex(HouseData.selectedHouse.portals[i].textData.position);
                newPortal.GenerateText(textLocation, HouseData.selectedHouse.portals[i].textData.rotation.x, -HouseData.selectedHouse.portals[i].textData.rotation.y, HouseData.selectedHouse.portals[i].textData.rotation.z);
            }

            //Set as parent so they do not clutter scene heirarchy
            newPortal.setParentOfPortal(portalContainer);

            //Seting Portal hold content, crucial for raycasting
            newPortal.portal.GetComponent<PortalHold>().sphere = sphere;
            newPortal.portal.GetComponent<PortalHold>().RoomLoader = gameObject;
            newPortal.portal.GetComponent<PortalHold>().portalContainer = portalContainer;
        }

        //If portal name is the same as the first image name, set as active in the scene.
        foreach (Transform portal in portalContainer.transform)
        {
            if (portal.name == materialCollection[0].name)
            {
                portal.gameObject.SetActive(true);
            }
        }
    }

    /*
     * Sets the images and menu data of the house onto the hand Planes.
     */
    public void setImageHandAndmenuPlane()
    {
        //loads the images onto the hand using the materialCollection
        imageLoader.loadImagesOnHand(materialCollection);

        //Loading screen is set as active, the hands are set as active and their associated button images are set as true (in the unPressed position)
        loadingScreen.SetActive(false);
        menuPlane.SetActive(true);
        menuPlane.transform.GetChild(0).gameObject.SetActive(true);
        menuPlane.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(true);
        menuPlane.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(false);
        imageHand.SetActive(true);
        imageHand.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(true);
        imageHand.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(false);
        cam.GetComponent<PortalClick>().enabled = true;

        //Set first image in imageList to red, as in selected.
        imageList.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(255, 0, 0);
    }
}
