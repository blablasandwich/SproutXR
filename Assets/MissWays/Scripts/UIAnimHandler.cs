using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimHandler : MonoBehaviour
{
    public bool canChangeAnim = true;

    public void ToggleSpeaking()
    {
        if (canChangeAnim)
        {
            transform.GetChild(0).GetComponent<Animator>().SetBool("Speak", !GetComponent<Animator>().GetBool("Speak"));
            canChangeAnim = false;
        }
    }

    public void SetChangeAnim()
    {
        canChangeAnim = true;
    }

    public void CloseUI()
    {
        this.GetComponent<Animator>().SetTrigger("Close");
    }

    public void HideUI()
    {
        this.gameObject.SetActive(false);
    }
    public void ShowUI()
    {
        this.gameObject.SetActive(true);
        this.GetComponent<Animator>().SetTrigger("Open");
    }

}
