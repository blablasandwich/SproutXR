using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouse : MonoBehaviour
{
    public GameObject head;
    public float speed = 1.0f;
    public float maxRayDistance = 10.0f;
    public bool selected = false;

    void Start()
    {
        //Debug.Log(ActiveTargets.SelectedObject.name);
    }


    void FixedUpdate()
    {
        if(selected)
        {
            Debug.Log(gameObject.name + " is currently selected");
            Vector3 forward = transform.TransformDirection(Vector3.forward) * maxRayDistance;
            //temp look at variable
            Vector3 ground = new Vector3(transform.position.x, 0, transform.position.z);
        	Plane playerPlane = new Plane(Vector3.up, ground);

        	// Get the point along the ray that hits the calculated distance.
            /* Instead of looking at ret pos, look at the player
        	Vector3 targetBodyPoint = ActiveTargets.RetPos;
            Vector3 targetHeadPoint = ActiveTargets.RetPos;
            */
            Vector3 targetBodyPoint = ActiveTargets.Possess.transform.position;
            Vector3 targetHeadPoint = ActiveTargets.Possess.transform.position;
            //temporary fix to keep body from tilting
            targetBodyPoint.y = transform.position.y;

            // Determine the target rotation.  This is the rotation if the transform looks at the target point.
        	Quaternion targetBodyRotation = Quaternion.LookRotation(targetBodyPoint - transform.position);

            // Smoothly rotate towards the target point.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetBodyRotation, speed * Time.deltaTime);

            if(head)
            {
                //Do same as above
                Quaternion targetHeadRotation = Quaternion.LookRotation(targetHeadPoint - head.transform.position);
                head.transform.rotation = Quaternion.Slerp(head.transform.rotation, targetHeadRotation, speed * Time.deltaTime);
            }






            /*//May not be needed
            if (Physics.Raycast(head.transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
            {
                Debug.DrawRay(head.transform.position, head.transform.forward * hit.distance, Color.yellow);
                //Debug.Log("Did Hit");

            } else {
                Debug.DrawRay(head.transform.position, head.transform.TransformDirection(Vector3.forward) * 100, Color.white);
                //Debug.Log("Did not Hit");

                ActiveTargets.SelectedObject = null;
            }
            */
        }
    }

}
