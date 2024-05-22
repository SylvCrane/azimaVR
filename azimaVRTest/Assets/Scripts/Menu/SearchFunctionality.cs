using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SearchFunctionality : MonoBehaviour
{
    public TMP_InputField houseSearch;
    public GameObject houseList;

    public void SearchForHouses()
    {
        foreach(Transform child in houseList.transform)
        {
            GameObject address = child.transform.GetChild(0).gameObject;
            if (!address.GetComponent<TextMeshProUGUI>().text.Contains(houseSearch.text))
            {
                child.gameObject.SetActive(false);
            }
            else
            {
                child.gameObject.SetActive(true);
            }

        }
    }

    public void ReturnAllHouses()
    {
        foreach(Transform child in houseList.transform)
        {
            child.gameObject.SetActive(true);
        }
    }
}
