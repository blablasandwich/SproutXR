using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Text;

public class LoginFromAPI : MonoBehaviour
{
    public LoginUIManager libraryCanvas;
    public InputField usernameInputField;
    public InputField passwordInputField;
    public Text feedbackText;
    public bool cardUser = false;
    public static bool LoginWithEmail = false;
    /* Testing Login
     email: example@lucernastudios.com
     pass: 12345678
    */

       // public string API_URL = "http://sproutxr-api.herokuapp.com";

      public string API_URL = "http://api.sproutxr.com";

    private User user;
    private bool authenticated = false;
    private string inputUsername = "";
    private string email = "";
    private string inputPassword = "";    

    [System.Serializable]
    public class User
    {
        public string username = "";
        public string email = "";
        public string password_hash = "";
    }

    public void Start()
    {
        if (Debug.isDebugBuild)
        {
            API_URL = "http://api.lucernalabs.com/";
           // API_URL = "http://sproutxr-api-dev.herokuapp.com";
        }

        user = new User();
        usernameInputField = GameObject.Find("Username/InputField").GetComponent<InputField>();
        passwordInputField = GameObject.Find("Password/InputField").GetComponent<InputField>();
        feedbackText = GameObject.Find("FeedbackText").GetComponent<Text>();
        libraryCanvas = GetComponent<LoginUIManager>();
    }

    public void CheckUser()
    {
            email = usernameInputField.text;
            inputPassword = passwordInputField.text;
            cardUser = false;
            StartCoroutine(GetUser());
    }

    public void LogInCard(string user, string password)
    {
        inputUsername = user;
        inputPassword = password;
        cardUser = true;
        StartCoroutine(GetUser());
    }

    void CheckPassword()
    {
        if (user.password_hash == MD5Hash(inputPassword))
        {
            authenticated = true;            
        }
        else
        {
            Debug.Log("Error: Password does not match.");
            feedbackText.text = "Username/Password Not Found";
        }

        if (authenticated)
        {
            // TODO: Save user info to local session
            NextScene();
        }
    }

    void NextScene()
    {
        Debug.Log("Passwords match!");
        libraryCanvas.OpenLibraryCanvas();
    }

    IEnumerator GetUser()
    {
        string url;
        if (!cardUser)
        {
            url = API_URL + "/api/email/" + email;
        }
        else
        {
            url = API_URL + "/api/username/" + inputUsername;
        }

        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);
            
            user = JsonUtility.FromJson<User>(www.downloadHandler.text);
            // Or retrieve results as binary data
            // byte[] results = www.downloadHandler.data;
        }

        if (!cardUser)
        {
            CheckPassword();
        }
        else if (cardUser)
        {
            NextScene();
        }
    }

    public static string MD5Hash(string text)
    {
        MD5 md5 = new MD5CryptoServiceProvider();

        //compute hash from the bytes of text  
        md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

        //get hash result after compute it  
        byte[] result = md5.Hash;

        StringBuilder strBuilder = new StringBuilder();
        for (int i = 0; i < result.Length; i++)
        {
            //change it into 2 hexadecimal digits  
            //for each byte  
            strBuilder.Append(result[i].ToString("x2"));
        }

        return strBuilder.ToString();
    }
}
