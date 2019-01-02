using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCanvas : MonoBehaviour
{
    void Start() {
        SetPlayerUIParent();
    }

    public void SetPlayerUIParent()
    {
        this.transform.SetParent(GameObject.FindGameObjectWithTag("Player").transform.GetChild(2).transform);
    }
}