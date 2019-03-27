using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DownloadButtonBehavior : MonoBehaviour
{
    public string currentApp = "";
    public MathController mController;
    public GameObject dlManager;
    public GameObject loadingScreen;

    public void Start()
    {
        dlManager = GameObject.Find("DownloadManager");
    }
    public void SelectCurrentApp(string selectedApp)
    {
        currentApp = selectedApp;
        dlManager.GetComponent<DownloadManagerController>().SetGame(selectedApp);
        dlManager.GetComponent<ServerDownload>().IsAssetBundleCached();
        
    }

    public void launchApp()
    {
        if (currentApp != "")
        {
            if (dlManager.GetComponent<GameEnumList>().gameList != GameEnumList.GameList.none)
            {
                loadingScreen.SetActive(true);
                dlManager.GetComponent<ServerDownload>().DownloadAndroidAssetBundle();
            } else
            {
                loadingScreen.SetActive(true);
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
