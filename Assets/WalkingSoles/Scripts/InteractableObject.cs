using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class InteractableObject : MonoBehaviour
{
    /* Trigger stuff may be scrapped
    EventTrigger trigger;

    void Awake()
    {

        trigger = gameObject.GetComponent<EventTrigger>();
        if(!trigger)
        {
            Debug.LogError("There is no event trigger on interactable object of: " + gameObject.name);
            // Below is a bit bugged, it only calls from the inherited DoAction instead of the child DoAction
            trigger = gameObject.AddComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((data) => { DoAction( (PointerEventData)data ); });
            trigger.triggers.Add(entry);
        }

    }
    */

    public void DoAction()
    {
        //Display menu or something
        Debug.Log(gameObject.name + " Has done something.");
    }
}
