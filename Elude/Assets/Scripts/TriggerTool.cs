using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TriggerTool : MonoBehaviour
{
    [Header("Settings")]
    public bool enable;

    public enum TriggerType {enableObject, disableObject, instantiateObject}
    public TriggerType onTrigger;

    public enum OnCompletion { nothing, disableTrigger }
    public OnCompletion onCompletion;

    [Header("Objects - NOT ALL ARE NEEDED")]
    public GameObject targetObject;         // The target object to be activated / deactivated
    public GameObject destination;          // The location at which an object will be instantiated
    public GameObject instantiatedObject;   // The object to instantiate

    private void OnTriggerEnter(Collider other)
    {
        if (enable) // Checks if the trigger is enabled
        {
            if (onTrigger == TriggerType.enableObject) // Activates the target object upon triggering
            {
                targetObject.gameObject.SetActive(true);
            }
            else if (onTrigger == TriggerType.disableObject) // Deactivated the target object upon triggering
            {
                targetObject.gameObject.SetActive(false);
            }
            else if (onTrigger == TriggerType.instantiateObject) // Spawns a new object at a destination upon triggering
            {
                Instantiate(instantiatedObject, destination.transform);
            }

            if (onCompletion == OnCompletion.nothing)
            {
                // do nothing ( ͡° ͜ʖ ͡°)
            }
            else if (onCompletion == OnCompletion.disableTrigger) // If toggled, will disable the trigger after running once.
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}

