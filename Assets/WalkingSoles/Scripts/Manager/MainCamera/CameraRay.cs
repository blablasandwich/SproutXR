using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraRay : MonoBehaviour
{

    private RaycastHit hit;
    private Ray ray;
    public float maxRetDistance = 100.0f;
    private int layerMask;
    private RaycastResult gvrHit;
    public GameObject mainCamRoot;


    void Start()
    {
        //Sets VR camera to one of the player prefabs
        SetPossession(ActiveTargets.Possess);
        
        //Aim at this specific target
        layerMask = 1 <<  LayerMask.NameToLayer("Interactable");

        //~ inverts bitmask to selectively aim at everything else besides player. Use when necessary
        //layerMask = ~layerMask;
    }

    // Update is called once per frame
    void Update()
    {
        //gvrHit is the google VR raycast
        gvrHit = GvrPointerInputModule.CurrentRaycastResult;

        Debug.Log("Previous: " + ActiveTargets.PrevSelectedObject);
        Debug.Log("Selected: " + ActiveTargets.SelectedObject);

        if(gvrHit.gameObject != null)
        {
            //Insert interactable object tags here
            if(ActiveTargets.SelectedObject != gvrHit.gameObject && gvrHit.gameObject.CompareTag("Player"))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * gvrHit.distance , Color.yellow);
                ActiveTargets.SelectedObject = gvrHit.gameObject;
            } else if (ActiveTargets.SelectedObject != gvrHit.gameObject && gvrHit.gameObject.CompareTag("PossiblePlayer"))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * gvrHit.distance , Color.yellow);
                ActiveTargets.SelectedObject = gvrHit.gameObject;
            } else {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * maxRetDistance , Color.blue);

                //Don't set to null, it causes problems
                //ActiveTargets.SelectedObject = null;
            }

            //Sets the hit position of the reticle in static var in GameManager
            ActiveTargets.RetPos = gvrHit.worldPosition;
        }


    }

    //Use this to possesss others
    public void SetPossession(GameObject other)
    {
        mainCamRoot.transform.SetParent(other.transform);
    }

}
