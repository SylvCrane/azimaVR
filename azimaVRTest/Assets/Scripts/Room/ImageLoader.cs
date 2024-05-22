using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ImageLoader : MonoBehaviour
{
    public GameObject imageList;

    public void loadImagesOnHand(Material[] materialCollection)
    {
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
                GameObject currentImage = Instantiate(nonOverWrittenImage, imageList.transform);
                currentImage.transform.SetParent(imageList.transform);
                currentImage.transform.SetAsLastSibling();
                assignImage(currentImage, materialCollection[i]);
                assignLocation(currentImage, materialCollection[i]);
            }
        }
    }

    public void assignImage(GameObject subjectImage, Material currentMaterial)
    {
        Image image = subjectImage.transform.GetChild(0).GetComponent<Image>();
        Sprite imageSprite = Sprite.Create((Texture2D)currentMaterial.mainTexture, new Rect(0, 0, currentMaterial.mainTexture.width, currentMaterial.mainTexture.height), new Vector2());
        image.sprite = imageSprite;

    }

    public void assignLocation(GameObject image, Material material)
    {
        TextMeshProUGUI location = image.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        location.text = material.name;

        imageHold holdImage = image.transform.GetComponent<imageHold>();
        holdImage.location = material.name;

    }
}
