using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using OVRSimpleJSON;

//Used for logging the user into the system and loading their houses.
public class LoginManager : MonoBehaviour
{
    public GameObject Keyboard; //The login keyboard
    public TMP_InputField Username; //The username field
    public TMP_InputField Password; //The password field
    public GameObject houseList; //The list of public houses
    public GameObject LoginHouseArea; //The houseArea for the user's houses
    public GameObject LoginArea; //The login screen
    public userDetails user; //The user themselves as a class
    public TextMeshProUGUI loginFailed; //Text indicating the login failed
    public GameObject userButtonCanvas; //The collection of buttons used to interact between user's houses and public houses

    //Switches input field in keyboard to username
    public void userPressed()
    {
        Keyboard.transform.Find("background").GetComponent<KeyboardFunctionality>().houseSearch = Username;
    }

    //Switches input field in keyboard to password
    public void passPressed()
    {
        Keyboard.transform.Find("background").GetComponent<KeyboardFunctionality>().houseSearch = Password;
    }

    //Turns off keyboard and starts pull
    public void SearchForUser()
    {
        Keyboard.SetActive(false);
        StartCoroutine(UserSearch());

    }

    //Performs a UnityWebRequest to find user. If they are found, the user is loaded.
    IEnumerator UserSearch()
    {
        //Declare new Account, and append user's login and password to it. 
        Account searchAccount = new Account();
        searchAccount.email = Username.text;
        searchAccount.password = Password.text;

        //Set as JSON and send as a post.
        string userJSON = JsonUtility.ToJson(searchAccount);

        using (UnityWebRequest request = UnityWebRequest.Post("https://azima.onrender.com/api/login/", userJSON, "application/json"))
        {
            request.downloadHandler = new DownloadHandlerBuffer();

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(request.error);
            }
            else
            {
                //If the variable status is ok, the user can log in
                JSONNode statusCheck = JSON.Parse(request.downloadHandler.text);
                string status = statusCheck["status"];

                if (status == "ok")
                {
                    //Call new user, and parse their details, then load their houses.
                    loginFailed.text = "";
                    Debug.Log(request.downloadHandler.text);
                    user = new userDetails();
                    user = user.ParseUserDetails(request.downloadHandler.text);
                    loadUser();
                }
                else
                {
                    Debug.Log(request.error);
                    loginFailed.text = "Incorrect username and/or password";
                }
            }
        }
    }

    IEnumerator getUserProfile()
    {
        return null;
    }

    //Sets the LoginHouseArea to true, including the userButtonCanvas, and gets relevant houses.
    public void loadUser()
    {
        userButtonCanvas.SetActive(true);
        getRelevantHouses();
        LoginHouseArea.SetActive(true);
        LoginArea.SetActive(false);
    }

    //Gets houses with an author that matches the user's email and copies them to the user's houseList.
    public void getRelevantHouses()
    {
        for (int i = 0; i < houseList.transform.childCount; i++)
        {
            GameObject house = houseList.transform.GetChild(i).gameObject;

            string houseAuthor = house.transform.Find("author").GetComponent<TextMeshProUGUI>().text;

            if (houseAuthor == user.email)
            {
                house.transform.SetParent(LoginHouseArea.transform.Find("HouseScroll").transform.Find("HouseList"));
                house.transform.SetAsLastSibling();
            }
        }
    }
}
