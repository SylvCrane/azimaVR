using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RoomLoader : MonoBehaviour
{
    public GameObject sphere;
    public Material[] materialCollection;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(loadImages());
    }

    private IEnumerator loadImages()
    {
        yield return new WaitForSeconds(5f);

        materialCollection = new Material[HouseData.selectedHouse.images.Length];

        for (int i = 0; i < HouseData.selectedHouse.images.Length; i++)
        {
            using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(HouseData.selectedHouse.images[i].imageURL))
            {

                yield return request.SendWebRequest();

                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(request.error);
                }
                else
                {
                    Material newMaterial = null;
                    var texture = DownloadHandlerTexture.GetContent(request);

                    newMaterial.mainTexture = texture;
                    materialCollection[i] = newMaterial;
                }
            }
        }
    }
     
}
