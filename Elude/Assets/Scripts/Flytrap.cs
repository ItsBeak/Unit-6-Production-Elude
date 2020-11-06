using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flytrap : MonoBehaviour
{
    
    public float snapTimer = 5;         // The timer that can be modified within the inspector in Unity
    private float timer;                // The internal timer that is used within code, references its public counterpart
    private float fadeTimer;            // Used in the fade in / out when caught

    private bool isTriggered;           // Toggles when the player first triggers the flytrap
    private bool isInMouth;             // Is true whenever the player is inside the mouth

    private bool runFlytrapSequence = false;    // Will run the flytraps sequence if toggled

    public GameObject outputLocation;   // The output location of the flytrap
    public GameObject player;           // A reference to the player
    public GameObject playerMesh;      // A reference to the player mesh

    public Image fadePanel;             // The black panel that fades in / out when the player is caught

    AudioSource audioSource;
    public AudioClip clip;

    private void Start()
    {
        timer = snapTimer;
        fadeTimer = 0;
        fadePanel.color = Color.black;
        fadePanel.CrossFadeAlpha(0, 1f, false);
        audioSource = GetComponent<AudioSource>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isInMouth = true;

            if (isTriggered == false) // Checks if the flytrap has been triggered, if it hasn't, it will trigger
            {
                timer = snapTimer;
                isTriggered = true;
            }
        }
    }
    void Update()
    {

        if (timer >= 0 && isTriggered == true) // Ticks down the timer once triggered
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0 && isInMouth == true && isTriggered == true) // Checks if the player is in the mouth of the trap when the timer runs out
        {
            PlayerCaught();
        }

        if (timer <= 0) // Resets the trap when the timer runs out
        {
            isTriggered = false;
        }

        if (runFlytrapSequence == true)
        {
            //Hides the player
            playerMesh.GetComponent<SkinnedMeshRenderer>().enabled = false;

            player.GetComponent<CharacterController>().enabled = false;



            if (fadeTimer <= 0)
            {

                // Moves player to output location
                player.transform.position = outputLocation.transform.position;
                fadePanel.CrossFadeAlpha(0, 1f, false);

                //Shows the player
                playerMesh.GetComponent<SkinnedMeshRenderer>().enabled = true;

                //Resets the variables
                runFlytrapSequence = false;

                player.GetComponent<CharacterController>().enabled = true;


            }
        }

        //Timer for the screen fade
        fadeTimer -= Time.deltaTime;

    }

    /// <summary>
    /// Lets the flytrap know when the player has left its mouth
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isInMouth = false;
        }  
    }

    /// <summary>
    /// Is called when the player is successfully caught in the mouth of the flytrap
    /// </summary>
    private void PlayerCaught()
    {
        // Fades screen to black
        fadeTimer = 1;
        fadePanel.CrossFadeAlpha(1, 1f, false);

        audioSource.PlayOneShot(clip, 0.1f); // Play the chomp sound

        //Reset flytrap
        isTriggered = false;
        runFlytrapSequence = true;
    }
}
