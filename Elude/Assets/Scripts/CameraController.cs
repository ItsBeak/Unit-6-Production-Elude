using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    Transform Camera;
    Transform parent;
    public GameObject player;
    Vector3 localRotation;
    public float mouseSensitivity = 4f;
    public float orbitDampening = 10f;


    // Use this for initialization
    void Start()
    {
        Camera = transform;
        parent = transform.parent;
    }


    void LateUpdate()
    {
        transform.parent.position = player.transform.position;
        //Rotation of the Camera based on Mouse Coordinates
        localRotation.y -= Input.GetAxis("Mouse Y") * mouseSensitivity;

        //Clamp the y Rotation to horizon and not flipping over at the top
        if (localRotation.y < 0f)
            localRotation.y = 0f;
        else if (localRotation.y > 90f)
            localRotation.y = 90f;
    
        //Actual Camera Transformations
        // Test
        Quaternion QT = Quaternion.Euler(localRotation.y, player.transform.rotation.y, 0);
        parent.rotation = Quaternion.Lerp(parent.rotation, QT, Time.deltaTime * orbitDampening);
    }
}