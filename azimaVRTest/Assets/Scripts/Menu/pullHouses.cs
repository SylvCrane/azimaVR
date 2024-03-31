using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class pullHouses : MonoBehaviour
{
    public House tempHouses;
    public GameObject MenuHouse;


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
        using (UnityWebRequest request = UnityWebRequest.Get("http://localhost:8082/api/house/house/puller/Testing"))
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

        if (data != null)
        {
            Debug.Log("House downloaded!");
            tempHouses.houseID = data.houses[0].houseID;
            tempHouses.rooms = data.houses[0].rooms;
            tempHouses.bathrooms = data.houses[0].bathrooms;
            tempHouses.livingAreas = data.houses[0].livingAreas;
            tempHouses.sqFootage = data.houses[0].sqFootage;
            tempHouses.price = data.houses[0].price;
            tempHouses.dateListed = data.houses[0].dateListed;
            tempHouses.location = data.houses[0].location;
            tempHouses.kitchen = data.houses[0].kitchen;
            tempHouses.backyard = data.houses[0].backyard;
            tempHouses.laundryRoom = data.houses[0].laundryRoom;

            for (int i = 0; i < data.houses[0].images.Length; i++)
            {
                tempHouses.images[i].name = data.houses[0].images[i].name;
                tempHouses.images[i].imageURL = data.houses[0].images[i].imageURL;
                tempHouses.images[i].houseID = data.houses[0].houseID;
            }

            MenuHouse.transform.Find("houseStore").GetComponent<houseStorage>().specificHouse = tempHouses;
            assignValues();
        }
        else
        {
            Debug.LogError("The house data was not downloaded");
        }
    }

    public void assignValues()
    {
        TextMeshProUGUI locationText = MenuHouse.transform.Find("houseName").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI priceText = MenuHouse.transform.Find("Price").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI bedroomText = MenuHouse.transform.Find("BedroomNum").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI bathroomText = MenuHouse.transform.Find("BathroomNum").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI livingRoomText = MenuHouse.transform.Find("LivingRoomNum").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI sqFootageText = MenuHouse.transform.Find("SqFootageNum").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI kitchenText = MenuHouse.transform.Find("KitchenNum").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI backyardText = MenuHouse.transform.Find("Backyard?").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI laundryRoomText = MenuHouse.transform.Find("LaundryRoom?").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI dateText = MenuHouse.transform.Find("DateTimeNum").GetComponent<TextMeshProUGUI>();

        locationText.text = tempHouses.location;
        priceText.text = tempHouses.price.ToString();
        bedroomText.text = tempHouses.rooms.ToString();
        bathroomText.text = tempHouses.bathrooms.ToString();
        livingRoomText.text = tempHouses.livingAreas.ToString();
        sqFootageText.text = tempHouses.sqFootage.ToString();
        kitchenText.text = tempHouses.kitchen.ToString();

        backyardText.text = (tempHouses.backyard) ? "True" : "False";
        laundryRoomText.text = (tempHouses.laundryRoom) ? "True" : "False";

        dateText.text = tempHouses.dateListed;
    }
}
