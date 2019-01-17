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
    public InputField emailInputField;
    public InputField passwordInputField;
    public Text feedbackText;

    public string API_URL = "http://sproutxr-api-dev.herokuapp.com";

    private User user;
    private bool authenticated = false;
    private string inputEmail = "";
    private string inputPassword = "";    

    [System.Serializable]
    public class User
    {
        public string email = "";
        public string password_hash = "";
    }

    public void Start()
    {
        user = new User();
        emailInputField = GameObject.Find("Email/InputField").GetComponent<InputField>();
        passwordInputField = GameObject.Find("Password/InputField").GetComponent<InputField>();
        feedbackText = GameObject.Find("FeedbackText").GetComponent<Text>();
        libraryCanvas = GetComponent<LoginUIManager>();
    }

    public void VerifyCredentials()
    {
        inputEmail = emailInputField.text;
        inputPassword = passwordInputField.text;

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
            // TODO: Tell the user the password is not good
            feedbackText.text = "Email/Password Not Found";
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
        string url = API_URL + "/api/email/" + inputEmail;
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            // Debug.Log(www.downloadHandler.text);
            
            user = JsonUtility.FromJson<User>(www.downloadHandler.text);
            // Or retrieve results as binary data
            // byte[] results = www.downloadHandler.data;
        }

        CheckPassword();
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
