using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;

public class pullHouses : MonoBehaviour
{
    public GameObject houseList; //The list where the houses will be shown
    public Sprite defaultThumb; //The thumbnail if none is present in the house data


    // Start is called before the first frame update
    void Start()
    {
        //Coroutine starts download, with processHouse as the parameter as this is where teh returend data from ParseHouseCollection will go.
        StartCoroutine(Download(processHouse));
    }

    /*
     * Download(System.Action<HouseCollection> caller = null)
     * 
     * Uses the UnityWebRequest framework to pull all houses in the public database for Azima in render and returns the
     * data as a request. Then, ParseHouseCollection(string json) is called, passing the request itself. 
     * 
     * Params)
     * - System.Action<HouseCollection> caller) Used to process the return data, being of type HouseCollection, as in ProcessHouse.
     */
    IEnumerator Download(System.Action<HouseCollection> caller = null)
    {
        //Calls get link to backend
        using (UnityWebRequest request = UnityWebRequest.Get("https://azima.onrender.com/api/house/house/public"))
        {
            request.downloadHandler = new DownloadHandlerBuffer();

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(request.error);
            }
            else
            {
                //if successful, call PraseHouseCollection, data returned passed into processHouse
                Debug.Log(request.downloadHandler.text);
                caller.Invoke(HouseCollection.ParseHouseCollection(request.downloadHandler.text));
            }
        }
    }

    /*
     * Processes the house data to be displayed by saving to house GameObjects
     * 
     * Params)
     * - data: The HouseCollection object returned from Download(), contains all of the public houses
     */
    public void processHouse(HouseCollection data)
    {
        //Create a nonOverWrittenHouse from houseList's House child that will represent the blank house.
        GameObject blankHouse = houseList.transform.GetChild(0).gameObject;
        GameObject nonOverWrittenHouse = blankHouse;

        if (data != null)
        {
            for (int i = 0; i < data.houses.Length; i++)
            {
                if (i == 0)
                {
                    //perform for first house in system
                    assignValues(blankHouse, data.houses[i]);
                }
                else
                {
                    //Create new GameObject based off of nonOverWrittenHouse, settings its parent as the HouseList, and assign values to it.
                    GameObject currentHouse = Instantiate(nonOverWrittenHouse, houseList.transform);
                    currentHouse.transform.SetParent(houseList.transform);
                    currentHouse.transform.SetAsLastSibling();
                    assignValues(currentHouse, data.houses[i]);
                }
            }
        }
        else
        {
            Debug.LogError("The house data was not downloaded");
        }
    }

    /*
     * Assigns the values in the house to its GameObject, including the text variables and the thumbnail 
     * image, if it is present.
     * 
     * Params)
     * - subjectHouse) The GameObject representing the house in the Unity scene
     * - currentHouseData) The pulled House from the backend represented using the House class
     */
    public void assignValues(GameObject subjectHouse, House currentHouseData)
    {
        //Assign the raw data to the house GameObject's houseStore, pushed when displaying house in 360
        subjectHouse.transform.Find("houseStore").GetComponent<houseStorage>().specificHouse = currentHouseData;

        //Get every TextMeshPro object to fill with data
        TextMeshProUGUI locationText = subjectHouse.transform.Find("houseAddress").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI priceText = subjectHouse.transform.Find("Price").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI bedroomText = subjectHouse.transform.Find("BedroomNum").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI bathroomText = subjectHouse.transform.Find("BathroomNum").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI livingRoomText = subjectHouse.transform.Find("LivingRoomNum").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI sqFootageText = subjectHouse.transform.Find("SqFootageNum").GetComponent<TextMeshProUGUI>();
        Image thumbImage = subjectHouse.transform.Find("ShowcaseImage").GetComponent<Image>();
        TextMeshProUGUI authorText = subjectHouse.transform.Find("author").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI houseName = subjectHouse.transform.Find("houseName").GetComponent<TextMeshProUGUI>();

        //Pull thuimbnail image using coroutine
        StartCoroutine(PullThumbImage(currentHouseData, thumbImage));

        //Assign text data to TextMeshPro objects, with ToString() for non-string variables.
        authorText.text = currentHouseData.author;
        locationText.text = currentHouseData.location;
        priceText.text = currentHouseData.price.ToString();
        priceText.text += "/w";
        bedroomText.text = currentHouseData.rooms.ToString();
        bathroomText.text = currentHouseData.bathrooms.ToString();
        livingRoomText.text = currentHouseData.livingAreas.ToString();
        sqFootageText.text = currentHouseData.sqFootage.ToString();
        houseName.text = currentHouseData.houseName;

    }

    /*Pulls the thumbnail image for the house from its link provided in its json data and applies it to the thumb.
     * 
     * Params)
     * - currentHouse: The current house being processed that was pulled from the backend and is represented using the House class
     * - thumb: The image object on which the pulled thumbnail will be applied.
     */
    IEnumerator PullThumbImage(House currentHouse, Image thumb)
    {
        if (currentHouse.thumbnail != null)
        {
            //Web Request using link from House's thumbnial variable.
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(currentHouse.thumbnail);
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(request.error);
            }
            else
            {
                //If successful, apply to Texture2D, then apply to thumb's sprite
                Texture2D imageTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;
                thumb.sprite = Sprite.Create(imageTexture, new Rect(0, 0, imageTexture.width, imageTexture.height), new Vector2 ());

            }
        }
        else
        {
            //Set as the defaultThumb if no thumbnail is present.
            thumb.sprite = defaultThumb;
        }
    }
}
