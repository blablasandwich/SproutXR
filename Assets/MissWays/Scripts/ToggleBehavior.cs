using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleBehavior : MonoBehaviour
{
    public Animator toggleAnim;

    // Start is called before the first frame update
    public void toggle()
    {
        //print("togglingToggle");
        toggleAnim.Play("toggleAnimation");
        toggleAnim.speed *= -1f;
    }
}
