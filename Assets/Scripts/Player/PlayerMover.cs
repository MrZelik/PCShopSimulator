using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public float movementSpeed = 5f;   
    public float mouseSensitivity = 2f;   

    private CharacterController characterController;
    private Camera playerCamera;
    private float verticalRotation = 0f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();
    }

    void Update()
    {
        if (StateController.canMove)
        {
            movementSpeed = 5;
            mouseSensitivity = 2;
        }
        else
        {
            movementSpeed = 0;
            mouseSensitivity = 0;
        }

        // Поворот персонажа по горизонтали
        float horizontalRotation = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0f, horizontalRotation, 0f);

        // Поворот камеры по вертикали
        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

        // Движение персонажа
        float forwardSpeed = Input.GetAxis("Vertical") * movementSpeed;
        float sideSpeed = Input.GetAxis("Horizontal") * movementSpeed;
        Vector3 speed = new Vector3(sideSpeed, 0, forwardSpeed);
        speed = transform.rotation * speed;
        characterController.SimpleMove(speed);
    }
}
