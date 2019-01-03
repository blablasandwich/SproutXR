using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public void ClickOnLink(string link)
    {
        Application.OpenURL(link);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
