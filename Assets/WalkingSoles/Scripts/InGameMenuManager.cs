using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenuManager : MonoBehaviour {
    public static bool Paused = false;
    private GameObject pauseMenuUI;

    private void Start() {
        pauseMenuUI = this.transform.GetChild(1).gameObject;
    }

    public void pause() {
        if(Paused) {
            Paused = false;
            Time.timeScale = 1f;
            HidePauseHud();
        } else {
            Paused = true;
            Time.timeScale = 0f;
            ShowPauseHud();
        }
    }

    public void ShowPauseHud() {
        pauseMenuUI.SetActive(true);
    }

    public void HidePauseHud()
    {
        pauseMenuUI.SetActive(false);
    }


    public void ExitGame() {
        Application.Quit();
    }
}
