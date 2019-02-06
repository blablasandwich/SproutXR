using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;
using UnityEngine.SceneManagement;
using System.Linq;


public class ServerDownload : MonoBehaviour
{
    //https://drive.google.com/uc?export/download&id=1Xda_w1mEJwmRkQs5d3F1cIFc54VdT_fv   med math levels
    //https://students.cah.ucf.edu/~fe171213/medmathscene                                Another med math level
    //https://s3.amazonaws.com/flamfoof/MedievalMath/AssetBundle/medmathscene           med math amazon
    //https://drive.google.com/uc?export/download&id=1828BYrrrOqNevnMa4N2eLC-zPLTw2DWd   anims
    //https://drive.google.com/uc?export/download&id=1OekL2DFVA6G2UF2nhOn8PNEe0n5IYfk3  sounds
    //https://drive.google.com/uc?export/download&id=1FVoQV7ffz0kxeFuw4xGbpkCElbGmeolK walking sole scene
    UnityWebRequest uwr;
    WWW www;
    AssetBundle bundle;
    
    public bool uCache = false;
    public Slider prog;
    public Text downloadText;
    [Tooltip("This will automatically be set from the GameEnumList at start of play")]
    public string level;
    [Tooltip("Read only purposes from where asset bundle location is downloaded from")]
    public string url;
    //TODO: android/iOS type for url
    public string deviceType;

    private GameEnumList game;
    public DownloadButtonBehavior dlButton;
    private MathController mController;

    private bool isDownloading = false;
    private bool checksizeOnce = false;
    private ServerDownload instance;
    private float bitsDL = 0; 
    bool printOnce = false;
    
    string fileSize;
    string fileName;
    string filePath;

    

    Dictionary<string, string> dict;

    private void Awake()
    {

        Caching.compressionEnabled = false;

        //Should usually be false, but if checked true, it will keep on clearing the downloaded assetbundle
        if(uCache)
        {
            Caching.ClearCache();
        }
    }

    void Start()
    {
        game = GetComponent<GameEnumList>();
        level = GetComponent<GameEnumList>().selectedLevel;
        mController = FindObjectOfType<MathController>();
        dlButton = FindObjectOfType<DownloadButtonBehavior>();
        if(dlButton == null)
        {
            //dlButton = Resources.FindObjectsOfTypeAll<DownloadButtonBehavior>()[0];
        }
        //TODO: Make an iOS and Android file path for url depending on device
        url = "https://s3.amazonaws.com/flamfoof/" + game.gameList + "/AssetBundle/" + level;
        
        filePath = GetStreamedAssetPath(level.ToString());
        //Debug.Log(filePath);
        //StartCoroutine(PlayButton());
        //StartCoroutine(GetAndroidBundle(url));
        //StartCoroutine(GetAssetBundleFileSize(url));
        //StartCoroutine(GetAssetBundleOnline(url));
        //DownloadAndroidAssetBundle();
    }

    //TODO: Make this function more modular for other situations like a different game URL
    //Currently being called from download button behavior script
    public void DownloadAndroidAssetBundle()
    {
        url = "https://s3.amazonaws.com/flamfoof/" + game.gameList + "/AssetBundle/" + level;
        //Checks if a version already exists
        if (!IsAssetBundleCached())
        {
            if (!isDownloading)
                StartCoroutine(PlayButton());
        } else
        {
            Debug.Log("Asset bundle already exists");
            if (downloadText)
                downloadText.text = "Launch";
            //Finds that asset bundle, same as above because it assigns it the asset bundle
            StartCoroutine(PlayButton());

            //Different ways to start each app. Should end up looking the same as kellsLevel
            StartCoroutine(PlayGame());
        }
        
    }
    //TODO: Check if asset bundle is already downloaded, then change text accordingly
    public IEnumerator PlayButton()
    {
        isDownloading = true;
        //Checks if there is an available bundle
        yield return GetAndroidBundle();
        if (!bundle)
        {
            Debug.Log("Bundle failed to load");
            if (game.ToString() == "none")
                Debug.LogError("No game is selected for download");
            if (level == "none")
                Debug.LogError("No level is selected");

            isDownloading = false;
            yield break;
        }

        isDownloading = false;

        Debug.Log("Starting this");
    }

    public IEnumerator GetAndroidBundle()
    {
        //TODO: Have a version checking function to compare different asset bundle files
        WWW request = WWW.LoadFromCacheOrDownload(url, 0);
        
            
        while (!request.isDone)
        {
            if(!prog.isActiveAndEnabled)
                prog.gameObject.SetActive(true);
            if (request.progress > 0)
                prog.value = request.progress + 0.1f;
            if (downloadText)
                downloadText.text = "Downloading....";

            yield return null;
        }
        if (request.error == null)
        {
            if (!bundle)
            {
                bundle = request.assetBundle;
            }

            prog.gameObject.SetActive(false);
            if (downloadText)
                downloadText.text = "Launch";
            Debug.Log("Success");
        }
        else
        {
            Debug.Log(request.error);
        }

    }

    public IEnumerator PlayGame()
    {
        float time = 0;
        //to prevent switching between games when loading
        GameEnumList.GameList currentGame = game.gameList;
        while(!bundle)
        {
            if (time > 5.0f)
                break;
            yield return null;
        }

        string[] sceneArray;
        Debug.Log("Yes this does exist");
        switch (currentGame)
        {
            case GameEnumList.GameList.MedievalMath:
                sceneArray = bundle.GetAllScenePaths();
                for (int i = 0; i < bundle.GetAllScenePaths().Length; i++)
                {
                    Debug.Log(bundle.GetAllScenePaths()[i]);
                }
                Debug.Log("Level name loaded is: " + sceneArray[0]);
                mController.StartGame();
                break;
            /* No asset bundle made for miss ways yet
            case GameEnumList.GameList.MissWays:
                sceneArray = bundle.GetAllScenePaths();
                for (int i = 0; i < bundle.GetAllScenePaths().Length; i++)
                {
                    Debug.Log(bundle.GetAllScenePaths()[i]);
                }
                Debug.Log("Level name loaded is: " + sceneArray[0]);
                break;
                */
            default:
                SceneManager.LoadScene(game.selectedLevel);
                break;
        }
        

        /*For texting without the dlButton
        sceneArray = bundle.GetAllScenePaths();
        for (int i = 0; i < bundle.GetAllScenePaths().Length; i++)
        {
            Debug.Log(bundle.GetAllScenePaths()[i]);
        }
        Debug.Log("Level name loaded is: " + sceneArray[0]);

        SceneManager.LoadScene(sceneArray[0]);
        */
    }

    public bool IsAssetBundleCached()
    {
        //update url
        url = "https://s3.amazonaws.com/flamfoof/" + game.gameList + "/AssetBundle/" + level;
        if (Caching.IsVersionCached(url, 0))
        {
            if (downloadText)
                downloadText.text = "Launch";
            return true;
        }
        if (downloadText)
            downloadText.text = "Download Now!";
        return false;
    }

    private string GetStreamedAssetPath(string name)
    {
        string path;

        #if UNITY_EDITOR
            path = "file:" + Application.dataPath + "/StreamingAssets/";
        #elif UNITY_ANDROID
            path = "jar:file://"+ Application.dataPath + "!/assets/";
        #elif UNITY_IOS
            path = "file:" + Application.dataPath + "/Raw/";
        #else
            //Desktop (Mac OS or Windows)
            path = "file:"+ Application.dataPath + "/StreamingAssets/";
        #endif
        path = path + name;
        return path;
    }


}









//Unused code that may return


/*
/// <summary>
/// Currently not implemented. It's for loading scenes of other games from external sources (like separately downloaded apks)
/// not needed currently.
/// </summary>
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
}*/

/*
void Update()
{


if (fileSize == "" || fileSize == null)
{
fileSize = "1";
}

if (uwr != null)
{

if (uwr.downloadedBytes == 0)
{
Debug.Log("Currently connecting to server....");
}
else if (!uwr.isDone)
{
float realProgress = ((float)uwr.downloadedBytes / 1048576) / (float.Parse(fileSize) / 1048576);
float temp = (float)uwr.downloadedBytes / 1048576;
//Debug.Log(temp);
//Debug.Log(uwr.downloadProgress);

Debug.Log("Downloaded: " + ((float)uwr.downloadedBytes / 1048576).ToString("F2") + "MB of " + (float.Parse(fileSize) / 1048576).ToString("F2") + "MB.");
prog.value = realProgress;
}
else if (!printOnce)
{
printOnce = true;
Debug.Log(bundle.isStreamedSceneAssetBundle);
Debug.Log(filePath);


printOnce = true;
Debug.Log(uwr.downloadProgress);
sceneArray = bundle.GetAllScenePaths();
for (int i = 0; i < bundle.GetAllScenePaths().Length; i++)
{
    Debug.Log(bundle.GetAllScenePaths()[i]);
}
Debug.Log("Level name loaded is: " + sceneArray[0]);
//SceneManager.LoadSceneAsync(sceneArray[0]);
//SceneManager.LoadScene(sceneArray[0]);

}
}
else
{
//Debug.Log("Currently connecting to server....");
}
//Debug.Log("Size: " + fileSize);

}*/

/*
// Will get this function working when actually pulling from the server itself.
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

    Debug.Log("Size is : " + fileSize + "(" + System.Math.Round((float.Parse(fileSize) / 1048576), 2) + "MB)");

    //Printing out available header responses

    foreach (KeyValuePair<string, string> kvp in dict)
    {
        Debug.Log("Key = " + kvp.Key + ", Value = " + kvp.Value);
    }



    if (wrHead.disposeDownloadHandlerOnDispose)
    {
        Debug.Log("disposed");
    }
    wrHead.Dispose();
    StartCoroutine(GetAssetBundleOnline(url));
}

}

IEnumerator GetAssetBundleOnline(string url)
{
Debug.Log("Checking if file path exists");
AssetBundle validAB;

if (System.IO.File.Exists(filePath))
    validAB = AssetBundle.LoadFromFile(GetStreamedAssetPath(level.ToString()));
else
{
    validAB = null;
}

if(!validAB)
{
    Debug.Log("Started bundle dl");
    uwr = UnityWebRequestAssetBundle.GetAssetBundle(url);
    DownloadHandler dh = UnityWebRequestAssetBundle.GetAssetBundle(url).downloadHandler;
    //uwr.downloadHandler = new DownloadHandlerAssetBundle(url, 0);

    while (!Caching.ready)
    {
        yield return null;
    }


    yield return uwr.SendWebRequest();

    if (uwr.isNetworkError || uwr.isHttpError)
    {
        Debug.Log(uwr.error);
    }
    else
    {
        // Get downloaded asset bundle
        Debug.Log("FInishd bundle dl");
        bundle = DownloadHandlerAssetBundle.GetContent(uwr);
        save(dh.data, filePath);
        //yield return new WaitForSeconds(10.0f);
        //Application.Quit();

    }
} else
{
    Debug.Log("Asset Does Exist on path: " + filePath + "\nLoading Asset...");
    bundle = validAB;
}
}

void save(byte[] data, string path)
{
//Create the Directory if it does not exist
if (!Directory.Exists(Path.GetDirectoryName(path)))
{
    Directory.CreateDirectory(Path.GetDirectoryName(path));
}

try
{
    File.WriteAllBytes(path, data);
    Debug.Log("Saved Data to: " + path.Replace("/", "\\"));
}
catch (System.Exception e)
{
    Debug.LogWarning("Failed To Save Data to: " + path.Replace("/", "\\"));
    Debug.LogWarning("Error: " + e.Message);
}
}
*/
