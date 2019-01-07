using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class ServerDownload : MonoBehaviour
{
    //https://drive.google.com/uc?export/download&id=1Xda_w1mEJwmRkQs5d3F1cIFc54VdT_fv   med math levels
    //https://students.cah.ucf.edu/~fe171213/medmathscene                                Another med math level
    //https://drive.google.com/uc?export/download&id=1828BYrrrOqNevnMa4N2eLC-zPLTw2DWd   anims
    //https://drive.google.com/uc?export/download&id=1OekL2DFVA6G2UF2nhOn8PNEe0n5IYfk3  sounds
    //https://drive.google.com/uc?export/download&id=1FVoQV7ffz0kxeFuw4xGbpkCElbGmeolK walking sole scene
    UnityWebRequest www;
    AssetBundle bundle;
    string url;
    public Slider prog;
    private bool checksizeOnce = false;
    private float bitsDL = 0; 
    bool printOnce = false;
    string[] sceneArray;
    string fileSize;
    Dictionary<string, string> dict;
    void Start()
    {
        url = "https://students.cah.ucf.edu/~fe171213/medmathscene";

        StartCoroutine(GetAssetBundleFileSize(url));
        //StartCoroutine(GetAssetBundleOnline(url));
    }
   
    IEnumerator GetAssetBundleFileSize(string url)
    {
        //Below is not working at all.
        Debug.Log("Started bundle size checking");
        UnityWebRequest wrHead = UnityWebRequest.Head(url);
        yield return wrHead.SendWebRequest();
        //Google drive doesn't use content-length
        fileSize = wrHead.GetResponseHeader("Content-Length");
        
        //use this to check the available header responses
        //dict = wrHead.GetResponseHeaders();
        
        if (wrHead.isNetworkError || wrHead.isHttpError)
        {
            Debug.Log("Error While Getting Length: " + wrHead.error);
        } else
        {
            Debug.Log("Finished file size check no error");

            Debug.Log("Size is : " + fileSize);

            /* Printing out available header responses
            foreach (KeyValuePair<string, string> kvp in dict)
            {
                Debug.Log("Key = " + kvp.Key + ", Value = " + kvp.Value);
            }
            */
            StartCoroutine(GetAssetBundleOnline(url));

            if (wrHead.disposeDownloadHandlerOnDispose)
            {
                Debug.Log("disposed");
            }
        }
    
    }

    IEnumerator GetAssetBundleOnline(string url)
    {
        Debug.Log("Started bundle dl");
        www = UnityWebRequestAssetBundle.GetAssetBundle(url);
        //www.downloadHandler = new DownloadHandlerAssetBundle(url, 0);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Get downloaded asset bundle
            Debug.Log("FInishd bundle dl");
            bundle = DownloadHandlerAssetBundle.GetContent(www);
            //yield return new WaitForSeconds(10.0f);
            //Application.Quit();
        }

    }

    void Update()
    {


        if (www != null)
        {
            if (www.downloadedBytes == 0)
            {
                Debug.Log("Currently connecting to server....");
            } else if (!www.isDone)
            {
                float realProgress = (float)www.downloadedBytes / float.Parse(fileSize);
                float temp = (float)www.downloadedBytes / 1048576;
                //Debug.Log(temp);
                //Debug.Log(www.downloadProgress);

                Debug.Log("Downloaded: " + ((float)www.downloadedBytes / 1048576).ToString("F2") + "MB of " + (float.Parse(fileSize) / 1048576).ToString("F2") + "MB.");
                prog.value = realProgress;
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
        } else
        { 
            Debug.Log("Currently connecting to server....");
        }
        //Debug.Log("Size: " + fileSize);
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
                launchIntent = packageManager.Call<AndroidJavaObject>("getLaunchIntentForPackage",bundleId);
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
                ca.Call("startActivity",launchIntent);

            up.Dispose();
            ca.Dispose();
            packageManager.Dispose();
            launchIntent.Dispose();
    }
}
