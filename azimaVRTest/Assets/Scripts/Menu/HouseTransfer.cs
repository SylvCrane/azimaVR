using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HouseTransfer : MonoBehaviour
{
    public GameObject thisHouse;

    public void transferHouseDetails()
    {
        HouseData.selectedHouse = thisHouse.transform.Find("houseStore").GetComponent<houseStorage>().specificHouse;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
}
