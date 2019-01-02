using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    Transform player;
    Transform nextRoom;
    void Start() {
        player = ActiveTargets.Possess.transform;

        nextRoom = GameObject.Find("spawn").transform;
    }

    public void OpenDoor() {
        if (GetComponent<Animator>().GetBool("open") != true) {
            GetComponent<Animator>().SetBool("open", true);
          //  StartCoroutine("Fade", 3f);
        } else if (GetComponent<Animator>().GetBool("open") == true) {
            GetComponent<Animator>().SetBool("open", false);
        }

    }

    public void ChangeRoom() {
        player.SetPositionAndRotation(nextRoom.position,nextRoom.rotation);
        GetComponent<Animator>().SetBool("open", false);
    }

    // IEnumerator Fade(float s) {
    //      yield return new WaitForSeconds(s);

    //   ChangeRoom(nextRoom);
    // }

}
