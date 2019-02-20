using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimPicker : MonoBehaviour
{

    public enum AnimationToPlay { None, Scale };

    public AnimationToPlay AnimToPlay;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        switch (AnimToPlay)
        {
            case AnimationToPlay.None:
                anim.SetTrigger("None");
                break;
            case AnimationToPlay.Scale:
                anim.SetTrigger("Scale");
                break;
            default:
                anim.SetTrigger("None");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
