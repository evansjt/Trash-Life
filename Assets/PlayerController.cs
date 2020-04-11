using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float rotationSpeed = 7f;
    Camera mainCam;
    [SerializeField] float sprintSpeed = 10f;
    [SerializeField] float jumpForce = 15f;

    [SerializeField] Transform groundCheck;

    [SerializeField] LayerMask whatIsGround;

    [SerializeField] float grav = 25f;

    bool justJumped = false;

    CharacterController controller;

    Vector2 inputs;
    Vector3 movement;

    public bool isGrounded = false;

    [SerializeField] float groundCheckRadius = 0.15f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        GetMovementInputs();
        ApplyGravity();
        CheckGroundCollision();
        HandleJumpInput();
        HandleMovement();
    }

    private void GetMovementInputs()
    {
        inputs.x = Input.GetAxis("Horizontal");
        inputs.y = Input.GetAxis("Vertical");
    }

    private void ApplyGravity()
    {
        movement.y -= (grav * Time.deltaTime);
    }

    private void CheckGroundCollision()
    {
        if (justJumped)
        {
            isGrounded = false;
            justJumped = false;
            return;
        }

        Collider[] colliders = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, whatIsGround);

        
        if(colliders.Length > 0)
        {
            isGrounded = true;
            movement.y = 0f;
        } else
        {
            isGrounded = false;
        }


    }

    private void HandleJumpInput()
    {
        float val = Input.GetAxis("Jump");
        if ((val > 0f) && isGrounded)
        {
            movement.y = jumpForce;
            justJumped = true;
        }
        
    }

    private void HandleMovement()
    {
        Vector2 dir2D = inputs.normalized;

        Vector3 dir3D = mainCam.transform.forward * dir2D.y + mainCam.transform.right * dir2D.x;
        Vector3 dir = new Vector3(dir3D.x, 0f, dir3D.z).normalized;

        if(Input.GetKey(KeyCode.LeftShift))
        {
            movement.x = (dir3D.x * sprintSpeed);
            movement.z = (dir3D.z * sprintSpeed);
        } else
        {
            movement.x = (dir3D.x * walkSpeed);
            movement.z = (dir3D.z * walkSpeed);
        }

        if(inputs != Vector2.zero)
        {
            transform.forward = Vector3.Lerp(transform.forward, dir, Time.deltaTime * rotationSpeed);
        }

        controller.Move(movement * Time.deltaTime);
    }
}
