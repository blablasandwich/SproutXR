using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class ServerDownload : MonoBehaviour
{
    // TODO: Import urls from API
    //https://drive.google.com/uc?export/download&id=1Xda_w1mEJwmRkQs5d3F1cIFc54VdT_fv   med math levels
    //https://drive.google.com/uc?export/download&id=1828BYrrrOqNevnMa4N2eLC-zPLTw2DWd   anims
    //https://drive.google.com/uc?export/download&id=1OekL2DFVA6G2UF2nhOn8PNEe0n5IYfk3  sounds
    //https://drive.google.com/uc?export/download&id=1FVoQV7ffz0kxeFuw4xGbpkCElbGmeolK walking sole scene
    UnityWebRequest www;
    AssetBundle bundle;
    bool printOnce = false;
    string[] sceneArray;
    IEnumerator Start()
    {
        www = UnityWebRequestAssetBundle.GetAssetBundle("https://drive.google.com/uc?export/download&id=1Xda_w1mEJwmRkQs5d3F1cIFc54VdT_fv");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Get downloaded asset bundle
            bundle = DownloadHandlerAssetBundle.GetContent(www);
            //yield return new WaitForSeconds(10.0f);
            //Application.Quit();
        }
    }

    void Update()
    {
        if (!www.isDone)
        {
            Debug.Log(www.downloadProgress);
        }
        else if (!printOnce)
        {
            printOnce = true;
            Debug.Log(www.downloadProgress);
            sceneArray = bundle.GetAllScenePaths();
            for (int i = 0; i < bundle.GetAllScenePaths().Length; i++)
            {
                Debug.Log(bundle.GetAllScenePaths()[i]);
            }
            SceneManager.LoadSceneAsync(System.IO.Path.GetFileNameWithoutExtension(sceneArray[0]));

        }

    }

    void LoadGame()
    {
        bool fail = false;
        string bundleId = "com.google.appname"; // your target bundle id
        AndroidJavaClass up = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject ca = up.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject packageManager = ca.Call<AndroidJavaObject>("getPackageManager");

        AndroidJavaObject launchIntent = null;
        try
        {
            launchIntent = packageManager.Call<AndroidJavaObject>("getLaunchIntentForPackage", bundleId);
        }
        catch (System.Exception e)
        {
            fail = true;
        }

        if (fail)
        { //open app in store
            Application.OpenURL("https://google.com");
        }
        else //open the app
            ca.Call("startActivity", launchIntent);

        up.Dispose();
        ca.Dispose();
        packageManager.Dispose();
        launchIntent.Dispose();
    }
}
