using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class House
{
    public string houseID;
    public Portals[] portals;
    public Images[] images;
    public int rooms;
    public int bathrooms;
    public int livingAreas;
    public double sqFootage;
    public double price;
    public DateTime dateListed;
    public string location;
    public int kitchen;
    public bool backyard;
    public bool laundryRoom;

    public static House ParseHouse(string json)
    {
        return JsonUtility.FromJson<House>(json);
    }
}
