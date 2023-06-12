using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] private Camera carCamera;
    [SerializeField] private Camera playerCamera;
    [SerializeField] Transform PlayerExitPos;

    public static bool driveMode = false;

    Rigidbody Rigidbody;

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();

        playerCamera = GetComponent<Camera>();
        playerCamera = Camera.main;

        driveMode = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && driveMode)
        {
            ExitDriveMode();
        }
    }

    public void EnterDriveMode()
    {
        print("12");
        driveMode = true;
        playerCamera.enabled = false;
        carCamera.enabled = true;

        Player.SetActive(false);
    }

    public void ExitDriveMode()
    {
        print("321");
        driveMode = false;
        carCamera.enabled = false;
        playerCamera.enabled = true;

        Player.transform.position = PlayerExitPos.position;
        Player.SetActive(true);

        Rigidbody.velocity = Vector3.zero;
    }
}
