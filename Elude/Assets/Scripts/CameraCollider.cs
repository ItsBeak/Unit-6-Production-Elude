using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollider : MonoBehaviour
{
    public float minDistance;
    public float maxDistance;
    public float smooth;
    float distance;
    Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
        dir = transform.localPosition.normalized;
        distance = transform.localPosition.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 desiredPos = transform.parent.TransformPoint(dir * maxDistance);
        RaycastHit hit;
        if (Physics.Linecast(transform.parent.position, desiredPos, out hit))
        {
            distance = Mathf.Clamp((hit.distance * 0.9f), minDistance, maxDistance);
        }
        else
        {
            distance = maxDistance;
        }
        transform.localPosition = Vector3.Lerp(transform.localPosition, dir * distance, Time.deltaTime * smooth);
    }
}
