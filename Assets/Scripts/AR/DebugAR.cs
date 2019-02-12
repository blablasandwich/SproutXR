using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugAR : MonoBehaviour
{
    // Displays text on AR screen for debug on the phone
    public Text debugText;

    public void Log<T> (T msg)
    {
        debugText.text += msg.ToString() + "\n";
    }

    void Start()
    {
        if (debugText == null)
        {
            debugText = GameObject.Find("DebugAR").GetComponent<Text>();
        }
    }
}
