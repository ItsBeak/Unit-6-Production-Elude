using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CameraController : MonoBehaviour
{
    // Transform of the pareny
    Transform parent;
    // Object camera orbits
    public GameObject player;
    public GameObject pursuer;
    // Rotation for camera
    Vector3 localRotation;
    // How sensitive the mouse is
    float mouseSensitivity = 4f;
    // Modifies time taken to go from a to b
    public float orbitDampening = 10f;
    public GameObject transitionPoint;
    public bool doorOpening;
    float timer;
    

    // Use this for initialization
    void Start()
    {
        mouseSensitivity = player.GetComponent<Player>().mouseSensitivity;
        parent = transform.parent;
        timer = 0;
    }


    void LateUpdate()
    {
        if (doorOpening)
        {
            player.GetComponent<Player>().enabled = false;
            pursuer.GetComponent<NavMeshAgent>().enabled = false;
            parent.position = transitionPoint.transform.position;
            parent.rotation = transitionPoint.transform.rotation;
            timer += Time.deltaTime;
            if (timer > 16)
            {
                doorOpening = false;
                player.GetComponent<Player>().enabled = true;
                pursuer.GetComponent<NavMeshAgent>().enabled = true;
            }
            else
                return;
        }
        transform.parent.position = player.transform.position;
        //Rotation of the Camera based on Mouse Coordinates
        if (Input.GetAxis("Mouse X") != 0)
        {
            localRotation.x += Input.GetAxis("Mouse X") * mouseSensitivity;
        }
        if (Input.GetAxis("Joystick X") != 0)
        {
            localRotation.x += Input.GetAxis("Joystick X") * mouseSensitivity;
        }
        if (Input.GetAxis("Mouse Y") != 0)
        {
            localRotation.y -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        }
        if (Input.GetAxis("Joystick Y") != 0)
        {
            localRotation.y -= Input.GetAxis("Joystick Y") * mouseSensitivity;
        }

        //Clamp the y Rotation to horizon and not flipping over at the top
        if (localRotation.y < -80f)
            localRotation.y = -80f;
        else if (localRotation.y > 30f)
            localRotation.y = 30f;
    
        //Actual Camera Transformations
        Quaternion QT = Quaternion.Euler(localRotation.y, localRotation.x, 0);
        parent.rotation = Quaternion.Lerp(parent.rotation, QT, Time.deltaTime * orbitDampening);
    }
}