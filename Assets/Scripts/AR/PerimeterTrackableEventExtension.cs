using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerimeterTrackableEventExtension : DefaultTrackableEventHandler
{
    public int VideoToPlay;

    // TODO: Move debug code to external file
    public Text debugText;

    public void DebugAR<T> (T msg)
    {
        // Function displays text on AR screen for debug on the phone
        debugText.text += msg.ToString() + "\n";
    }

    public GameObject Fence;

    protected override void OnTrackingFound()
    {
        UniversalMediaPlayer turnOffuniMed = FindObjectOfType<UniversalMediaPlayer>();
        TV_Behavior TV = FindObjectOfType<TV_Behavior>();

        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);

        DebugAR("Tracking Found: " + mTrackableBehaviour.TrackableName);

        // Enable rendering:
        foreach (var component in rendererComponents)
            component.enabled = true;

        // Enable colliders:
        foreach (var component in colliderComponents)
            component.enabled = true;

        // Enable canvas':
        foreach (var component in canvasComponents)
            component.enabled = true;

        Fence.SetActive(true);
    }

    protected override void OnTrackingLost()
    {
        UniversalMediaPlayer turnOffuniMed = FindObjectOfType<UniversalMediaPlayer>();

        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
        DebugAR("Tracking lost");

        turnOffuniMed.Pause();

        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);

        // Disable rendering:
        foreach (var component in rendererComponents)
            component.enabled = false;

        // Disable colliders:
        foreach (var component in colliderComponents)
            component.enabled = false;

        // Disable canvas':
        foreach (var component in canvasComponents)
            component.enabled = false;
    }
}
