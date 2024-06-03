using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Used for saving the image data, including the URL.
[System.Serializable]
public class Images 
{
    public string name; //The name of the image
    public string imageURL; //The URL to get the image
    public string houseID; //The house the image is associated with
}
