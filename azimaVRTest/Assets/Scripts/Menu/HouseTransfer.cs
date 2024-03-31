using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HouseTransfer : MonoBehaviour
{
    public void transferHouseDetails()
    {
        HouseData.selectedHouse = GameObject.Find("houseStore").GetComponent<houseStorage>().specificHouse;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
}
