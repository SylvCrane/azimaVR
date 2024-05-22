using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;

public class pullHouses : MonoBehaviour
{
    public GameObject houseList;
    public Sprite defaultThumb;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Download(processHouse));
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Download(System.Action<HouseCollection> caller = null)
    {
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
                Debug.Log(request.downloadHandler.text);
                caller.Invoke(HouseCollection.ParseHouseCollection(request.downloadHandler.text));
            }
        }
    }

    public void processHouse(HouseCollection data)
    {
        GameObject blankHouse = houseList.transform.GetChild(0).gameObject;
        GameObject nonOverWrittenHouse = blankHouse;

        if (data != null)
        {
            for (int i = 0; i < data.houses.Length; i++)
            {
                if (i == 0)
                {
                    assignValues(blankHouse, data.houses[i]);
                }
                else
                {
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

    public void assignValues(GameObject subjectHouse, House currentHouseData)
    {
        subjectHouse.transform.Find("houseStore").GetComponent<houseStorage>().specificHouse = currentHouseData;

        TextMeshProUGUI locationText = subjectHouse.transform.Find("houseAddress").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI priceText = subjectHouse.transform.Find("Price").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI bedroomText = subjectHouse.transform.Find("BedroomNum").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI bathroomText = subjectHouse.transform.Find("BathroomNum").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI livingRoomText = subjectHouse.transform.Find("LivingRoomNum").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI sqFootageText = subjectHouse.transform.Find("SqFootageNum").GetComponent<TextMeshProUGUI>();
        Image thumbImage = subjectHouse.transform.Find("ShowcaseImage").GetComponent<Image>();
        TextMeshProUGUI authorText = subjectHouse.transform.Find("author").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI houseName = subjectHouse.transform.Find("houseName").GetComponent<TextMeshProUGUI>();

        StartCoroutine(PullThumbImage(currentHouseData, thumbImage));

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

    IEnumerator PullThumbImage(House currentHouse, Image thumb)
    {
        if (currentHouse.thumbnail != null)
        {
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(currentHouse.thumbnail);
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(request.error);
            }
            else
            {
                Texture2D imageTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;
                thumb.sprite = Sprite.Create(imageTexture, new Rect(0, 0, imageTexture.width, imageTexture.height), new Vector2 ());

            }
        }
        else
        {
            thumb.sprite = defaultThumb;
        }
    }

 
}
