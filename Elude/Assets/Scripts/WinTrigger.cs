using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    public SceneSwitcher switcher;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            switcher.ToggleSceneChange();
        }
    }
}
