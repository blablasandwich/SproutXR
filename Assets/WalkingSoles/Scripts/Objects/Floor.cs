using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {
    private Transform body;
    private GameObject player;
    private GvrReticlePointer distance;
    bool walking = false;
    public int MoveRate = 4;
    void Start() {
       player = ActiveTargets.Possess;
       body = ActiveTargets.Possess.transform.GetChild(2).transform;
       distance = Camera.main.transform.GetChild(0).GetComponent<GvrReticlePointer>();
    }

    private void Update()
    {
        if (walking)
        {
            player.transform.Translate(Vector3.forward * (Time.deltaTime * MoveRate), body);
        }
    }
    public void teleport() {
        player.transform.Translate(new Vector3(0, 0, distance.ReticleDistanceInMeters), body);
    }

    public void walk()
    {
        walking = !walking;
    }

    public void exit() {
        Application.Quit();
        }


}
