using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffOnLeave : DefaultTrackableEventHandler
{
    protected override void OnTrackingFound()
    {
        UniversalMediaPlayer turnOffuniMed = FindObjectOfType<UniversalMediaPlayer>();

        turnOffuniMed.Play();

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
