using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class pullHouses : MonoBehaviour
{
    public House tempHouses;
    public GameObject houseList;


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
        using (UnityWebRequest request = UnityWebRequest.Get("http://localhost:8082/api/house"))
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
        tempHouses = new House();
        tempHouses.images = new Images[data.houses[0].images.Length];

        for (int i = 0; i < tempHouses.images.Length; i++)
        {
            tempHouses.images[i] = new Images();
        }

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

        TextMeshProUGUI locationText = subjectHouse.transform.Find("houseName").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI priceText = subjectHouse.transform.Find("Price").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI bedroomText = subjectHouse.transform.Find("BedroomNum").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI bathroomText = subjectHouse.transform.Find("BathroomNum").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI livingRoomText = subjectHouse.transform.Find("LivingRoomNum").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI sqFootageText = subjectHouse.transform.Find("SqFootageNum").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI kitchenText = subjectHouse.transform.Find("KitchenNum").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI backyardText = subjectHouse.transform.Find("Backyard?").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI laundryRoomText = subjectHouse.transform.Find("LaundryRoom?").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI dateText = subjectHouse.transform.Find("DateTimeNum").GetComponent<TextMeshProUGUI>();

        locationText.text = currentHouseData.location;
        priceText.text = currentHouseData.price.ToString();
        bedroomText.text = currentHouseData.rooms.ToString();
        bathroomText.text = currentHouseData.bathrooms.ToString();
        livingRoomText.text = currentHouseData.livingAreas.ToString();
        sqFootageText.text = currentHouseData.sqFootage.ToString();
        kitchenText.text = currentHouseData.kitchen.ToString();

        backyardText.text = (currentHouseData.backyard) ? "True" : "False";
        laundryRoomText.text = (currentHouseData.laundryRoom) ? "True" : "False";

        dateText.text = currentHouseData.dateListed;
    }
}
