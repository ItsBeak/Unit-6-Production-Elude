using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSteps : MonoBehaviour
{
    public AudioSource ac;

    public AudioClip left;
    public AudioClip right;

    public void PlayStep(int foot)
    {
        if (foot == 1)
        {
            ac.PlayOneShot(left);

        }
        else if (foot == 2)
        {
            ac.PlayOneShot(right);

        }



    }


}
