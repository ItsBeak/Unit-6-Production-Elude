using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{

    public GameManager gameManager;             // A reference to the game manager

    private void OnTriggerExit(Collider other)  // When the player enters the trigger of the collectible, the counter is increased
    {                                           // by one, and the collectible is deactivated
        if (other.tag == "Player")
        {
            gameManager.collectibleCounter++; // Increases counter by one
            gameObject.SetActive(false); // Disables the collectible
        }
    }
}
