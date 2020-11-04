using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogRotation : MonoBehaviour
{

    public bool reverse;
    public bool isSpinning;

    public GameObject previousCog;

    void Update()
    {
        if(previousCog.activeSelf == true)
        {
            if(previousCog.GetComponent<CogRotation>().isSpinning == true)
            {

                isSpinning = true;

                if (!reverse)
                {
                    transform.Rotate(0, 0, 0.75f, Space.World);
                }
                else
                {
                    transform.Rotate(0, 0, -0.75f, Space.World);
                }
            }
        }
    }
}
