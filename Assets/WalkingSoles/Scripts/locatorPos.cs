using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class locatorPos : MonoBehaviour
{
    Transform player;

    private void Start()
    {
       player = GameObject.FindGameObjectWithTag("Player").transform.GetChild(2).transform;
    }
    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(player);
        this.transform.position = GvrPointerInputModule.CurrentRaycastResult.worldPosition;
    }
}
