﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbing : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        // Makes the trigger appear in the right spot
        transform.localScale = new Vector3((0.1f/transform.parent.localScale.x), transform.localScale.y, transform.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
