using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightbulb : MonoBehaviour
{
    private Material[] mats;
    private static Material bulb;
    private static GameObject lightSource;
    private static Color on = new Color(.75f, .75f, .75f, 1f);
    private static Color off = new Color(0, 0, 0, 0);
    private static bool b = true;
    // Start is called before the first frame update
    void Start()
    {
        lightSource = gameObject.transform.GetChild(0).gameObject;
        mats = gameObject.transform.GetChild(1).gameObject.GetComponent<Renderer>().materials;
        bulb = mats[1];
        toggleVocabLight();
    }


    public static void toggleVocabLight()
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

    static void turnOn()
    {
        bulb.SetColor("_EmissionColor", on);
        lightSource.SetActive(true);
    }

    static void turnOff()
    {
        bulb.SetColor("_EmissionColor", off);
        lightSource.SetActive(false);
    }

}
