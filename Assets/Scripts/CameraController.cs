using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouseMovement= new Vector2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));
        Vector3 eulerAngles = transform.rotation.eulerAngles;
        eulerAngles.x -= mouseMovement.x;
        eulerAngles.y += mouseMovement.y;
        transform.rotation = Quaternion.Euler(eulerAngles);

        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        movement = transform.rotation * movement;
        transform.position += movement * Time.deltaTime * cameraSpeed;
    }
}
