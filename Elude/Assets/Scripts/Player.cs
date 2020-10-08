using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    // Can the player jump
    bool canJump = true;
    // Is the player currently climbing
    bool isClimbing = false;
    // Is the player using a bounce pad
    bool isBouncing = false;
    // Is the player alive
    bool isAlive = true;
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
    // The starting position of the player
    Vector3 startPos;
    // This one is pretty self explanitory
    CharacterController controller;
    // Instance of MakeGoBoing script
    MakeGoBoing makeGoBoing;
    TargetedBounce targetedBounce;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        transform.position = startPos;
    }

    // Update is called once per frame
    // 
    void Update()
    {
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
            isBouncing = true;
            makeGoBoing = other.GetComponent<MakeGoBoing>();
            playerVelocity.y += Mathf.Sqrt(makeGoBoing.boingHeight * -3.0f * Physics.gravity.y);
        }
        else if (other.gameObject.tag == "Targeted Bounce")
        {
             targetedBounce = other.GetComponent<TargetedBounce>();
            playerVelocity.x += targetedBounce.Target.x;
            playerVelocity.y += Mathf.Sqrt(targetedBounce.Target.y * -3.0f * Physics.gravity.y);
            playerVelocity.z += targetedBounce.Target.z;

        }
    }

}
