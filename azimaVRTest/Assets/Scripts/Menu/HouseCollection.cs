using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVRSimpleJSON;

public class HouseCollection
{
    public House[] houses;

    public static HouseCollection ParseHouseCollection(string json)
    {
        HouseCollection newHouses = new HouseCollection();
         
        JSONArray houseCollectionJSON = JSON.Parse(json).AsArray;

        newHouses.houses = new House[houseCollectionJSON.Count];

        for (int i = 0; i < houseCollectionJSON.Count; i++)
        {
            newHouses.houses[i] = new House();
        }

        int houseNum = 0;

        foreach (JSONNode house in houseCollectionJSON)
        {
            Debug.Log(house["houseID"]);
            newHouses.houses[houseNum].houseID = house["houseID"];
            Debug.Log(house[newHouses.houses[houseNum].houseID]);
            //houses[houseNum].portals = house["portals"];

            JSONArray imagesCollection = house["images"].AsArray;
            newHouses.houses[houseNum].images = new Images[imagesCollection.Count];

            for (int i = 0; i < imagesCollection.Count; i++)
            {
                newHouses.houses[houseNum].images[i] = new Images();
            }

            int imageNum = 0;

            foreach (JSONNode image in imagesCollection)
            {
                newHouses.houses[houseNum].images[imageNum].houseID = image["houseID"];
                newHouses.houses[houseNum].images[imageNum].imageURL = image["imageURL"];
                newHouses.houses[houseNum].images[imageNum].name = image["name"];

                imageNum++;
            }

            newHouses.houses[houseNum].rooms = house["rooms"].AsInt;
            newHouses.houses[houseNum].bathrooms = house["bathrooms"].AsInt;
            newHouses.houses[houseNum].livingAreas = house["livingAreas"].AsInt;
            newHouses.houses[houseNum].sqFootage = house["livingAreas"].AsDouble;
            newHouses.houses[houseNum].price = house["price"].AsDouble;
            newHouses.houses[houseNum].dateListed = house["dateListed"];
            newHouses.houses[houseNum].location = house["location"];
            newHouses.houses[houseNum].kitchen = house["kitchen"].AsInt;
            newHouses.houses[houseNum].backyard = house["backyard"].AsBool;
            newHouses.houses[houseNum].laundryRoom = house["laundryRoom"].AsBool;

            houseNum++;

        }

        return newHouses;
    }
}
