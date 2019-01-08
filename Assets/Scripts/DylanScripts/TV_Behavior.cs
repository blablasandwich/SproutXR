using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TV_Behavior : MonoBehaviour
{
    private UniversalMediaPlayer uniMed;
    private bool isPaused;
    private bool playImmediately;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        uniMed = FindObjectOfType<UniversalMediaPlayer>();

        StartCoroutine(CheckVid());
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown("space"))
       {
            OnOff();
       }
       if (Input.GetKeyDown(KeyCode.P))
       {
            Pause();
       }

       if (Input.GetKeyDown(KeyCode.O))
       {
            Resume();
       }

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

    void OnOff()
    {
        if (uniMed.IsPlaying || isPaused)
        {
            uniMed.Stop();
            isPaused = false;
        }
        else
        {
            uniMed.Play();
        }
    }

    [System.Serializable]
    public class TV_URL
    {
        public string video;
    }



    private IEnumerator CheckVid()
    {
        using (UnityWebRequest w = UnityWebRequest.Get("http://sproutXR-api-dev.herokuapp.com/api/video"))
        {
            yield return w.SendWebRequest();

            if (w.isNetworkError || w.isHttpError)
            {
                Debug.Log("Error: " + w.error);
            }
            else
            {
                Debug.Log("Found Video: " + w.downloadHandler.text);

                TV_URL S=JsonUtility.FromJson<TV_URL>(w.downloadHandler.text);

                uniMed.Path = S.video;
                uniMed.Play();

                Debug.Log(S.video);
            }
        }
    }
}