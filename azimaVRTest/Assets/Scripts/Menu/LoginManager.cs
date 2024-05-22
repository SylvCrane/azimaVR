using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using OVRSimpleJSON;

public class LoginManager : MonoBehaviour
{
    public GameObject Keyboard;
    public TMP_InputField Username;
    public TMP_InputField Password;
    public GameObject houseList;
    public GameObject LoginHouseArea;
    public GameObject LoginArea;
    public userDetails user;
    public TextMeshProUGUI loginFailed;
    public GameObject userButtonCanvas;

    public void userPressed()
    {
        Keyboard.transform.Find("background").GetComponent<KeyboardFunctionality>().houseSearch = Username;
    }

    public void passPressed()
    {
        Keyboard.transform.Find("background").GetComponent<KeyboardFunctionality>().houseSearch = Password;
    }

    public void SearchForUser()
    {
        Keyboard.SetActive(false);
        StartCoroutine(UserSearch());

    }

    IEnumerator UserSearch()
    {
        Account searchAccount = new Account();
        searchAccount.email = Username.text;
        searchAccount.password = Password.text;

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
                JSONNode statusCheck = JSON.Parse(request.downloadHandler.text);
                string status = statusCheck["status"];

                if (status == "ok")
                {
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

    public void loadUser()
    {
        userButtonCanvas.SetActive(true);
        getRelevantHouses();
        LoginHouseArea.SetActive(true);
        LoginArea.SetActive(false);
    }

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
