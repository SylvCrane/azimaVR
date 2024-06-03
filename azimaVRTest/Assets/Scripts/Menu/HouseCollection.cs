using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVRSimpleJSON;

//Used to parse collection of houses
public class HouseCollection
{
    //House array
    public House[] houses;

    /*
     * Used to parse the JSON Data containing each house into useable house data. 
     * 
     * Returns a houseCollection of useable houses.
     * 
     * Params;
     * - json) The string containing the public houses in JSON
     */
    public static HouseCollection ParseHouseCollection(string json)
    {
        HouseCollection newHouses = new HouseCollection();

        //Declare JSONArray from SimpleJSON, then set collection of houses to number of objects in array
        JSONNode HouseDataCollectionNode = JSON.Parse(json);
        JSONArray houseCollectionJSON = HouseDataCollectionNode["data"].AsArray;
        newHouses.houses = new House[houseCollectionJSON.Count];

        //Instantiate each new house
        for (int i = 0; i < houseCollectionJSON.Count; i++)
        {
            newHouses.houses[i] = new House();
        }

        int houseNum = 0;

        //Loop through each of the JSON houses to assign to houseCollectionJSON
        foreach (JSONNode houseJSON in houseCollectionJSON)
        {
            newHouses.houses[houseNum].houseID = houseJSON["houseID"];

            //Set collection of images
            JSONArray imagesCollection = houseJSON["images"].AsArray;
            newHouses.houses[houseNum].images = new Images[imagesCollection.Count];

            //Instantiate each new image
            for (int i = 0; i < imagesCollection.Count; i++)
            {
                newHouses.houses[houseNum].images[i] = new Images();
            }

            int imageNum = 0;

            //Set each piece of data to the images in the houseCollection.
            foreach (JSONNode image in imagesCollection)
            {
                newHouses.houses[houseNum].images[imageNum].houseID = image["houseID"];
                newHouses.houses[houseNum].images[imageNum].imageURL = image["imageURL"];
                newHouses.houses[houseNum].images[imageNum].name = image["name"];

                imageNum++;
            }

            newHouses.houses[houseNum].rooms = houseJSON["rooms"].AsInt;
            newHouses.houses[houseNum].bathrooms = houseJSON["bathrooms"].AsInt;
            newHouses.houses[houseNum].livingAreas = houseJSON["livingAreas"].AsInt;

            JSONNode sqFootage = houseJSON["sqFootage"];
            JSONNode price = houseJSON["price"];

            newHouses.houses[houseNum].sqFootage = sqFootage["$numberDecimal"].AsDouble;
            newHouses.houses[houseNum].price = price["$numberDecimal"].AsDouble;
            newHouses.houses[houseNum].author = houseJSON["author"];

            //Date is complicated in JSON form, so separate function declared
            parseDate(newHouses.houses[houseNum], houseJSON);
            newHouses.houses[houseNum].location = houseJSON["location"];
            newHouses.houses[houseNum].kitchen = houseJSON["kitchen"].AsInt;
            newHouses.houses[houseNum].backyard = houseJSON["backyard"].AsBool;
            newHouses.houses[houseNum].laundryRoom = houseJSON["laundryRoom"].AsBool;
            newHouses.houses[houseNum].houseName = houseJSON["houseName"];
            
            if (houseJSON["thumbnail"] != null)
            {
                newHouses.houses[houseNum].thumbnail = houseJSON["thumbnail"];
            }

            processPortal(newHouses.houses[houseNum], houseJSON);

            houseNum++;
        }

        return newHouses;
    }

    /*
     * Parses the date value
     * 
     * Params)
     * - currentHouse) THe current house that ius being processed
     * - jsonHouse) the JSON data for that house
     */
    public static void parseDate(House currentHouse, JSONNode jsonHouse)
    {
        //Set houseData to loop through, and empty strings for date
        string houseDate = jsonHouse["dateListed"];
        string year = "";
        string month = "";
        string day = "";

        int i = 0;

        //Data ends at 10th string, so loops until 11th character
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

        //Append as abbreviated date
        currentHouse.dateListed = day + "/" + month + "/" + year;
    }

    /*
     * Processes the portal data into the house.
     * 
     * Params) 
     * - currentHouse) The current house that is being processed
     * - jsonHouse) The JSON data for that house
     * 
     */
    public static void processPortal(House currentHouse, JSONNode jsonHouse)
    {
        //Assign portal data to a JSONArray and make currentHouse portals be the same number of portals as in the array
        JSONArray portalCollection = jsonHouse["portals"].AsArray;
        currentHouse.portals = new Portals[portalCollection.Count];

        //Declare each portal for the currentHouse depending on the number of portals
        for (int i = 0; i < portalCollection.Count; i++)
        {
            currentHouse.portals[i] = new Portals();
            currentHouse.portals[i].triangles = new Triangles[4];

            //Each portal has four triangles
            currentHouse.portals[i].triangles[0] = new Triangles();
            currentHouse.portals[i].triangles[1] = new Triangles();
            currentHouse.portals[i].triangles[2] = new Triangles();
            currentHouse.portals[i].triangles[3] = new Triangles();

            currentHouse.portals[i].textData = new textData();
            currentHouse.portals[i].textData.rotation = new Rotation();
        }

        int currentPortal = 0;

        //Loop through each portal in the collection
        foreach (JSONNode portal in portalCollection)
        {
            //Needed for looping through the four triangles
            int currentTriangle = 0;

            //Because of how it is saved in backend, singular textData is set as array
            JSONArray textData = portal["textData"].AsArray;
            JSONNode rotation;

            //Only loops once on account of there being one piece of data
            foreach(JSONNode textDataSpecific in textData)
            {
                //Rotation of text in portal, contains x, y and z data
                rotation = textDataSpecific["rotation"];
                currentHouse.portals[currentPortal].textData.rotation.x = rotation["x"].AsDouble;
                currentHouse.portals[currentPortal].textData.rotation.y = rotation["y"].AsDouble;
                currentHouse.portals[currentPortal].textData.rotation.z = rotation["z"].AsDouble;

                currentHouse.portals[currentPortal].textData.value = textDataSpecific["value"];
                currentHouse.portals[currentPortal].textData.position = textDataSpecific["position"];
            }

            //Four triangles are set to this JSONArray
            JSONArray triangleCollection = portal["triangles"].AsArray;

            

            foreach (JSONNode triangle in triangleCollection)
            {
                //Each triangle has three vertexes and a color, though the color is the same for all triangles
                currentHouse.portals[currentPortal].triangles[currentTriangle].vertexA = triangle["vertexA"];
                currentHouse.portals[currentPortal].triangles[currentTriangle].vertexB = triangle["vertexB"];
                currentHouse.portals[currentPortal].triangles[currentTriangle].vertexC = triangle["vertexC"];
                currentHouse.portals[currentPortal].triangles[currentTriangle].color = triangle["color"];

                currentTriangle++;
            }

            //Destination here is what is set as the text for the portal, the location is the room it will be in when the 'Explore' section begins
            currentHouse.portals[currentPortal].destination = portal["destination"];
            currentHouse.portals[currentPortal].location = portal["location"];

            currentPortal++;
        }
    }
}
