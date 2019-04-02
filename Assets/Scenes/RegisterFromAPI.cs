using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
public class RegisterFromAPI : MonoBehaviour {
    public LoginUIManager libraryCanvas;
    public InputField     usernameInputField;
    public InputField     emailInputField;
    public InputField     passwordInputField;
    public InputField     passwordConfirmInputField;
    public Text           feedbackText;
    public TMP_InputField CID_InputField;
    public GameObject     CIDUI;
    public string         API_URL = "http://sproutxr-api-dev.herokuapp.com";

    private User   user;
    private string inputUsername = "";
    private string inputEmail    = "";
    private string inputPassword = "";
    private string cid           = "";

    [System.Serializable]
    public class User {
        public string username      = "";
        public string email         = "";
        public string password_hash = "";
        public string cid           = "";
    }

    public void Start() {
        user                      = new User();
  //    usernameInputField        =  GameObject.Find("").GetComponent<InputField>();
        emailInputField           = GameObject.Find("DisplayInputField").GetComponent<InputField>();
        passwordInputField        = GameObject.Find("PasswordInputField").GetComponent<InputField>();
        passwordConfirmInputField = GameObject.Find("ConfirmPassInputField").GetComponent<InputField>();
        libraryCanvas             = GetComponent<LoginUIManager>();
    }

    public void ValidateCID() {
        if (CID_InputField.text != "") {
            user.cid = CID_InputField.text;
            CIDUI.SetActive(false);
        }
    }

    public void VerifyPasswords() {
        user.username      = usernameInputField.text;
        user.email         = emailInputField.text;
        user.password_hash = passwordInputField.text;

        if (user.password_hash == passwordConfirmInputField.text) {
            // Register
            StartCoroutine(APIPost("/api/user", CreateUserPayload()));
            Debug.Log("Passwords Match");
            // TODO: Verify success
        }

        else {
            Debug.Log("Passwords Dont Match: " + user.password_hash.ToString() + "  -  " + passwordConfirmInputField.text.ToString());
        }
    }


    void NextScene() {
        Debug.Log("Passwords match!");
        libraryCanvas.OpenLibraryCanvas();
    }

    public static string MD5Hash(string text) {
        MD5 md5 = new MD5CryptoServiceProvider();

        //compute hash from the bytes of text  
        md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

        //get hash result after compute it  
        byte[] result = md5.Hash;

        StringBuilder strBuilder = new StringBuilder();
        for (int i = 0; i < result.Length; i++) {
            //change it into 2 hexadecimal digits  
            //for each byte  
            strBuilder.Append(result[i].ToString("x2"));
        }

        return strBuilder.ToString();
    }

    IEnumerator APIPost(string key, string jsonPayload) {
        string url = API_URL + key;
        Debug.Log("Server: " + url);

        var www = new UnityWebRequest(url, "POST");
        byte[] data = System.Text.Encoding.UTF8.GetBytes(jsonPayload);
        www.uploadHandler = (UploadHandler)new UploadHandlerRaw(data);
        www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.Send();
 
        if (www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
        }
        else {
            Debug.Log(www.downloadHandler.text);
            // byte[] results = www.downloadHandler.data;
        }
    }

    /*public static List<string> APIRead(string propertyName, string dataRange)
    {
        //TODO: Implement Read()
        Debug.Log("TelemetryManager.APIRead() is not implemented yet...\nSorry o_o");
        return new List<string>();
    }
    */

    public string addJson<T>(string old, string key, T val) {
        if (old == "") {
            return string.Format("\"{0}\":\"{1}\"", key, val.ToString());
        }
        return string.Format("{0},\"{1}\":\"{2}\"", old, key, val.ToString());
    }

    public string CreateUserPayload() {

        string username      = user.username;
        string email         = user.email;
        string password_hash = MD5Hash(user.password_hash);
        string cid           = user.cid;
        string payload       = "";

        payload = addJson(payload, "username", username);
        payload = addJson(payload, "password_hash", password_hash);
        payload = addJson(payload, "classroom_code", cid);

        string jsonPayload = "{" + payload + "}";

        Debug.Log("<color=red>JSON payload:\n</color>" + jsonPayload);

        return jsonPayload;
    }

}