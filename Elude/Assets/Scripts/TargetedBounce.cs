using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetedBounce : MonoBehaviour
{
    // The second child of the mushrooms
    Transform trigger;
    Transform child;
    Player player;


    // Start is called before the first frame update
    void Start()
    {
        trigger = transform.GetChild(0);
        child = transform.GetChild(1);
        // Makes the trigger appear in the right spot
        trigger.localScale = new Vector3(trigger.localScale.x, 0.1f / trigger.parent.localScale.y, trigger.localScale.z);
        trigger.localPosition = new Vector3(0, (0.1f / trigger.parent.localScale.y) + trigger.parent.localScale.y / 2, 0);
    }
}
