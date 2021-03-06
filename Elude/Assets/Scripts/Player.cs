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
    // How high the player can jump
    public float jumpHeight;
    // How fast the player can climb
    public float climbSpeed;
    // How fast the player can move
    public float playerSpeed;
    // The sensitivity of the mouse
    public float mouseSensitivity;
    // Distance between the player and pursuer before death
    public float deathRange;
    //The players velocity, utalized by jump and bounce mechanics
    Vector3 playerVelocity;
    // The direction the player is moving in
    public  Vector3 moveDirection;
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
    // The lose condition
    public GameObject pursuer;

    // A reference to the animator on the players rig
    public Animator playerAnimator;



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
        moveDirection = Vector3.zero;
        if (PursuerIsClose())
        {
            GetComponent<SceneSwitcher>().sceneName = "LoseMenu";
            Cursor.lockState = CursorLockMode.Confined;
            GetComponent<SceneSwitcher>().ToggleSceneChange();
        }
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
        if (controller.isGrounded && !isBouncing)
        {
            playerVelocity.y = 0.0f;
            playerAnimator.SetBool("Bounce", false);
        }
        if (isClimbing || controller.isGrounded)
        {
            canJump = true;
            isBouncing = false;
            playerAnimator.SetBool("Jump", false);

        }
        if (isBouncing)
        {
           playerAnimator.SetBool("Running", false);
        }



        // Regular movment
        moveDirection = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal") *playerSpeed, 0, isClimbing ? 0 : Input.GetAxis("Vertical") * playerSpeed));
        // Can the player jump
        if (Input.GetButtonDown("Jump") && canJump)
        {
            //Jump
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * Physics.gravity.y);
            canJump = false;

            playerAnimator.SetBool("Jump", true);

            if (isClimbing)
            {
                StopClimbing();
            }

        }

        playerAnimator.SetFloat("Falling", Mathf.Round(playerVelocity.y));

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            playerAnimator.SetBool("Running", true);
        }
        else
        {
            playerAnimator.SetBool("Running", false);
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
        playerVelocity.y += Physics.gravity.y * Time.deltaTime;
        // For Movement
        controller.Move(moveDirection * Time.deltaTime);
        // For Forces (bouncing)
        controller.Move(playerVelocity * Time.deltaTime);
        float rotate = 0;
        if (Input.GetAxis("Mouse X") != 0)
        {
            rotate = Input.GetAxis("Mouse X") * mouseSensitivity;
        }
        if (Input.GetAxis("Joystick X") != 0)
        {
            rotate = Input.GetAxis("Joystick X") * mouseSensitivity;
        }
         

        transform.Rotate(new Vector3(0, rotate));
        
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
            canJump = false;
            makeGoBoing = other.GetComponent<MakeGoBoing>();
            playerVelocity.y = Mathf.Sqrt(makeGoBoing.boingHeight * -3.0f * Physics.gravity.y);
            playerAnimator.SetBool("Jump", true);
            playerAnimator.SetBool("Bounce", true);
        }
        // Bounce player and lock controls
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
        else if( other.gameObject.tag == "WinGame")
        {
            GetComponent<SceneSwitcher>().sceneName = "WinMenu";
            Cursor.lockState = CursorLockMode.Confined;
            GetComponent<SceneSwitcher>().ToggleSceneChange();
        }
    }

    bool PursuerIsClose()
    {
        Vector3 difference = new Vector3(Mathf.Abs(pursuer.transform.position.x - transform.position.x), Mathf.Abs(pursuer.transform.position.y - transform.position.y), Mathf.Abs(pursuer.transform.position.z - transform.position.z));
        if (difference.x < deathRange && difference.y < deathRange && difference.z < deathRange)
        {
            return true;
        }

        return false;
    }
}
