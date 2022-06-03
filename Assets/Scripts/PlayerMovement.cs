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
    public float minJumpHeight = 30f;

    public Transform wallCheck;
    public float wallDistance = 0.6f;
    public LayerMask wallMask;

    bool onWall;

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
        onWall = Physics.CheckSphere(wallCheck.position, wallDistance, wallMask);

        if (onWall && AboveGround() && z > 0)
        {
            velocity.y = 0f;
        }

        /*if (Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }*/
    }

    private bool AboveGround()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight, groundMask);
    }
}
