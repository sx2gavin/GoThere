using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider), typeof (Animator))]
public class PlayerMovement : MonoBehaviour
{
    enum PlayerState
    {
        Grounded,
        Airborne,
        KnockBack,
        Climb
    }

    public float speed = 20f;
    public float additionalGravityForce = 10f;
    public float jumpSpeed = 30.0f;
    public float knockBackForce = 10.0f;
    public float groundCheckDistance = 0.01f;
    public float climbingSpeed = 10.0f;
    private Animator animator;

    [Tooltip("Camera rig determines the direction of the movement.")]
    public CameraRig rig;

    new Rigidbody rigidbody;
    PlayerState playerState = PlayerState.Grounded;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rig == null || Time.timeScale <= 0.0f)
        {
            return;
        }

        CheckIfGrounded();

        switch (playerState)
        {
            case PlayerState.Grounded:
                Move(speed);
                HandleJump(jumpSpeed);
                break;
            case PlayerState.Airborne:
                Move(speed);
                Airborne();
                break;
            case PlayerState.KnockBack:
                Airborne();
                break;
            case PlayerState.Climb:
                Climbing();
                break;
            default:
                break;
        }
    }

    private void Climbing()
    {
        var upDown = Input.GetAxis("Vertical") * climbingSpeed;
        rigidbody.velocity = Vector3.up * upDown;
        if (CheckIfGrounded() == true)
        {
            playerState = PlayerState.Grounded;
        }
    }

    private bool CheckIfGrounded()
    {
        int layerMask = 0;
        layerMask = ~layerMask; // check ground state with everything
        return Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, out RaycastHit hitInfo, 0.1f + groundCheckDistance, layerMask, QueryTriggerInteraction.Ignore);
    }

    public void KnockBack(Vector3 direction)
    {
        playerState = PlayerState.KnockBack;
        var knockBackDirection = direction * knockBackForce;
        LiftPlayerOffGround();
        rigidbody.velocity = knockBackDirection;
    }

    private void HandleJump(float jumpSpeed)
    {
        // Normal jump
        if (playerState == PlayerState.Grounded && Input.GetAxis("Jump") > 0.0f)
        {
            var newVelocity = rigidbody.velocity;
            newVelocity.y = jumpSpeed;
            rigidbody.velocity = newVelocity;
            playerState = PlayerState.Airborne;
        }

        // Cliff jump
        if (CheckIfGrounded() == false)
        {
            playerState = PlayerState.Airborne;
        }
    }

    private void Airborne()
    {
        rigidbody.AddForce(Vector3.down * additionalGravityForce);
        if (CheckIfGrounded())
        {
            playerState = PlayerState.Grounded;
        }
    }

    private void Move(float speed)
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (movement.magnitude > 0.0f)
        {
            var direction = Quaternion.Euler(0, rig.transform.eulerAngles.y, 0);
            movement = direction * movement * speed;
            var yAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yAngle, 0), 0.5f);
            animator.SetFloat("Speed", 1.0f);
        }
        else
        {
            animator.SetFloat("Speed", 0.0f);
        }

        movement.y = rigidbody.velocity.y;
        rigidbody.velocity = movement;
    }

    private void OnTriggerEnter(Collider other)
    {
        var ladder = other.GetComponent<Ladder>();
        if (ladder != null)
        {
            playerState = PlayerState.Climb;
            var ladderCenter = other.bounds.center;
            transform.position = new Vector3(ladderCenter.x, transform.position.y, ladderCenter.z);
            LiftPlayerOffGround();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var ladder = other.GetComponent<Ladder>();
        if (ladder != null && playerState != PlayerState.Grounded)
        {
            playerState = PlayerState.Climb;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var ladder = other.GetComponent<Ladder>();
        if (ladder != null)
        {
            playerState = PlayerState.Airborne;
        }
    }

    private void LiftPlayerOffGround()
    {
        transform.Translate(Vector3.up * (groundCheckDistance + 0.001f));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position + Vector3.up * 0.1f, transform.position + Vector3.up * 0.1f + Vector3.down * groundCheckDistance);
    }
}
