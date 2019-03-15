using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginUIManager : MonoBehaviour
{
    public GameObject libraryCanvas;

    // Start is called before the first frame update
    public void CloseLoginCanvas()
    {
        gameObject.SetActive(false);
    }

    public void OpenLibraryCanvas()
    {
        libraryCanvas.SetActive(true);
    }
    // Update is called once per frame
    public void activateFreeTrial()
    {
        GameStateManager.instance.currentSubscription = EnumManager.ActivationType.Free;
    }
}
