using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TV_Behavior : MonoBehaviour
{
    private UniversalMediaPlayer uniMed;
    private bool isPaused;
    private bool playImmediately;

    private GameObject Screen;

    public int activeVideo = 0;

    // Start is called before the first frame update
    void Start()
    {
        Screen = GameObject.FindGameObjectWithTag("Screen");
        uniMed.RenderingObjects[1] = Screen;

        isPaused = false;
        uniMed = FindObjectOfType<UniversalMediaPlayer>();

        StartCoroutine(CheckVid());
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Space))
       {
            OnOff();
            Debug.Log("A");
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

       /*
       if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit))
            {
                if (raycastHit.collider.gameObject.layer == 23)
                {
                    if (isPaused)
                        Resume();
                    else
                        Pause();
                }
                if (raycastHit.collider.gameObject.layer == 24)
                {
                    OnOff();
                }
            }
        }
        */
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
        uniMed.Position = 0;
    }

    void OnOff()
    {
        if (uniMed.IsPlaying || isPaused)
            uniMed.Stop();
        else
            uniMed.Play();

    }

    [System.Serializable]
    public class TV_URL
    {
        public string video;
        public string video2;
        public string video3;
    }



    private IEnumerator CheckVid()
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
            }
            else if (activeVideo == 2)
            {
                Debug.Log("Found Video: " + w.downloadHandler.text);

                TV_URL S = JsonUtility.FromJson<TV_URL>(w.downloadHandler.text);

                uniMed.Path = S.video2;
                uniMed.Play();

                Debug.Log(S.video2);
            }
            else if (activeVideo == 3)
            {
                Debug.Log("Found Video: " + w.downloadHandler.text);

                TV_URL S = JsonUtility.FromJson<TV_URL>(w.downloadHandler.text);

                uniMed.Path = S.video3;
                uniMed.Play();

                Debug.Log(S.video3);
            }
        }
    }
}