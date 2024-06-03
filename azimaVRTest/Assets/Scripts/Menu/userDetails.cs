using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVRSimpleJSON;

//Used to save the user's pulled userData when they log in.
public class userDetails
{
    public string email; //The user's email
    public string firstName; //The user's first name
    public string lastName; //The user's last name
    public string company; //The user's company they are associated with
    public string bio; //The user's bio
    public string location; //The user's location
    public string profileImage; //The profile image of the user.

    /*
     * Parses the JSON string containing the user's data into vorkeable data
     * 
     * Returns the userDetails.
     * 
     * Params)
     * - json) The string containing the user's data in JSON form.
     */
    public userDetails ParseUserDetails(string json)
    {
        //because of how the simpleJSON works, the json needs to be parsed as an array, even
        //though it is only one document of data.
        userDetails user = new userDetails();
        JSONNode userJSON = JSON.Parse(json);
        JSONNode userDetailsJSON = userJSON["user"];

        //Parse the details of the user
        user.firstName = userDetailsJSON["firstName"];
        user.lastName = userDetailsJSON["lastName"];
        user.email = userDetailsJSON["email"];
        user.bio = userDetailsJSON["bio"];
        user.location = userDetailsJSON["location"];
        user.profileImage = userDetailsJSON["profileImage"];
        user.company = userDetailsJSON["company"];

        return user;
    }
}
