using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _speed = 0.3f;
    [SerializeField] float _gravity = 9.8f;
    [SerializeField] CameraRig _rig; // Camera rig determines the direction of the movement.

    CharacterController _characterController;
    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (movement.magnitude > 0.0f)
        {
            var direction = Quaternion.Euler(0, _rig.transform.eulerAngles.y, 0);
            movement = direction * movement * _speed;
            _characterController.Move(movement);
            var yAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yAngle, 0), 0.5f);

        }
    }
}
