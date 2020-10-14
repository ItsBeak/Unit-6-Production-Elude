using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetedBounce : MonoBehaviour
{
    public Vector3 target;
    Transform root;
    Transform startPoint;
    Transform maxPoint;
    Transform endPoint;
    Transform child;


    // Start is called before the first frame update
    void Start()
    {
        child = transform.GetChild(0);
        child.localScale = new Vector3(child.localScale.x, 0.1f / child.parent.localScale.y, child.localScale.z);
        child.localPosition = new Vector3(0, (0.1f / child.parent.localScale.y) + child.parent.localScale.y / 2, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
