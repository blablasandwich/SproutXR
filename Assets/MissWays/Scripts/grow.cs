using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grow : MonoBehaviour
{
    Vector3 a;
    public float rate = 1f;
    // Start is called before the first frame update
    void Start()
    {
       a = transform.localScale * 1.2f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, a, Time.deltaTime * rate);
    }
}
