using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DownloadButtonBehavior : MonoBehaviour
{
    public string currentApp = "";
    public MathController mController;
    public GameObject dlManager;

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
    // Start is called before the first frame update
    public void launchApp()
    {
        if (currentApp != "")
        {
            if(dlManager.GetComponent<GameEnumList>().gameList == GameEnumList.GameList.MedievalMath) {
                dlManager.GetComponent<ServerDownload>().DownloadAndroidAssetBundle();
            } else if(dlManager.GetComponent<GameEnumList>().gameList == GameEnumList.GameList.MissWays)
            {
                SceneManager.LoadScene(dlManager.GetComponent<GameEnumList>().misswaysLevels.ToString());
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
