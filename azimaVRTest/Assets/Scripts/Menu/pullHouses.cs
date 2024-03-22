using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class pullHouses : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Download());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Download()
    {
        using (UnityWebRequest request = UnityWebRequest.Get("http://localhost:8082/routes/api/house/puller/HouseTest1"))
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
            }
        }
    }
}
