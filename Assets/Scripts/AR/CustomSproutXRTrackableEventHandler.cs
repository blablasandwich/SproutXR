using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomSproutXRTrackableEventHandler : DefaultTrackableEventHandler
{
    public int VideoToPlay;
    public Text debugText;

    public void DebugAR<T> (T msg)
    {
        debugText.text += msg.ToString() + "\n";
    }

    protected override void OnTrackingFound()
    {
        UniversalMediaPlayer turnOffuniMed = FindObjectOfType<UniversalMediaPlayer>();
        TV_Behavior TV = FindObjectOfType<TV_Behavior>();

        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);

        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
        DebugAR("Tracking Found");

        // Enable rendering:
        foreach (var component in rendererComponents)
            component.enabled = true;

        // Enable colliders:
        foreach (var component in colliderComponents)
            component.enabled = true;

        // Enable canvas':
        foreach (var component in canvasComponents)
            component.enabled = true;

        switch (VideoToPlay)
        {
            case 1:
                TV.activeVideo = 1;
                DebugAR("Playing Video 1");
                DebugAR(TV.mediaPlayer.Path);
                TV.ReplayCanvas.enabled = false;
                TV.RunCheckVid();
                break;
            case 2:
                TV.activeVideo = 2;
                TV.ReplayCanvas.enabled = false;
                TV.RunCheckVid();
                break;
            case 3:
                TV.activeVideo = 3;
                TV.ReplayCanvas.enabled = false;
                TV.RunCheckVid();
                break;
            default:
                print("Video to play not found");
                break;
        }
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
