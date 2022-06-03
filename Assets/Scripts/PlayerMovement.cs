using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;

    Vector3 velocity;

    // ground movement

    public float speed = 12f;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;

    public float x;
    public float z;

    public Transform groundCheck;
    public float groundDistance = 0.1f;
    public LayerMask groundMask;

    bool isGrounded;

    // wall movement

    public float jumpDistance = 2f;
    public float minJumpHeight = 2f;

    public Transform wallCheckLeft;
    public Transform wallCheckRight;
    public float wallDistance = 0.1f;
    public LayerMask wallMask;

    bool onWallLeft;
    bool onWallRight;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        groundMovement();
        wallMovement();
    }

    void groundMovement()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    void wallMovement()
    {
        onWallLeft = Physics.CheckSphere(wallCheckLeft.position, wallDistance, wallMask);
        onWallRight = Physics.CheckSphere(wallCheckRight.position, wallDistance, wallMask);

        if ((onWallLeft || onWallRight) && AboveGround() && z > 0)
        {
            //velocity.y = 0f;
            if (Input.GetButtonDown("Jump"))
            {
                WallJump();
            }
        }
    }

    private bool AboveGround()
    {
        return !Physics.CheckSphere(groundCheck.position, minJumpHeight, groundMask);
    }

    private void WallJump()
    {
        Vector3 JumpForce = new Vector3(-500f, 0f, 0f);

        if (onWallLeft)
        {
            rb.AddForce(JumpForce, ForceMode.Impulse);
        }
        if (onWallRight)
        {
            rb.AddForce(JumpForce, ForceMode.Impulse);
        }
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        
    }
}
