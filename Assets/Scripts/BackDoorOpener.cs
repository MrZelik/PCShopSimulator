using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class BackDoorOpener : MonoBehaviour, IController
{
    [SerializeField] GameObject CarRoof;
    [SerializeField] Vector3 OpenPosition;
    [SerializeField] Quaternion OpenRotation;

    private Vector3 stockPosition;
    private Quaternion stockRotation;

    public static bool doorOpen = false;

    Collider Collider;
    Collider RoofCollider;

    private void Start()
    {
        Collider = GetComponent<Collider>();
        RoofCollider = CarRoof.GetComponent<Collider>();

        doorOpen = false;
        stockPosition = transform.localPosition;
        stockRotation = transform.localRotation;
    }

    public void Interact()
    {
        if(doorOpen)
            CloseDoor();
        else
            OpenDoor();
    }

    private void OnEnable()
    {
        ControllInfoController.OpenCarBackDoor += OpenDoor;
        ControllInfoController.CloseCarBackDoor += CloseDoor;
    }

    private void OnDisable()
    {
        ControllInfoController.OpenCarBackDoor -= OpenDoor;
        ControllInfoController.CloseCarBackDoor -= CloseDoor;
    }

    private void OpenDoor()
    {
        Collider.isTrigger = true;
        RoofCollider.isTrigger = true;
        transform.localPosition = OpenPosition;
        transform.localRotation = OpenRotation;
        doorOpen = true;
    }

    private void CloseDoor()
    {
        Collider.isTrigger = false;
        RoofCollider.isTrigger = false;
        transform.localPosition = stockPosition;
        transform.localRotation = stockRotation;
        doorOpen = false;
    }
}
