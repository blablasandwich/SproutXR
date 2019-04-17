using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickVocab : MonoBehaviour
{
    private string NAME;
    private Material mat;
    private bool b;
    private Color on = new Color(0.08f, 0.71f, 0, 1);
    private Color off = new Color(0.08f, 0.3f, 0, 1);
    // Start is called before the first frame update
    void Start()
    {
        NAME = gameObject.name;
        mat = gameObject.transform.GetChild(1).gameObject.GetComponent<Renderer>().material;
        b = false;
        mat.SetColor("_EmissionColor", off);
    }

    public void toggleHighLight()
    {
        b = !b;
        if (b)
        {
            turnOff();
        }
        else
        {
            turnOn();
        }
    }

    void turnOn()
    {
        mat.SetColor("_EmissionColor", on);
        lightbulb.toggleVocabLight();
    }

    void turnOff()
    {
        mat.SetColor("_EmissionColor", off);
    }



    // Update is called once per frame
    void Update()
    {
        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit))
            {
                if (raycastHit.collider.gameObject.name == NAME)
                {
                    toggleHighLight();
                }

            }
        }
    }
}
