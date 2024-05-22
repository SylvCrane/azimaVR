using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVRSimpleJSON;

public class userDetails
{
    public string email;
    public string firstName;
    public string lastName;
    public string company;
    public string bio;
    public string location;
    public string profileImage;

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
