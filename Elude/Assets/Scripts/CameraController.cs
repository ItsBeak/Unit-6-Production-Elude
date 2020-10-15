using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    Transform Camera;
    Transform Parent;
    Vector3 LocalRotation;
    public float MouseSensitivity = 4f;
    public float OrbitDampening = 10f;


    // Use this for initialization
    void Start()
    {
        Camera = transform;
        Parent = transform.parent;
    }


    void LateUpdate()
    {
        //Rotation of the Camera based on Mouse Coordinates
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            LocalRotation.x += Input.GetAxis("Mouse X") * MouseSensitivity;
            LocalRotation.y -= Input.GetAxis("Mouse Y") * MouseSensitivity;

            //Clamp the y Rotation to horizon and not flipping over at the top
            if (LocalRotation.y < 0f)
                LocalRotation.y = 0f;
            else if (LocalRotation.y > 90f)
                LocalRotation.y = 90f;
        }
        //Actual Camera Rig Transformations
        Quaternion QT = Quaternion.Euler(LocalRotation.y, LocalRotation.x, 0);
        this.Parent.rotation = Quaternion.Lerp(this.Parent.rotation, QT, Time.deltaTime * OrbitDampening);
    }
}