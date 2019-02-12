using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleBehavior : MonoBehaviour
{
    public Animator toggleAnim;

    // Start is called before the first frame update
    public void toggle(string selectedButton)
    {
        if(selectedButton == "area")
        {
            toggleAnim.SetTrigger("AreaSelected");

        }
        else toggleAnim.SetTrigger("PerimeterSelected");


    }
}
