using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ComputerController : MonoBehaviour
{
    [SerializeField] private GameObject MainCanvas;

    public void ChangePcMode()
    {
        if (!StateController.pcMode)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            StateController.pcMode = true;
            StateController.canMove = false;
            MainCanvas.SetActive(false);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            StateController.pcMode = false;
            StateController.canMove = true;
            MainCanvas.SetActive(true);
        }
    }
}
