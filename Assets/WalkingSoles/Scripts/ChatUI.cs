using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Chat animations and animator are in the SuperCyan Character Package

public class ChatUI : MonoBehaviour {
    bool hidden = true;
    public Animator[] answerBubbles;
    public Text chatText;

    // Use this for initialization
    void Start () {

	}

	// Update is called once per frame
	public void NextBubble() {
        if (hidden)
        {
            GetComponent<Animator>().Play("PopUp");
            hidden = false;
            StartCoroutine("ShowOptions");
        }
	}

    public void UpdateText(string newText)
    {
        chatText.text = newText;

    }

    IEnumerator ShowOptions()
    {
        yield return new WaitForSeconds(1f);
        answerBubbles[0].Play("PopUp");

        yield return new WaitForSeconds(.2f);
        answerBubbles[1].Play("PopUp");

        yield return new WaitForSeconds(.2f);
        answerBubbles[2].Play("PopUp");

    }
}
