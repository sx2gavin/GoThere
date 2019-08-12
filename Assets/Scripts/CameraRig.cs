using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRig : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The length of the rig, the longer the rig, the further the camera to the object.")]
    float _rigLength = 10f;

    [SerializeField]
    [Tooltip("Initial eular angle for the camera rig.")]
    Vector3 _initialEular = Vector3.zero;

    [SerializeField]
    [Tooltip("The object which the camera follows.")]
    GameObject _attachedObject;

    [SerializeField]
    [Tooltip("Offset of the rig anchor point on the object from the center of the object.")]
    Vector3 _anchorPointOffset = Vector3.zero;

    [SerializeField]
    [Tooltip("How smooth the camera response when the object moves.")]
    [Range(0, 1)] float _cameraMoveResponse = 0.5f;

    [SerializeField]
    [Tooltip("The speed of rotation.")]
    float _rotationSpeed = 10f;

    [SerializeField]
    [Range(0, 1)]
    float _cameraRotateResponse = 0.5f; 

    Camera _mainCamera;
    Vector3 _eularAngles = Vector3.zero;

    public void Detach()
    {
        _attachedObject = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = GetComponentInChildren<Camera>();

        // make the camera 10 units back from the origin.
        _mainCamera.transform.localPosition = new Vector3(0, 0, -_rigLength);
        _eularAngles = _initialEular;

        Move();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
        CheckForGround();
    }

    private void CheckForGround()
    {
        var onlyGroundLayerMask = 1 << LayerMask.NameToLayer("Ground");

        var direction = _mainCamera.transform.position - transform.position;
        if (Physics.Raycast(transform.position, direction, out RaycastHit hitInfo, _rigLength, onlyGroundLayerMask))
        {

            _mainCamera.transform.localPosition = new Vector3(0, 0, -hitInfo.distance + 1);
        }
        else
        {
            _mainCamera.transform.localPosition = new Vector3(0, 0, -_rigLength);
        }
    }

    private void Move()
    {
        if (_attachedObject)
        {
            transform.position = Vector3.Lerp(transform.position, _attachedObject.transform.position + _anchorPointOffset, _cameraMoveResponse);
        }
    }

    private void Rotate()
    {
        Vector3 rotateAmount = new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
        _eularAngles += rotateAmount * _rotationSpeed;
        _eularAngles.x = Mathf.Clamp(_eularAngles.x, -45f, 75f);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(_eularAngles), _cameraRotateResponse);
    }
}
