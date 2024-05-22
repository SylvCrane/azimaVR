using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DetailsPanelAssignment : MonoBehaviour
{
    public TextMeshProUGUI bedrooms;
    public TextMeshProUGUI bathrooms;
    public TextMeshProUGUI sqFootage;
    public TextMeshProUGUI livingAreas;
    public TextMeshProUGUI price;
    public TextMeshProUGUI address;
    public TextMeshProUGUI dateAvailable;
    public GameObject backyardYes;
    public GameObject backyardNo;
    public GameObject laundryYes;
    public GameObject laundryNo;
    
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
