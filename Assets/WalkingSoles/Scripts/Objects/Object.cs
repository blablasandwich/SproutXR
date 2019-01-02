using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Object : MonoBehaviour
{
    bool testbool = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void test()
    {
        Debug.Log(GvrPointerInputModule.CurrentRaycastResult.worldPosition);
        Debug.Log("AFSDJGFJDHNGSJENHNJERDHSRH");
        if (testbool)
        {
            GetComponent<Renderer>().sharedMaterial.color = new Color(1f, 0f, 0f, 1f);
        }
        else
        {
            GetComponent<Renderer>().sharedMaterial.color = new Color(1f, 1f, 1f, 1f);
        }

        testbool = !testbool;
    }

}
