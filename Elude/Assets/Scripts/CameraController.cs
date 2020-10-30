using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Transform of the pareny
    Transform parent;
    // Object camera orbits
    public GameObject player;
    // Rotation for camera
    Vector3 localRotation;
    // How sensitive the mouse is
    float mouseSensitivity = 4f;
    // Modifies time taken to go from a to b
    public float orbitDampening = 10f;


    // Use this for initialization
    void Start()
    {
        mouseSensitivity = player.GetComponent<Player>().mouseSensitivity;
        parent = transform.parent;
    }


    void LateUpdate()
    {
        transform.parent.position = player.transform.position;
        //Rotation of the Camera based on Mouse Coordinates
        localRotation.x += Input.GetAxis("Mouse X") * mouseSensitivity;
        localRotation.y -= Input.GetAxis("Mouse Y") * mouseSensitivity; 

        //Clamp the y Rotation to horizon and not flipping over at the top
        if (localRotation.y < -80f)
            localRotation.y = -80f;
        else if (localRotation.y > 80f)
            localRotation.y = 80f;
    
        //Actual Camera Transformations
        Quaternion QT = Quaternion.Euler(localRotation.y, localRotation.x, 0);
        parent.rotation = Quaternion.Lerp(parent.rotation, QT, Time.deltaTime * orbitDampening);
    }
}