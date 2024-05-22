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

        JSONNode HouseDataCollectionNode = JSON.Parse(json);
        JSONArray houseCollectionJSON = HouseDataCollectionNode["data"].AsArray;

        //JSONArray houseCollectionJSON = JSON.Parse(HouseDataCollectionString).AsArray;

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

            JSONNode sqFootage = house["sqFootage"];
            JSONNode price = house["price"];

            newHouses.houses[houseNum].sqFootage = sqFootage["$numberDecimal"].AsDouble;
            newHouses.houses[houseNum].price = price["$numberDecimal"].AsDouble;
            newHouses.houses[houseNum].author = house["author"];

            parseDate(newHouses.houses[houseNum], house);
           // newHouses.houses[houseNum].dateListed = house["dateListed"];
            newHouses.houses[houseNum].location = house["location"];
            newHouses.houses[houseNum].kitchen = house["kitchen"].AsInt;
            newHouses.houses[houseNum].backyard = house["backyard"].AsBool;
            newHouses.houses[houseNum].laundryRoom = house["laundryRoom"].AsBool;
            newHouses.houses[houseNum].houseName = house["houseName"];
            
            if (house["thumbnail"] != null)
            {
                newHouses.houses[houseNum].thumbnail = house["thumbnail"];
            }

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

        int index = 0;

        foreach (JSONNode portal in portalCollection)
        {
            
            int j = 0;

            JSONArray textData = portal["textData"].AsArray;
            JSONNode rotation;

            foreach(JSONNode textDataSpecific in textData)
            {
                rotation = textDataSpecific["rotation"];
                currentHouse.portals[index].textData.rotation.x = rotation["x"].AsDouble;
                currentHouse.portals[index].textData.rotation.y = rotation["y"].AsDouble;
                currentHouse.portals[index].textData.rotation.z = rotation["z"].AsDouble;

                currentHouse.portals[index].textData.value = textDataSpecific["value"];
                currentHouse.portals[index].textData.position = textDataSpecific["position"];
            }

            JSONArray triangleCollection = portal["triangles"].AsArray;

            

            foreach (JSONNode triangle in triangleCollection)
            {
                currentHouse.portals[index].triangles[j].vertexA = triangle["vertexA"];
                currentHouse.portals[index].triangles[j].vertexB = triangle["vertexB"];
                currentHouse.portals[index].triangles[j].vertexC = triangle["vertexC"];
                currentHouse.portals[index].triangles[j].color = triangle["color"];

                j++;
            }

            currentHouse.portals[index].destination = portal["destination"];
            currentHouse.portals[index].location = portal["location"];

            index++;
        }
    }
}
