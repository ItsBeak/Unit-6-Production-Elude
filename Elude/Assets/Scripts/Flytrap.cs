using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flytrap : MonoBehaviour
{
    public float snapTimer = 5;
    private float timer;
    private float fadeTimer;

    private bool isTriggered;
    private bool isInMouth;
    private bool isPlayerCaught = false;

    private bool runFlytrapSequence = false;

    public GameManager gameManager;

    public GameObject outputLocation;
    public GameObject player;

    public Image fadePanel; 

    private void Start()
    {
        timer = snapTimer;
        fadeTimer = 0;
        fadePanel.color = Color.black;
        fadePanel.CrossFadeAlpha(0, 1f, false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isInMouth = true;

            if (isTriggered == false)
            {
                timer = snapTimer;

                isTriggered = true;
            }

        }
    }
    void Update()
    {

        if (timer >= 0 && isTriggered == true)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0 && isInMouth == true && isTriggered == true)
        {
            PlayerCaught();
        }

        if (timer <= 0 && isPlayerCaught == false )
        {
            isTriggered = false;
        }


        if (runFlytrapSequence == true)
        {
            //Hides the player
            player.GetComponent<MeshRenderer>().enabled = false;

            if (fadeTimer <= 0)
            {

                // Moves player to output location
                player.transform.position = outputLocation.transform.position;
                fadePanel.CrossFadeAlpha(0, 1f, false);

                //Shows the player
                player.GetComponent<MeshRenderer>().enabled = true;

                //Resets the variables
                runFlytrapSequence = false;
                isPlayerCaught = false;

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

        //Reset flytrap
        isPlayerCaught = true;
        isTriggered = false;
        runFlytrapSequence = true;
    }
}
