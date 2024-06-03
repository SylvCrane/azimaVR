using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Sets the infoPlane text based on the data in houseData
public class DetailsPanelAssignment : MonoBehaviour
{
    public TextMeshProUGUI bedrooms; //The text object for the # of bedrooms
    public TextMeshProUGUI bathrooms; //The text object for the # of bathrooms
    public TextMeshProUGUI sqFootage; //The text object for the square footage
    public TextMeshProUGUI livingAreas; //The text object for the # of living areas
    public TextMeshProUGUI price; //The text object for the price
    public TextMeshProUGUI address; //The text object for the address
    public TextMeshProUGUI dateAvailable; //The text object for the date
    public GameObject backyardYes; //The Gameobject representing there being a backyard
    public GameObject backyardNo; //The Gameobject representing there being no backyard
    public GameObject laundryYes; //The Gameobject representing there being a laundry room
    public GameObject laundryNo; //The Gameobject representing there being no laundry room

    // Start is called before the first frame update
    void Start()
    {
        //Assign all of the data to the details panel in the user's right hand
        bedrooms.text = HouseData.selectedHouse.rooms.ToString();
        bathrooms.text = HouseData.selectedHouse.bathrooms.ToString();
        sqFootage.text = HouseData.selectedHouse.sqFootage.ToString();
        livingAreas.text = HouseData.selectedHouse.livingAreas.ToString();
        price.text = HouseData.selectedHouse.price.ToString();
        address.text = HouseData.selectedHouse.location.ToString();
        dateAvailable.text = HouseData.selectedHouse.dateListed.ToString();
        
        //Conditionals for the backyard and laundry booleans
        if (HouseData.selectedHouse.backyard == true)
        {
            backyardYes.SetActive(true);
        }
        else
        {
            backyardNo.SetActive(true);
        }

        if (HouseData.selectedHouse.laundryRoom == true)
        {
            laundryYes.SetActive(true);
        }
        else
        {
            laundryNo.SetActive(true);
        }
    }

}
