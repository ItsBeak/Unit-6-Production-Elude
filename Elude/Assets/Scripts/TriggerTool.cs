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
    public GameObject targetObject;
    public GameObject destination;
    public GameObject instantiatedObject;

    private void OnTriggerEnter(Collider other)
    {
        if (enable)
        {
            if (onTrigger == TriggerType.enableObject)
            {
                targetObject.gameObject.SetActive(true);
            }
            else if (onTrigger == TriggerType.disableObject)
            {
                targetObject.gameObject.SetActive(false);
            }
            else if (onTrigger == TriggerType.instantiateObject)
            {
                Instantiate(instantiatedObject, destination.transform);
            }

            if (onCompletion == OnCompletion.nothing)
            {
                // do nothing ( ͡° ͜ʖ ͡°)
            }
            else if (onCompletion == OnCompletion.disableTrigger)
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}

