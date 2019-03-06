using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenRoof : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        anim.StopPlayback();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.touchCount > 3) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit))
            {
                if(raycastHit.Equals(this))
                {
                    anim.SetTrigger("Open");
                }
            }
        }
}
}
