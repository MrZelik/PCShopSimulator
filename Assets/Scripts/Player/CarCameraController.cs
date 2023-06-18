using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCameraController : MonoBehaviour
{
    public float mouseSensitivity = 2f;
    private Camera carCamera;
    private float verticalRotation = 0f;
    private float horizontalRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        carCamera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CarController.driveMode)
        {
            mouseSensitivity = 2f;
        }
        else
        {
            horizontalRotation = 0;
            verticalRotation = 0;
            mouseSensitivity = 0f;
        }

        horizontalRotation += Input.GetAxis("Mouse X") * mouseSensitivity;
        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        
        carCamera.transform.localRotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0f);
    }
}
