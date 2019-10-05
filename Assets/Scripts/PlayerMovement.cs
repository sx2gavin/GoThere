using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 20f;
    public float additionalGravityForce = 10f;
    public float jumpSpeed = 30.0f;
    public float knockBackHeight;
    public float knockBackSpeed;
    public float groundCheckDistance = 0.1f;
    public Animator animator;

    [Tooltip("Camera rig determines the direction of the movement.")]
    public CameraRig rig;

    new Rigidbody rigidbody;
    bool movable = true;
    bool isClimbing = false;
    bool isGrounded = true;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rig == null)
        {
            return;
        }

        CheckGroundState();
        Airborne(jumpSpeed);
        Walk();
    }

    public void KnockBack(Vector3 direction)
    {
        var knockBackDirection = direction * knockBackSpeed;
        rigidbody.AddForce(knockBackDirection, ForceMode.Impulse);
    }


    private void Airborne(float jumpSpeed)
    {
        if (isGrounded && Input.GetAxis("Jump") > 0.0f)
        {
            var newVelocity = rigidbody.velocity;
            newVelocity.y = jumpSpeed;
            rigidbody.velocity = newVelocity;
        }
        else
        {
            rigidbody.AddForce(Vector3.down * additionalGravityForce);
        }
    }

    private void Walk()
    {
        Vector3 movement = Vector3.zero;
        if (movable && Time.timeScale > 0.0f)
        {
            movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
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
            isClimbing = true;
        }
    }

    private void CheckGroundState()
    {
        int layerMask = 0;
        layerMask = ~layerMask; // check ground state with everything
        if (Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, out RaycastHit hitInfo, groundCheckDistance, layerMask, QueryTriggerInteraction.Ignore))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position + Vector3.up * 0.1f, transform.position + Vector3.up * 0.1f + Vector3.down * groundCheckDistance);
    }
}
