using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ComputerController : MonoBehaviour
{
    [SerializeField] private GameObject ComputerScreen;

    public void ChangePcMode()
    {
        if (!StateController.pcMode)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            StateController.pcMode = true;
            StateController.canMove = false;
            ComputerScreen.SetActive(true);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            StateController.pcMode = false;
            StateController.canMove = true;
            ComputerScreen.SetActive(false);
        }
    }
}
