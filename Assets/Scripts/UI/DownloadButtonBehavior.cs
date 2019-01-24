using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DownloadButtonBehavior : MonoBehaviour
{
    public string currentApp = "";
    public MathController mController;
    public ServerDownload serverDL;

    public void Start()
    {
        serverDL = FindObjectOfType<ServerDownload>();
    }
    public void SelectCurrentApp(string selectedApp)
    {
        currentApp = selectedApp;
    }
    // Start is called before the first frame update
    public void launchApp()
    {
        if (currentApp != "")
        {
            if(currentApp == "kellsLevel") {
                //Will do a check if it's already downloadd or not
                serverDL.DownloadAndroidAssetBundle();
            } else
            {
                SceneManager.LoadScene(currentApp);
            }
            
        }
        else print("No Scene Selected");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
