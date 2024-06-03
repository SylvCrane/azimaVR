using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

//Used to transfer the raw house data to the Room scene, called when a user wants to explore a house/tour.
public class HouseTransfer : MonoBehaviour
{
    //The GameObject house the data is attached to
    public GameObject thisHouse;

    /*
     * Gets houseStore, which contains raw house data, and assigns to houseData script, which contains global house.
     */
    public void transferHouseDetails()
    {
        HouseData.selectedHouse = thisHouse.transform.Find("houseStore").GetComponent<houseStorage>().specificHouse;

        //Loads room
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
}
