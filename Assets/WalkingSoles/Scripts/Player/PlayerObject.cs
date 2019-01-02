using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerObject : InteractableObject
{
    public ChatUI chatBub;
    private Animator m_animators;

    void Start()
    {
        m_animators = GetComponent<Animator>();
    }
    //Function called from being pressed
    new public void DoAction()
    {
        //Pop up a menu or something
        Debug.Log(gameObject.name + " was Clicked");

        if(chatBub)
        {
            m_animators.SetTrigger("Wave");
            chatBub.NextBubble();
        }
    }
}
