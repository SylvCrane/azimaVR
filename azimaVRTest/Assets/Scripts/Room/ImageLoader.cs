using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Used to load the images onto the imageHand.
public class ImageLoader : MonoBehaviour
{
    public GameObject imageList; //The list of images on the imageHand.

    /*
     * Loads the images onto the imageHand using the materialCollection from RoomLoader.
     * 
     * Params)
     * - materialCollection) The array of materials that contain the images of the house.
     */
    public void loadImagesOnHand(Material[] materialCollection)
    {
        //The process is very similar to pullHouses, how houses are applied to the scene
        GameObject blankImage = imageList.transform.GetChild(0).gameObject;
        GameObject nonOverWrittenImage = blankImage;

        for (int i = 0; i < materialCollection.Length; i++)
        {
            if (i == 0)
            {
                assignImage(blankImage, materialCollection[i]);
                assignLocation(blankImage, materialCollection[i]);
            }
            else
            {
                //Instantiate based on the nonOverWrittenImage, make the imageList the parent and assign the image and location.
                GameObject currentImage = Instantiate(nonOverWrittenImage, imageList.transform);
                currentImage.transform.SetParent(imageList.transform);
                currentImage.transform.SetAsLastSibling();
                assignImage(currentImage, materialCollection[i]);
                assignLocation(currentImage, materialCollection[i]);
            }
        }
    }

    /*
     * Assigns the image itself to the imageList. Gets the image component from
     * the Image GameObject and applies the image of the room to it.
     * 
     * Params)
     * - subjectImage) The GameObject that the image will be appended to
     * - currentMaterial) The material that contains the image
     */
    public void assignImage(GameObject subjectImage, Material currentMaterial)
    {
        //Apply image as sprite to image component
        Image image = subjectImage.transform.GetChild(0).GetComponent<Image>();
        Sprite imageSprite = Sprite.Create((Texture2D)currentMaterial.mainTexture, new Rect(0, 0, currentMaterial.mainTexture.width, currentMaterial.mainTexture.height), new Vector2());
        image.sprite = imageSprite;

    }

    /*
     * Assigns the location of the image to the TMP object
     * 
     * Params)
     * - image) The current image
     * - material) The material containing the image
     */
    public void assignLocation(GameObject image, Material material)
    {
        //Get the TMP component contained in the image GameObject's child
        TextMeshProUGUI location = image.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        location.text = material.name;

        //Sets location of imageHold, important for switching rooms
        imageHold holdImage = image.transform.GetComponent<imageHold>();
        holdImage.location = material.name;

    }
}
