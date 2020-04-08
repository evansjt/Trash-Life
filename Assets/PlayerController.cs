using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float sprintSpeed = 10f;
    [SerializeField] float jumpForce = 5f;

    [SerializeField] Transform groundCheck;

    [SerializeField] LayerMask whatIsGround;

    [SerializeField] float grav = 1f;

    CharacterController controller;

    Vector3 moveDir;

    public bool isGrounded = false;

    [SerializeField] float groundCheckRadius = 0.15f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        UpdateRotation();
        HandleGroundCollision();
        HandleGravity();
        HandleJump();
        HandleMovement();
    }

    private void HandleGravity()
    {
        if(!isGrounded)
        {
            moveDir.y -= grav;
        }
    }

    private void HandleGroundCollision()
    {
        Collider[] colliders = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, whatIsGround);

        if(colliders.Length > 0)
        {
            isGrounded = true;
            moveDir.y = 0f;
        } else
        {
            isGrounded = false;
        }


    }

    private void HandleJump()
    {
        float val = Input.GetAxis("Jump");
        if ((val > 0f) && isGrounded)
        {
            moveDir.y = jumpForce;
        }
        
    }

    private void HandleMovement()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float zAxis = Input.GetAxis("Vertical");

        Vector3 movement = (transform.forward * zAxis) + (transform.right * xAxis);

        RaycastHit hitInformation;

        Physics.SphereCast(transform.position, controller.radius, Vector3.down, out hitInformation, (controller.height / 2f));

        movement = Vector3.ProjectOnPlane(movement, hitInformation.normal).normalized;

        if(Input.GetKey(KeyCode.LeftShift))
        {
            moveDir.x = movement.x * sprintSpeed;
            moveDir.z = movement.z * sprintSpeed;
        } else
        {
            moveDir.x = movement.x * moveSpeed;
            moveDir.z = movement.z * moveSpeed;
        }
        

        controller.Move(moveDir * Time.deltaTime);
    }

    private void UpdateRotation()
    {
        Vector3 val = Camera.main.transform.parent.forward;

        transform.forward = val;

    }
}
