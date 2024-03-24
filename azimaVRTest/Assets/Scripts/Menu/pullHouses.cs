using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class pullHouses : MonoBehaviour
{
    public House housePulled;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Download(processHouse));
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Download(System.Action<House> caller = null)
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
                caller.Invoke(House.ParseHouse(request.downloadHandler.text));
            }
        }
    }

    public void processHouse(House data)
    {
        if (data != null)
        {
            Debug.Log("House downloaded!");
            housePulled.houseID = data.houseID;
            housePulled.rooms = data.rooms;
            housePulled.bathrooms = data.bathrooms;
            housePulled.livingAreas = data.livingAreas;
            housePulled.sqFootage = data.sqFootage;
            housePulled.price = data.price;
            housePulled.dateListed = data.dateListed;
            housePulled.location = data.location;
            housePulled.kitchen = data.kitchen;
            housePulled.backyard = data.backyard;
            housePulled.laundryRoom = data.laundryRoom;

            for (int i = 0; i < data.images.Length; i++)
            {
                housePulled.images[i].name = data.images[i].name;
                housePulled.images[i].imageURL = data.images[i].imageURL;
                housePulled.images[i].houseID = data.houseID;
            }
        }
        else
        {
            Debug.LogError("The house data was not downloaded");
        }
    }
}
