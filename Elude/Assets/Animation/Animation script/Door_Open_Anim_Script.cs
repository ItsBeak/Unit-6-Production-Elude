﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Open_Anim_Script : MonoBehaviour
{
    public bool cogsmet;
    public Animator door;
    bool cutsceneplayed = false;

    // Update is called once per frame
    void Update()
    {
        if (cogsmet == true)
        {
            door.SetBool("opening", true);
            if (!cutsceneplayed)
            {
                FindObjectOfType<Camera>().GetComponent<CameraController>().doorOpening = true;
                cutsceneplayed = true;
            }
            
        }
    }
}
