using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class movementController : MonoBehaviour
{
    [Header("Player settings")]
    public Rigidbody2D rigid; //Grabs the selected rigidbody
    public float moveSpeed = 4.5f; //Sets a default moveSpeed
    public float jumpHeight = 120f; //Sets a default jumpHeight
    public float deadzone = 0.1f; //The point at which joystick axis input is registered in order to minimize drift.

    [Header("PlayerInputs")]
    public Vector2 playerLookDirection;
    public Vector2 playerMoveDirection;

    [Header("Physics")]
    public float velocityClamp = 7.0f; //A maximum velocity clamp used to prevent the player from reaching velocities that are too high.
    public float groundDistance = 0.68f; //Sets the distance at which the game considers the player to be on the ground
    public float friction = 4.0f; //Friction is used as a multiplier for normalizing the player's X velocity towards 0 when not moving and touching the ground.
    public float gravity = 25.0f; //Gravity is used as a -y velocity multiplier when the player is not touching the ground.
    public LayerMask groundMask;

    //Booleans used for physics logic.
    private bool moving = false; 
    public bool onGround = false;

    private Vector2 movementInput = Vector2.zero;
    private bool jumped = false;

    public Vector2 playerVelocity; //Displays the player's current velocity for debug purposes.

    void Update()
    { 
        Movement();
        PlayerPhysics();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        playerMoveDirection = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.action.phase == InputActionPhase.Started)
        {
            jumped = true;
            Movement();
        }
        else
        {
            jumped = false;
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        playerLookDirection = context.ReadValue<Vector2>();
    }

    private void Movement()
    {
        //Grabs the current position of the joystick
        float x = movementInput.x;
        float y = movementInput.y; //Currently unused, but will be touched on later for the look direction.

        playerVelocity = rigid.velocity; //Displayed the player velocity in the inspector (for debug)

        //Logic for checking if the character is moving. Separate from the actual movement to prevent jittering due to the friction system.
        if (x >= deadzone || x <= -deadzone)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }

        //Generates the direction to add velocity in
        Vector2 moveDirection = transform.right * x;

        //Checks to ensure X (joystick position) is not within the deadzone and that the rigidbody's X velocity is not greater than 10 or less than -10.
        if (x >= deadzone && !(rigid.velocity.x > velocityClamp))
        {
            rigid.AddForce(moveDirection * Time.deltaTime * moveSpeed / 1000.0f, ForceMode2D.Impulse);
        }
        else if (x <= -deadzone && !(rigid.velocity.x < -velocityClamp))
        {
            rigid.AddForce(moveDirection * Time.deltaTime * moveSpeed / 1000.0f, ForceMode2D.Impulse);
        }

        //Jumps.
        if (onGround == true && jumped)
        {
            rigid.AddForce(transform.up * jumpHeight / 100000.0f, ForceMode2D.Impulse);
        }
    }

    private void PlayerPhysics()
    {
        Vector2 normalizeVelocity = new Vector2(0.0f, 0.0f); //Zero-velocity vector used for math in the friction system.

        //Used to use raycast, but that system broke frequently.
        //Instead, I replaced it with the ground checkpoint trigger, alongside the groundCheckpoint script.
        if (onGround)
        {
            //converts the player's x velocity into an absolute int (unsigned) in order to check velocity against friction.
            int absoluteXVelocity = Mathf.Abs((int)rigid.velocity.x); 
            if (absoluteXVelocity > 0 && ((Mathf.Abs(rigid.velocity.x) >= velocityClamp + 1) || !moving))
            {
                //Moves the player's X velocity towards 0 as long as it's above friction.
                if (rigid.velocity.x > friction)
                {
                    normalizeVelocity.x -= friction;
                    normalizeVelocity.y = rigid.velocity.y;
                    rigid.velocity = normalizeVelocity;
                }
                else if (rigid.velocity.x < -friction)
                {
                    normalizeVelocity.x += friction;
                    normalizeVelocity.y = rigid.velocity.y;
                    rigid.velocity = normalizeVelocity;
                }
            }
            if (absoluteXVelocity <= friction && ((Mathf.Abs(rigid.velocity.x) >= velocityClamp + 1) || !moving)) //Sets the player's X velocity to 0 if it's below friction and they aren't actively moving.
            {
                normalizeVelocity.y = rigid.velocity.y;
                normalizeVelocity.x = 0.0f;
                rigid.velocity = normalizeVelocity;
            }
            if (moving)
            {
                //This would be a good place to put a switch case for different ground audio effects
                //The ground colliders all have different Physics materials, which can be checked via script
                //;)
            }
        }
        else
        {
            //Applies -y velocity as long as the player is not touching the ground.
            rigid.AddForce(-transform.up * gravity * Time.deltaTime / 10000.0f, ForceMode2D.Impulse);
        }
    }
}
