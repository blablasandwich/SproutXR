using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActiveTargets : MonoBehaviour
{
    private static GameObject selectedObject;
    private static GameObject previousObject;
    private static GameObject currentPossessed;
    private static Vector3 vrRetPos;
    public GameObject mainCamRoot;


    void Awake()
    {
        GameObject initPlayer = GameObject.Find("Player");
        selectedObject = initPlayer;
        previousObject = selectedObject;
        //Sets VR camera to one of the player prefabs
        //SetPossession(initialPlayer);

        if(selectedObject)
        {
            Debug.Log("Initializing game manager default target to: " + selectedObject.name);
            //temporarily for now
            selectedObject.GetComponent<CameraMouse>().selected = false;
            Possess = selectedObject;
        } else {
            Debug.LogError("Didn't find any player object, currently nothing is selected on game manager. Maybe place a Player onto the scene.");
        }
    }

    public static GameObject SelectedObject
    {
        get
        {
            return selectedObject;
        }

        set
        {
            if(value != null)
            {
                //Sets interacted item according to tag as a selected item
                if(previousObject != value && value.tag == "Player")
                {
                    previousObject = selectedObject;
                    previousObject.GetComponent<CameraMouse>().selected = false;
                    selectedObject = value.transform.root.gameObject;
                    selectedObject.GetComponent<CameraMouse>().selected = true;
                } else if(previousObject != value && value.tag == "PossiblePlayer")
                {
                    previousObject = selectedObject;
                    previousObject.GetComponent<CameraMouse>().selected = false;
                    selectedObject = value.transform.root.gameObject;
                    selectedObject.GetComponent<CameraMouse>().selected = true;
                } else {
                    selectedObject = value.transform.root.gameObject;
                }
            } else {
                selectedObject = null;
                Debug.Log("An object couldn't be set as an selected selected target. No worries");
            }
        }
    }

    public static GameObject PrevSelectedObject
    {
        get
        {
            return previousObject;
        }

        set
        {
            if(value != null)
            {
                Debug.Log("Previous Object shouldn't/can't be set");
            }
        }
    }

    public static Vector3 RetPos
    {
        get
        {
            return vrRetPos;
        }

        set
        {
            vrRetPos = value;
        }
    }

    public static GameObject Possess
    {
        get
        {
            return currentPossessed;
        }
        set
        {
            currentPossessed = value;
        }
    }



}
