using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TV_Behavior : MonoBehaviour
{
    public UniversalMediaPlayer mediaPlayer;

    public bool isPaused;
    private bool isOff;
    public float delayDestroy = 5.0f;

    public GameObject Screen;
    public Image ReplayCanvas;

    public int activeVideo = 0;


    public Text debugText;

    //List<TV_URL> JsonArry;
    TV_URL urls;

    public void DebugAR<T> (T msg)
    {
        // Function displays text on AR screen for debug on the phone
        debugText.text += msg.ToString() + "\n";
    }

    // Start is called before the first frame update
    void Start()
    {
        ReplayCanvas.enabled = false;
        //Screen = GameObject.FindWithTag("Screen");

        isOff = false;
        isPaused = false;

        //mediaPlayer = FindObjectOfType<UniversalMediaPlayer>();

        //this array needs to be init in scene 
        //mediaPlayer.RenderingObjects[0] = Screen;

        //StartCoroutine(CheckVid());
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Space))
       {
            if (!isOff)
            {
                Off();
            }
            else
            {
                On();
            }
       }

       if (Input.GetKeyDown(KeyCode.P))
       {
            Pause();
       }

       if (Input.GetKeyDown(KeyCode.O))
       {
            Resume();
       }

       if (Input.GetKeyDown(KeyCode.I))
        {
            Replay();
        }

       if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit))
            {
                if (raycastHit.collider.gameObject.layer == 23)
                {
                    if (ReplayCanvas.enabled == true)
                        Replay();
                    else if (isPaused)
                        Resume();
                    else
                        Pause();
                }
                if (raycastHit.collider.gameObject.layer == 24)
                {
                    if (!isOff)
                    {
                        Off();
                    }
                    else
                    {
                        On();
                    }
                }
                if (raycastHit.collider.gameObject.layer == 25)
                {
                    Replay();
                }
            }
        }
    }

    void Pause()
    {
        mediaPlayer.Pause();
        isPaused = true;
    }

    void Resume()
    {
        mediaPlayer.Play();
        isPaused = false;
    }

    void Replay()
    {
        ReplayCanvas.enabled = false;
        mediaPlayer.Position = 0;
    }

    void On()
    {
        mediaPlayer.Play();
        isOff = false;
    }

    public void RunCheckVid()
    {
        StartCoroutine(CheckVid());
    }

    void Off()
    {
        transform.parent.gameObject.SetActive(false);
        Debug.Log("off now");
        mediaPlayer.Release();
        isPaused = false;
        isOff = true;
    }

    [System.Serializable]
    public class TV_URL
    {
        public string video;
        public string video2;
        public string video3;

    }

    //   [System.Serializable]
    //   private class MyWrapper
    //   {
    //       public List<TV_URL> video;
    //   }

    void ParseJsonToObject(string json)
    {
        var wrappedjsonArray = JsonUtility.FromJson<TV_URL>(json);
        urls = wrappedjsonArray;
    }

    void CompareVid(UnityWebRequest w)
    {
        ParseJsonToObject(w.downloadHandler.text);

        switch (activeVideo)
        {
            case 1:
            {
                Debug.Log("<color=green>URL: </color>" + urls.video);
                mediaPlayer.Path = urls.video;
                mediaPlayer.Play();
                break;
            }
            case 2:
            {
                Debug.Log("<color=green>URL: </color>" + urls.video2);
                mediaPlayer.Path = urls.video2;
                mediaPlayer.Play();
                break;
            }
            case 3:
            {
                Debug.Log("<color=green>URL: </color>" + urls.video3);
                mediaPlayer.Path = urls.video3;
                mediaPlayer.Play();
                break;
            }
        }
    }

    public IEnumerator CheckVid()
    {
        using (UnityWebRequest w = UnityWebRequest.Get("http://sproutXR-api-dev.herokuapp.com/api/video")) //http://sproutXR-api-dev.herokuapp.com/api/video
        {
            yield return w.SendWebRequest();

            if (w.isNetworkError || w.isHttpError)
            {
                Debug.Log("Error: " + w.error);
                DebugAR("Error: " + w.error);
                DebugAR("Error: " + w.isNetworkError);
                DebugAR("Error: " + w.isHttpError);
            }
            else
            {
                Debug.Log("<color=green>URL: </color>" + w.downloadHandler.text);
                CompareVid(w);
            }
        }
    }

    private IEnumerator DestroyTV()
    {
        Debug.Log("Destroyed TV in " + ((mediaPlayer.Length / 1000.0f)) + " seconds.");
        yield return new WaitForSeconds((mediaPlayer.Length / 1000.0f));
        ReplayCanvas.enabled = true;
    }
}