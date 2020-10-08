using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetedBounce : MonoBehaviour
{
    public Vector3 Target;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(transform.localScale.x, 0.1f / transform.parent.localScale.y, transform.localScale.z);
        transform.localPosition = new Vector3(0, (0.1f / transform.parent.localScale.y) + transform.parent.localScale.y / 2, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
