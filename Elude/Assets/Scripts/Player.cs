using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    // Can the player jump
    bool canJump = true;
    // Is the player currently climbing
    bool isClimbing = false;
    // Is the player just used a bounce pad
    bool bounce = false;
    // How high the player can jump
    public float jumpHeight;
    // How fast the player can climb
    public float climbSpeed;
    // How fast the player can move
    public float playerSpeed;
    //The players velocity, utalized by jump and bounce mechanics
    Vector3 playerVelocity;
    // The direction the player is moving in
    Vector3 moveDirection;
    // This one is pretty self explanitory
    CharacterController controller;
    // Instance of MakeGoBoing script
    MakeGoBoing makeGoBoing;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    // 
    void Update()
    {
        if (isClimbing || controller.isGrounded)
        { 
            canJump = true;
            bounce = false;
        }
        if (canJump && !bounce)
        {
            playerVelocity.y = 0.0f;            
        }
            
        moveDirection = new Vector3(Input.GetAxis("Horizontal") * playerSpeed, 0, isClimbing ? 0 : Input.GetAxis("Vertical")* playerSpeed);
        
        if (Input.GetButtonDown("Jump") && canJump)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * Physics.gravity.y);
            canJump = false;
            if (isClimbing)
            {
                StopClimbing();
            }
        }
        if (isClimbing)
        {
            
            if (Input.GetAxis("Vertical") != 0)
            {

                moveDirection.y += Input.GetAxis("Vertical") * climbSpeed;
            }
        }
        else
        {
            moveDirection += Physics.gravity;
        }
        playerVelocity.y += Physics.gravity.y * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
        controller.Move(playerVelocity * Time.deltaTime);
        
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
        if (other.gameObject.tag == "Vines")
        {
            isClimbing = true;
        }
        else if (other.gameObject.tag == "Bouncy")
        {
            bounce = true;
            makeGoBoing = other.GetComponent<MakeGoBoing>();
            playerVelocity.y += Mathf.Sqrt(makeGoBoing.boingHeight * -3.0f * Physics.gravity.y);
            Debug.Log("Boing");
        }
    }

}
