using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Used to search through houses.
public class SearchFunctionality : MonoBehaviour
{
    public TMP_InputField houseSearch; //The inptu field used for the search
    public GameObject houseList; //The list of houses

    /*
     * Searches through the houseList and returns any houses that contain the search query by setting them to true. 
     * All other houses are set to false.
     */
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

    /*
     * Used to return all houses back to active, used when 'Show All' is pressed
     */
    public void ReturnAllHouses()
    {
        foreach(Transform child in houseList.transform)
        {
            child.gameObject.SetActive(true);
        }
    }
}
