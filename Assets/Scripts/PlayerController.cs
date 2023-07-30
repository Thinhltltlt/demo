using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Weapon weapon;
    [SerializeField] private PlayerInputHandler inputHandler;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private float springSpeed = 20;
    [SerializeField] private float jumpForce = 1;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckRadius = 1;
    [SerializeField] private float groundCheckYOffset = 0;
    
    private bool isGrounded;
    
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        inputHandler = GetComponent<PlayerInputHandler>();
        weapon = GetComponent<Weapon>();
    }

    private void Update()
    {
        UpdateMoving();
        CheckGrounded();
        if (inputHandler.Jump && isGrounded)
        {
            inputHandler.Jump = false;
            Jump();
        }

        if (inputHandler.Attack)
        {
            Attack();
        }
    }

    private void UpdateMoving()
    {
        var movingSpeed = moveSpeed;
        if (inputHandler.Spring)
            movingSpeed = springSpeed;
        
        var moveDirection = inputHandler.MoveDirection;
        var velocity = new Vector3(moveDirection.x, 0, moveDirection.y);
        
        if (velocity.magnitude > 0)
            transform.rotation = Quaternion.LookRotation(velocity);
        
        velocity *= movingSpeed;
        velocity.y = rigidbody.velocity.y;
        rigidbody.velocity = velocity;
        
        animator.SetFloat("Speed", velocity.magnitude);
    }

    private void Jump()
    {
        rigidbody.AddForce(new Vector3(0, jumpForce, 0), 
                ForceMode.Impulse);
    }

    private void CheckGrounded()
    {
        isGrounded = Physics.CheckSphere(GetGroundCheckPos(), 
            groundCheckRadius, groundLayer);
        animator.SetBool("Jump", !isGrounded);
    }

    private Vector3 GetGroundCheckPos()
    {
        var pos = transform.position;
        pos.y += groundCheckYOffset;
        return pos;
    }

    private void Attack()
    {
        weapon.Attack();
    }
    
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        var pos = GetGroundCheckPos();
        
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(pos, groundCheckRadius);
    }
#endif
}
