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
        Jumping,
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
    public Vector3 throwDirection = new Vector3(0, 1f, 1f);
    public float throwSpeed = 5f;
    public Vector3 itemLocalPosition = new Vector3(0, 1f, -2f);
    private Animator animator;

    [Tooltip("Camera rig determines the direction of the movement.")]
    public CameraRig rig;

    private new Rigidbody rigidbody;
    private PlayerState playerState = PlayerState.Grounded;

    private Pickup pickup;
    private bool isHoldingPickup = false;

    public void KnockBack(Vector3 direction)
    {
        playerState = PlayerState.KnockBack;
        var knockBackDirection = direction * knockBackForce;
        LiftPlayerOffGround();
        rigidbody.velocity = knockBackDirection;
    }

    public void StartJump()
    {
        var newVelocity = rigidbody.velocity;
        newVelocity.y = jumpSpeed;
        rigidbody.velocity = newVelocity;
        playerState = PlayerState.Jumping;
    }

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

        HandleAction();

        switch (playerState)
        {
            case PlayerState.Grounded:
                Move(speed);
                HandleJump();
                HandleFall();
                break;
            case PlayerState.Jumping:
                HandleFall();
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

    private void HandleAction()
    {
        if (Input.GetButtonDown("Interact"))
        {
            Debug.Log("Handle Interact");
            if (pickup != null)
            {
                GameObject pickupObject = pickup.gameObject;
                if (!isHoldingPickup)
                {
                    pickup.PickedUp();
                    pickupObject.transform.SetParent(transform);
                    pickupObject.transform.localPosition = itemLocalPosition;
                    pickupObject.transform.localRotation = Quaternion.identity;
                    isHoldingPickup = true;
                }
                else
                {
                    pickupObject.transform.SetParent(null);
                    pickup.Dropped();
                    pickup.GetComponent<Rigidbody>().AddForce(rigidbody.velocity + transform.rotation * throwDirection * throwSpeed, ForceMode.VelocityChange);
                    isHoldingPickup = false;
                }
            }
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

    private void HandleJump()
    {
        // Normal jump
        if (playerState == PlayerState.Grounded && Input.GetButtonDown("Jump"))
        {
            animator.SetTrigger("Jump");
        }
    }

    private void HandleFall()
    {
        if (CheckIfGrounded() == false)
        {
            playerState = PlayerState.Airborne;
            animator.SetBool("InAir", true);
        }
    }

    private void Airborne()
    {
        rigidbody.AddForce(Vector3.down * additionalGravityForce);
        if (CheckIfGrounded())
        {
            animator.SetBool("InAir", false);
            playerState = PlayerState.Grounded;
        }
    }

    private void Move(float movementSpeed)
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (movement.magnitude > 0.0f)
        {
            animator.SetFloat("Speed", movement.magnitude);
            var direction = Quaternion.Euler(0, rig.transform.eulerAngles.y, 0);
            movement = direction * movement * movementSpeed;
            var yAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yAngle, 0), 0.5f);
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

        var pickupItem = other.GetComponent<Pickup>();
        if (pickupItem != null)
        {
            if (!isHoldingPickup)
            {
                Debug.Log("Enter pickup");
                pickup = pickupItem;
            }
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

        var pickupItem = other.GetComponent<Pickup>();
        if (pickupItem != null)
        {
            if (!isHoldingPickup)
            {
                Debug.Log("Exit pickup");
                pickup = null;
            }
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
