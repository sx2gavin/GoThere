using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 20f;
    public float gravity = 50f;
    public float jumpSpeed = 30.0f;
    public float knockBackHeight;
    public float knockBackSpeed;
    public Animator animator;

    [Tooltip("Camera rig determines the direction of the movement.")]
    public CameraRig rig;

    new Rigidbody rigidbody;
    float currentVerticalSpeed;
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

        if (Input.GetAxis("Jump") > 0 && isGrounded)
        {
            Jump(jumpSpeed);
        }

        Walk();
    }

    public void KnockBack(Vector3 direction)
    {
        StartCoroutine(KnockBackCoroutine(direction));
    }

    private IEnumerator KnockBackCoroutine(Vector3 direction)
    {
        movable = false;
        // Jump(knockBackHeight);
        while (!isGrounded)
        {
            var movementThisFrame = new Vector3(direction.x, 0, direction.z) * knockBackSpeed * Time.deltaTime;
            // characterController.Move(movementThisFrame);
            transform.localPosition += movementThisFrame;
            yield return new WaitForEndOfFrame();
        }
        movable = true;
    }


    private void Jump(float jumpSpeed)
    {
        currentVerticalSpeed = jumpSpeed;
        rigidbody.AddForce(new Vector3(0, jumpSpeed, 0), ForceMode.Impulse);
    }

    private void Walk()
    {
        if (movable && Time.timeScale > 0.0f)
        {
            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            if (movement.magnitude > 0.0f)
            {
                var direction = Quaternion.Euler(0, rig.transform.eulerAngles.y, 0);
                movement = direction * movement * speed * Time.deltaTime;
                rigidbody.AddForce(movement, ForceMode.VelocityChange);
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
    }

    private void OnTriggerEnter(Collider other)
    {
        var ladder = other.GetComponent<Ladder>();
        if (ladder != null)
        {
            isClimbing = true;
        }
    }
}
