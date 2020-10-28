﻿using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    // Can the player jump
    bool canJump = true;
    // Is the player currently climbing
    bool isClimbing = false;
    // Is the player using a bounce pad
    bool isBouncing = false;
    // Are the controls locked
    bool controlsLocked = false;
    // Is the player alive
    bool isAlive = true;
    // How high the player can jump
    public float jumpHeight;
    // How fast the player can climb
    public float climbSpeed;
    // How fast the player can move
    public float playerSpeed;
    // The sensitivity of the mouse
    public float mouseSensitivity;
    //The players velocity, utalized by jump and bounce mechanics
    Vector3 playerVelocity;
    // The direction the player is moving in
    Vector3 moveDirection;
    // The starting position of the player
    Vector3 startPos;
    // This one is pretty self explanitory
    CharacterController controller;
    // Instance of MakeGoBoing script
    MakeGoBoing makeGoBoing;
    // Instance of ParabolaController
    ParabolaController parabolaController;
    // The parabola that actually effects the player
    public GameObject settingsParabola;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        parabolaController = GetComponent<ParabolaController>();
        startPos = transform.position;
       parabolaController.Speed *= playerSpeed;
        transform.position = startPos;
    }

    // Update is called once per frame
    // 
    void Update()
    {
        if (controlsLocked)
        {
            if (!(settingsParabola.transform.position == transform.position))
            {
                transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * mouseSensitivity, 0));
                return;
            }
            else
            {
                controlsLocked = false;
            }
            
            
        }
        if (!isAlive)
        {
            //Do death
            gameObject.SetActive(false);
            transform.position = startPos;
            gameObject.SetActive(true);
        }
        if (isClimbing || controller.isGrounded)
        {
            canJump = true;
            isBouncing = false;
        }
        if (canJump && !isBouncing)
        {
            playerVelocity.y = 0.0f;
        }


        // Regular movment
        moveDirection = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal") * playerSpeed, 0, isClimbing ? 0 : Input.GetAxis("Vertical") * playerSpeed));
        // Can the player jump
        if (Input.GetButtonDown("Jump") && canJump)
        {
            //Jump
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * Physics.gravity.y);
            canJump = false;
            if (isClimbing)
            {
                StopClimbing();
            }
        }
        // Is the player Climbing
        if (isClimbing)
        {
            // Utilizes Vertical to go up/down instead of forward/backwards
            if (Input.GetAxis("Vertical") != 0)
            {

                moveDirection.y += Input.GetAxis("Vertical") * climbSpeed;
            }
        }
        else
        {
            //gravity for when player is not climbing
            moveDirection += Physics.gravity;
        }
        playerVelocity.y += Physics.gravity.y * Time.deltaTime;
        // For Movement
        controller.Move(moveDirection * Time.deltaTime);
        // For Forces (bouncing)
        controller.Move(playerVelocity * Time.deltaTime);
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * mouseSensitivity, 0));
    }
    



    
    private void OnTriggerExit(Collider other)
    {
        
        StopClimbing();
    }
    
    void StopClimbing()
    {
        isClimbing = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // if Touching vines climb them
        if (other.gameObject.tag == "Vines")
        {
            isClimbing = true;
        }
        // If touching bouncy mushrooms do some math (and bounce the player)
        else if (other.gameObject.tag == "Bouncy")
        {
            isBouncing = true;
            makeGoBoing = other.GetComponent<MakeGoBoing>();
            playerVelocity.y += Mathf.Sqrt(makeGoBoing.boingHeight * -3.0f * Physics.gravity.y);
        }
        else if (other.gameObject.tag == "Targeted Bounce")
        {
            Transform temp = other.transform.parent.GetChild(1);
            settingsParabola.transform.position = temp.position;
            settingsParabola.transform.GetChild(0).position = transform.position;
            settingsParabola.transform.GetChild(1).position = temp.GetChild(1).position;
            settingsParabola.transform.GetChild(2).position = temp.GetChild(2).position;
            controlsLocked = true;
            parabolaController.FollowParabola();
        }
        // Finish refers to the arc of the parabola, I should change that later
        else if (other.gameObject.tag == "Finish")
        {
            controlsLocked = false;
        }
    }

}
