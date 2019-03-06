using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GridBasedStudentLogIn : MonoBehaviour
{
    public GameObject studentProfilePicPrefab;
    public GameObject studentCardHolder;
    List<MyObject> JsonArry;
    public Text URLID;
    public void ClickBtn()
    {
      //  string id = inputid.text;
        StartCoroutine("CheckDataBase");
    }


    void ParseJsonToObject(string json)
    {
        var wrappedjsonArray = JsonUtility.FromJson<MyWrapper>(json);
        JsonArry = wrappedjsonArray.users;
    }

    [System.Serializable]
    private class MyWrapper
    {
        public List<MyObject> users;
    }

    [System.Serializable]
    private class MyObject
    {
        public string username;
        public string password_hash;
    }

    void stuff(UnityWebRequest w)
    {
        ParseJsonToObject(w.downloadHandler.text);

            foreach (MyObject obj in JsonArry)
            {
                GameObject currentStudent = Instantiate(studentProfilePicPrefab, studentCardHolder.transform);
                GridStudentButtonInfo info = currentStudent.GetComponent<GridStudentButtonInfo>();

                //set icon color
                Color c = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
                info.pic.color = c;

                //set gameobjname and user name
                info.studentName.text = obj.username;
                info.name = obj.username;
            info.password = obj.password_hash;
            }
        }

    public IEnumerator CheckDataBase()
    {
        switch (URLID.text)
        {
            case "15":
                URLID.text = "http://sproutxr-api-dev.herokuapp.com/api/classroom/15/user";
                break;
            case "14":
                URLID.text = "http://sproutxr-api-dev.herokuapp.com/api/classroom/14/user";
                break;
            case "13":
                URLID.text = "http://sproutxr-api-dev.herokuapp.com/api/classroom/13/user";
                break;
            case "12":
                URLID.text = "http://sproutxr-api-dev.herokuapp.com/api/classroom/12/user";
                break;
            case "11":
                URLID.text = "http://sproutxr-api-dev.herokuapp.com/api/classroom/11/user";
                break;
            case "10":
                URLID.text = "http://sproutxr-api-dev.herokuapp.com/api/classroom/10/user";
                break;
            case "9":
                URLID.text = "http://sproutxr-api-dev.herokuapp.com/api/classroom/9/user";
                break;
            case "8":
                URLID.text = "http://sproutxr-api-dev.herokuapp.com/api/classroom/8/user";
                break;
            case "7":
                URLID.text = "http://sproutxr-api-dev.herokuapp.com/api/classroom/7/user";
                break;
            case "6":
                URLID.text = "http://sproutxr-api-dev.herokuapp.com/api/classroom/6/user";
                break;
            case "5":
                URLID.text = "http://sproutxr-api-dev.herokuapp.com/api/classroom/5/user";
                break;
            case "4":
                URLID.text = "http://sproutxr-api-dev.herokuapp.com/api/classroom/4/user";
                break;
            case "3":
                URLID.text = "http://sproutxr-api-dev.herokuapp.com/api/classroom/3/user";
                break;
            case "2":
                URLID.text = "http://sproutxr-api-dev.herokuapp.com/api/classroom/2/user";
                break;
            case "1":
                URLID.text = "http://sproutxr-api-dev.herokuapp.com/api/classroom/1/user";
                break;
            default:
                URLID.text = "http://sproutxr-api-dev.herokuapp.com/api/classroom/0/user";
                break;
        }
        using (UnityWebRequest w = UnityWebRequest.Get("http://sproutxr-api-dev.herokuapp.com/api/classroom/15/user"))
        {
            yield return w.SendWebRequest();

            if (w.isNetworkError || w.isHttpError)
            {
                Debug.Log(URLID.text);
                Debug.Log("Error: " + w.error);
            }
            else
            {
                Debug.Log(w.downloadHandler.text);

                stuff(w);
            }
        }
    }
}
