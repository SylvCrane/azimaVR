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

            parseDate(newHouses.houses[houseNum], house);
           // newHouses.houses[houseNum].dateListed = house["dateListed"];
            newHouses.houses[houseNum].location = house["location"];
            newHouses.houses[houseNum].kitchen = house["kitchen"].AsInt;
            newHouses.houses[houseNum].backyard = house["backyard"].AsBool;
            newHouses.houses[houseNum].laundryRoom = house["laundryRoom"].AsBool;

            processPortal(newHouses.houses[houseNum], house);

            houseNum++;
        }

        return newHouses;
    }

    public static void parseDate(House currentHouse, JSONNode jsonHouse)
    {
        string houseDate = jsonHouse["dateListed"];
        string year = "";
        string month = "";
        string day = "";

        int i = 0;

        while (i < 10)
        {
            if (i < 4)
            {
                year += houseDate[i];
            }
            else if ((i > 4) && (i < 7))
            {
                month += houseDate[i];
            }
            else if ((i > 7) && (i < 10))
            {
                day += houseDate[i];
            }

            i++;
        }

        currentHouse.dateListed = day + "/" + month + "/" + year;
    }

    public static void processPortal(House currentHouse, JSONNode jsonHouse)
    {
        JSONArray portalCollection = jsonHouse["portals"].AsArray;
        currentHouse.portals = new Portals[portalCollection.Count];

        for (int i = 0; i < portalCollection.Count; i++)
        {
            currentHouse.portals[i] = new Portals();
            currentHouse.portals[i].triangles = new Triangles[4];

            currentHouse.portals[i].triangles[0] = new Triangles();
            currentHouse.portals[i].triangles[1] = new Triangles();
            currentHouse.portals[i].triangles[2] = new Triangles();
            currentHouse.portals[i].triangles[3] = new Triangles();

            currentHouse.portals[i].textData = new textData();
            currentHouse.portals[i].textData.rotation = new Rotation();
        }

        foreach (JSONNode portal in portalCollection)
        {
            int i = 0;
            int j = 0;

            JSONArray textData = portal["textData"].AsArray;
            JSONNode rotation;

            foreach(JSONNode textDataSpecific in textData)
            {
                rotation = textDataSpecific["rotation"];
                currentHouse.portals[i].textData.rotation.x = rotation["x"].AsDouble;
                currentHouse.portals[i].textData.rotation.y = rotation["y"].AsDouble;
                currentHouse.portals[i].textData.rotation.z = rotation["z"].AsDouble;

                currentHouse.portals[i].textData.value = textDataSpecific["value"];
                currentHouse.portals[i].textData.position = textDataSpecific["position"];
            }

            JSONArray triangleCollection = portal["triangles"].AsArray;

            

            foreach (JSONNode triangle in triangleCollection)
            {
                currentHouse.portals[i].triangles[j].vertexA = triangle["vertexA"];
                currentHouse.portals[i].triangles[j].vertexB = triangle["vertexB"];
                currentHouse.portals[i].triangles[j].vertexC = triangle["vertexC"];
                currentHouse.portals[i].triangles[j].color = triangle["color"];

                j++;
            }

            currentHouse.portals[i].destination = portal["destination"];
            currentHouse.portals[i].location = portal["location"];

            i++;
        }
    }
}
