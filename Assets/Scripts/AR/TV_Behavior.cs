using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TV_Behavior : MonoBehaviour
{
    private UniversalMediaPlayer uniMed;

    public bool isPaused;
    private bool isOff;
    public float delayDestroy = 5.0f;

    public static GameObject Screen;
    public Image ReplayCanvas;

    public int activeVideo = 0;

    // Start is called before the first frame update
    void Start()
    {
        ReplayCanvas.enabled = false;
        Screen = GameObject.FindWithTag("Screen");

        isOff = false;
        isPaused = false;

        uniMed = FindObjectOfType<UniversalMediaPlayer>();

        //this array needs to be init in scene 
        uniMed.RenderingObjects[0] = Screen;

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
        uniMed.Pause();
        isPaused = true;
    }

    void Resume()
    {
        uniMed.Play();
        isPaused = false;
    }

    void Replay()
    {
        ReplayCanvas.enabled = false;
        uniMed.Position = 0;
    }

    void On()
    {
        uniMed.Play();
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
        uniMed.Release();
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



    public IEnumerator CheckVid()
    {
        using (UnityWebRequest w = UnityWebRequest.Get("http://sproutXR-api-dev.herokuapp.com/api/video")) //http://sproutXR-api-dev.herokuapp.com/api/video
        {
            yield return w.SendWebRequest();

            if (w.isNetworkError || w.isHttpError)
            {
                Debug.Log("Error: " + w.error);
            }
            else if (activeVideo == 1)
            {
                Debug.Log("Found Video: " + w.downloadHandler.text);

                TV_URL S=JsonUtility.FromJson<TV_URL>(w.downloadHandler.text);

                uniMed.Path = S.video;
                uniMed.Play();
                
                
                Debug.Log(S.video);
                yield return new WaitForSeconds(delayDestroy);
                Debug.Log("Video Time Length: " + uniMed.Length / 1000);
                yield return DestroyTV();

            }
            else if (activeVideo == 2)
            {
                Debug.Log("Found Video: " + w.downloadHandler.text);

                TV_URL S = JsonUtility.FromJson<TV_URL>(w.downloadHandler.text);

                uniMed.Path = S.video2;
                uniMed.Play();

                Debug.Log(S.video2);
                yield return new WaitForSeconds(delayDestroy);
                Debug.Log("Video Time Length: " + uniMed.Length / 1000);
                yield return DestroyTV();
            }
            else if (activeVideo == 3)
            {
                Debug.Log("Found Video: " + w.downloadHandler.text);

                TV_URL S = JsonUtility.FromJson<TV_URL>(w.downloadHandler.text);

                uniMed.Path = S.video3;
                uniMed.Play();
                Debug.Log(S.video3);
                yield return new WaitForSeconds(delayDestroy);
                Debug.Log("Video Time Length: " + uniMed.Length / 1000);
                yield return DestroyTV();
            }
            else
            {
                Debug.Log("I started way too soon");
            }
        }
    }

    private IEnumerator DestroyTV()
    {
        Debug.Log("Destroyed TV in " + ((uniMed.Length / 1000.0f)) + " seconds.");
        yield return new WaitForSeconds((uniMed.Length / 1000.0f));
        ReplayCanvas.enabled = true;
    }


    /*
    private IEnumerator DestroyTV()
    {
        Debug.Log("Destroyed TV in " + ((uniMed.Length / 1000.0f)) + " seconds.");
        yield return new WaitForSeconds((uniMed.Length / 1000.0f));
        transform.gameObject.SetActive(false);
    }
    */
}