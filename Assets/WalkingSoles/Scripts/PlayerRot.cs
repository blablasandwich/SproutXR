using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRot : MonoBehaviour {

    Transform p;

    void Start() {
        p = ActiveTargets.Possess.transform.GetChild(2).transform;
    }

    void Update() {
        Vector3 lookDir = GvrPointerInputModule.CurrentRaycastResult.worldPosition - p.position;
        lookDir.y = 0; // keep only the horizontal direction
        p.rotation = Quaternion.LookRotation(lookDir);

    }
}
