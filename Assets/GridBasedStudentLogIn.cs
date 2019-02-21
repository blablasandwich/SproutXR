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

        // Start is called before the first frame update
     void Start()
    {
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
    }

    void stuff(UnityWebRequest w)
    {
        ParseJsonToObject(w.downloadHandler.text);

    foreach(MyObject obj in JsonArry)
        {
            GameObject currentStudent = Instantiate(studentProfilePicPrefab, studentCardHolder.transform);
            GridStudentButtonInfo info = currentStudent.GetComponent<GridStudentButtonInfo>();

            //set icon color
            Color c = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
            info.pic.color = c;

            //set gameobjname and user name
            info.studentName.text = obj.username;
            info.name = obj.username;
        }
    }

    public IEnumerator CheckDataBase()
    {
        using (UnityWebRequest w = UnityWebRequest.Get("http://sproutxr-api-dev.herokuapp.com/api/classroom/15/user"))
        {
            yield return w.SendWebRequest();

            if (w.isNetworkError || w.isHttpError)
            {
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
