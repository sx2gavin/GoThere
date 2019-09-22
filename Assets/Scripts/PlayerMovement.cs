using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    CharacterController characterController;
    float currentVerticalSpeed;
    bool movable = true;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rig == null)
        {
            return;
        }

        if (Input.GetAxis("Jump") > 0 && characterController.isGrounded)
        {
            Jump(jumpSpeed);
        }
        else
        {
            Fall();
        }

        Move();
    }

    public void KnockBack(Vector3 direction)
    {
        StartCoroutine(KnockBackCoroutine(direction));
    }

    private IEnumerator KnockBackCoroutine(Vector3 direction)
    {
        movable = false;
        Jump(knockBackHeight);
        while (!characterController.isGrounded)
        {
            var movementThisFrame = new Vector3(direction.x, 0, direction.z) * knockBackSpeed * Time.deltaTime;
            characterController.Move(movementThisFrame);
            yield return new WaitForEndOfFrame();
        }
        movable = true;
    }

    private void Jump(float jumpSpeed)
    {
        currentVerticalSpeed = jumpSpeed;
        characterController.Move(new Vector3(0, currentVerticalSpeed * Time.deltaTime, 0));
    }

    private void Fall()
    {
        if (characterController.isGrounded)
        {
            currentVerticalSpeed = 0.0f;
        }
        else
        {
            currentVerticalSpeed -= gravity * Time.deltaTime;
            characterController.Move(new Vector3(0, currentVerticalSpeed * Time.deltaTime, 0));
        }
    }

    private void Move()
    {
        if (movable && Time.timeScale > 0.0f)
        {
            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            if (movement.magnitude > 0.0f)
            {
                var direction = Quaternion.Euler(0, rig.transform.eulerAngles.y, 0);
                movement = direction * movement * speed * Time.deltaTime;
                characterController.Move(movement);
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
}
