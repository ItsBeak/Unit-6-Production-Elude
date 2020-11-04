using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{

    public GameManager gameManager;             // A reference to the game manager
    AudioSource audioSource;
    public AudioClip clip;

    public GameObject wallCog;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerExit(Collider other)  // When the player enters the trigger of the collectible, the counter is increased
    {                                           // by one, and the collectible is deactivated
        if (other.tag == "Player")
        {
            // gameManager.collectibleCounter++; // Increases counter by one
            // audioSource.PlayOneShot(clip, 0.05f);
            // gameObject.SetActive(false); // Disables the collectible

            gameManager.collectibleCounter++; // Increases counter by one
            audioSource.PlayOneShot(clip, 0.05f); // Play the pickup sound
            gameObject.GetComponent<CapsuleCollider>().enabled = false; // Turns off the collider, so the collectible cannot be picked up again
            gameObject.GetComponentInChildren<MeshRenderer>().enabled = false; // Hides the mesh of the collectible
            gameObject.GetComponentInChildren<Light>().enabled = false; // Disables the light

            wallCog.SetActive(true);    //Activates a cog on the wall

        }
    }
}
