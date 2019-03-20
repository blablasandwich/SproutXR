using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VocabBehavior : MonoBehaviour
{
    void OnCollision()
    {
        GetComponent<MeshRenderer>().material.color = new Color(1, 0, 0, 1);
        print("tappy tap tap");
    }
}
