using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Variables
    public float runSpeed = 8f;
    public float walkSpeed = 6f;
    public float gravity = -10f;
    public float jumpHeight = 15f;
    public float groundRayDistance = 1.1f;
    private CharacterController controller; // Reference to character controller
    private Vector3 motion; // Is the movement offset per frame
    private bool isJumping = false;
    private float currentJumpHeight;

    // Functions
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    private void Update()
    {
        // W A S D / Right Left Up Down Arrow Input
        float inputH = Input.GetAxis("Horizontal");
        float inputV = Input.GetAxis("Vertical");
        // Left Shift Input
        bool inputRun = Input.GetKey(KeyCode.LeftShift);
        // Space Bar Input
        bool inputJump = Input.GetButtonDown("Jump");
        // Put Horizontal & Vertical input into vector
        Vector3 inputDir = new Vector3(inputH, 0f, inputV);
        // Rotate direction to Player's Direction
        inputDir = transform.TransformDirection(inputDir);
        // If input exceeds length of 1
        if (inputDir.magnitude > 1f)
        {
            // Normalize it to 1f!
            inputDir.Normalize();
        }

        // If running
        if (inputRun)
        {
            Run(inputDir.x, inputDir.z);
        }
        else
        {
            Walk(inputDir.x, inputDir.z);
        }

        // If is Grounded
        if (controller.isGrounded)
        {
            // .. And jump?
            if (inputJump)
            {
                Jump(jumpHeight);
            }

            // Cancel the y velocity
            motion.y = 0f;

            // Is jumping bool set to true
            if (isJumping)
            {
                // Set jump height
                motion.y = currentJumpHeight;
                // Reset back to false
                isJumping = false;
            }
        }
        motion.y += gravity * Time.deltaTime;
        controller.Move(motion * Time.deltaTime);
    }
    private void Move(float inputH, float inputV, float speed)
    {
        Vector3 direction = new Vector3(inputH, 0f, inputV);

        // Convert local direction to world space direction (relative to Player's transform)
        //direction = transform.TransformDirection(direction);

        motion.x = direction.x * speed;
        motion.z = direction.z * speed;
    }
    public void Walk(float inputH, float inputV)
    {
        Move(inputH, inputV, walkSpeed);
    }
    public void Run(float inputH, float inputV)
    {
        Move(inputH, inputV, runSpeed);
    }
    public void Jump(float height)
    {
        isJumping = true;
        currentJumpHeight = height;
    }
}
