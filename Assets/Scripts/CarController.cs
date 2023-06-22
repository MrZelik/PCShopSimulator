using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditor.Animations;
using UnityEngine;

public class CarController : MonoBehaviour, IController
{
    [SerializeField] GameObject Player;
    [SerializeField] private Camera carCamera;
    [SerializeField] private Camera playerCamera;
    [SerializeField] Transform PlayerExitPos;
    [SerializeField] GameObject cursorControllerGO;

    public static bool driveMode = false;

    Rigidbody Rigidbody;
    CursorController cursorController;
    ControllInfoController controllInfoController;

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        playerCamera = GetComponent<Camera>();
        playerCamera = Camera.main;
        cursorController = playerCamera.gameObject.GetComponent<CursorController>();
        controllInfoController = playerCamera.gameObject.GetComponent<ControllInfoController>();
        driveMode = false;
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E) && driveMode)
        {
            ExitDriveMode();
        }    
    }

    public void Interact()
    {
        EnterDriveMode();
    }

    public void EnterDriveMode()
    {
        driveMode = true;
        StateController.canMove = false;

        cursorController.HideCursor();
        controllInfoController.HideControllInfo();
        playerCamera.enabled = false;
        carCamera.enabled = true;

        Player.SetActive(false);
    }

    public void ExitDriveMode()
    {
        StateController.canMove = true;

        driveMode = false;

        cursorController.ShowCursor();
        controllInfoController.HideControllInfo();
        
        carCamera.enabled = false;
        playerCamera.enabled = true;

        Player.transform.position = PlayerExitPos.position;
        Player.SetActive(true);

        Rigidbody.velocity = Vector3.zero;
    }
}
