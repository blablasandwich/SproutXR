using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomSproutXRTrackableEventHandler : DefaultTrackableEventHandler
{
    public int VideoToPlay;

    protected override void OnTrackingFound()
    {
        UniversalMediaPlayer turnOffuniMed = FindObjectOfType<UniversalMediaPlayer>();
        TV_Behavior TV = FindObjectOfType<TV_Behavior>();

        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);

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
