using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UMP;

public class TV_Behavior : MonoBehaviour
{
    public UniversalMediaPlayer mediaPlayer;

    public bool isPaused;
    private bool isOff;
    public float delayDestroy = 5.0f;

    public static GameObject screen;
    private Image ReplayCanvas;

    public bool videoReady = false;

    public int activeVideo = 0;
    public string apiUrl = "http://sproutXR-api-dev.herokuapp.com/api/video";
    public TV_JSON tvJson;

    public Image ReplayCanvas1 { get => ReplayCanvas1; set => ReplayCanvas1 = value; }


    // Start is called before the first frame update
    void Start()
    {
        screen = GameObject.FindWithTag("Screen");
        mediaPlayer = FindObjectOfType<UniversalMediaPlayer>();

        //this array needs to be init in scene 
        mediaPlayer.RenderingObjects[0] = screen;

        ReplayCanvas.enabled = false;

        On();
    }

    void Update()
    {
        TestWithKeyboad();
    }


    void Pause()
    {
        Debug.Log("TV: Pause");
        mediaPlayer.Pause();
        isPaused = true;
    }

    void Play()
    {
        Debug.Log("TV: Play");
        mediaPlayer.Play();
        isPaused = false;
    }

    void Resume()
    {
        Debug.Log("TV: Resume");
        Play();
    }

    void Replay()
    {
        Debug.Log("TV: Replay");
        ReplayCanvas.enabled = false;
        mediaPlayer.Position = 0;
        On();
    }

    void On()
    {
        Debug.Log("TV: On");
        isOff = false;
        Play();
    }

    void Off()
    {
        Debug.Log("TV: Off");
        transform.parent.gameObject.SetActive(false);
        mediaPlayer.Release();
        isPaused = false;
        isOff = true;
    }

    [System.Serializable]
    public class TV_JSON
    {
        public string video;
        public string video2;
        public string video3;
    }

    public void RunRequestVideos()
    {
        if (!videoReady)
        {
            StartCoroutine(RequestVideos());
        }
    }

    public IEnumerator RequestVideos()
    {
        using (UnityWebRequest w = UnityWebRequest.Get(apiUrl))
        {
            yield return w.SendWebRequest();

            if (w.isNetworkError || w.isHttpError)
            {
                Debug.Log("Error: " + w.error);
                ARPrint.log.text = "Error: " + w.error;
                ARPrint.log.text = "Error: " + w.isNetworkError;
                ARPrint.log.text = "Error: " + w.isHttpError;
            }
            else
            {
                tvJson = JsonUtility.FromJson<TV_JSON>(w.downloadHandler.text);
                videoReady = true;

                // TODO: Move to separate function that sets the videos
                switch (activeVideo)
                {
                    case 1:
                        mediaPlayer.Path = tvJson.video;
                        break;
                    case 2:
                        mediaPlayer.Path = tvJson.video2;
                        break;
                    case 3:
                        mediaPlayer.Path = tvJson.video3;
                        break;
                    default:
                        Debug.Log("RequestVideos() Started Too Early");
                        break;
                }

                Play();
                Debug.Log("Playing this video: " + mediaPlayer.Path);
                Debug.Log("Video Time Length: " + mediaPlayer.Length / 1000);
                yield return new WaitForSeconds(delayDestroy);
                yield return DestroyTV();
            }
        }
    }

    private IEnumerator DestroyTV()
    {
        Debug.Log("Destroyed TV in " + ((mediaPlayer.Length / 1000.0f)) + " seconds.");
        yield return new WaitForSeconds((mediaPlayer.Length / 1000.0f));
        ReplayCanvas.enabled = true;
    }

    void TestWithKeyboad()
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

}
