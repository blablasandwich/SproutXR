using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
public class PerAreaHandler : MonoBehaviour
{
    public Text def;
    public Text prob;
    public Text ans;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void P()
    {
        def.text = "L + L + W + W";
        prob.text = "5ft + 5ft + 3ft + 3ft";
        ans.text = "16ft";
    }
    public void A()
    {
        def.text = "L * W";
        prob.text = "5ft * 3ft";
        ans.text = "15ft";
    }
}
