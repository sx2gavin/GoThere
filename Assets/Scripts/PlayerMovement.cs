using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _speed = 10.0f;
    [SerializeField] float _gravity = 9.8f;
    [SerializeField] float _jumpSpeed = 10.0f;
    [SerializeField] CameraRig _rig; // Camera rig determines the direction of the movement.

    CharacterController _characterController;
    float _currentVerticalSpeed = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        Move();
    }

    private void Jump()
    {
        if (_characterController.isGrounded)
        {
            _currentVerticalSpeed = 0.0f;
            if (Input.GetButton("Jump"))
            {
                _currentVerticalSpeed = _jumpSpeed;
            }
        }
        else
        {
            _currentVerticalSpeed -= _gravity * Time.deltaTime;
        }

        _characterController.Move(new Vector3(0, _currentVerticalSpeed * Time.deltaTime, 0));

    }

    private void Move()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (movement.magnitude > 0.0f)
        {
            var direction = Quaternion.Euler(0, _rig.transform.eulerAngles.y, 0);
            movement = direction * movement * _speed * Time.deltaTime;
            _characterController.Move(movement);
            var yAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yAngle, 0), 0.5f);
        }
    }
}
