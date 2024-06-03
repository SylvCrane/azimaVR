using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//The house class that every called house uses
[System.Serializable]
public class House
{
    public string houseID; //The House's unique ID
    public Portals[] portals; //The portals associated with a house
    public Images[] images; //The images associated with the house
    public int rooms; //The number of rooms
    public int bathrooms; //# of bathrooms
    public int livingAreas; //# of living areas
    public double sqFootage; //Square footage
    public double price; //The price, usually per week
    public string dateListed; //The date the house was listed
    public string location; //The house's address
    public int kitchen; //# of kitchens
    public bool backyard; //Whether it has a backyard
    public bool laundryRoom; //Whether it has a laundry room
    public string thumbnail; //The thumbnail iumage URL
    public string author; //The house's author
    public string houseName; //The name of the house set by the author

    /*
     * Method used to parse a house from JSON to a house class, now deprecated.
     */
    public static House ParseHouse(string json)
    {
        return JsonUtility.FromJson<House>(json);
    }

    
}
