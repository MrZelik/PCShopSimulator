using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class ControllInfoController : MonoBehaviour
{
    public TextMeshProUGUI PressButtonText;
    [SerializeField] private GameObject ComputerControllerGO;
    [SerializeField] private TextMeshProUGUI interactionText;
    [SerializeField] private GameObject Controller;

    ComputerController CC;
    DayController DC;
    PutUpForSaleSystem PUFSS;

    public static Action OpenCarBackDoor;
    public static Action CloseCarBackDoor;

    private void Start()
    {
        CC = ComputerControllerGO.GetComponent<ComputerController>();
        DC = Controller.GetComponent<DayController>();
        PUFSS = Controller.GetComponent<PutUpForSaleSystem>();
    }

    private void Update()
    {
        if (CarController.driveMode)
        {
            HideControllInfo();
        }
    }

    public void CheckCollectableIteComponent()
    {
        PressButtonText.gameObject.SetActive(true);
        PressButtonText.text = "Press E";
        interactionText.text = "Подобрать";
    }

    public void ShowComputerControllInfo()
    {
        PressButtonText.gameObject.SetActive(true);
        PressButtonText.text = "Press E";
        interactionText.text = "Использовать компьютер";

        if (Input.GetKeyDown(KeyCode.E) && !StateController.pcMode)
        {
            CC.ChangePcMode();
            PressButtonText.gameObject.SetActive(false);
        }
        else if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E)) && StateController.pcMode)
        {
            CC.ChangePcMode();
        }
    }

    public void ShowPartConnectorControllInfo()
    {
        PressButtonText.gameObject.SetActive(true);
        PressButtonText.text = "Press F";
        interactionText.text = "Установить деталь";
    }

    public void ShowBedControllInfo()
    {
        PressButtonText.gameObject.SetActive(true);
        PressButtonText.text = "Press E";
        interactionText.text = "Лечь спать";

        if (Input.GetKeyDown(KeyCode.E))
        {
            DC.GoToSleep();
        }
    }

    public void ShowSellPointControllInfo(GameObject hit)
    {
        if (PCAssembler.CanSell)
        {
            PressButtonText.gameObject.SetActive(true);
            PressButtonText.text = "Press E";
            interactionText.text = "Выставить на продажу";

            if (Input.GetKeyDown(KeyCode.E))
            {
                PUFSS.StartSale(hit);
            }
        }
    }

    public void ShowCarControllInfo(GameObject hit)
    {
        PressButtonText.gameObject.SetActive(true);
        PressButtonText.text = "Press E";
        interactionText.text = "Сесть в машину";

        if (Input.GetKeyDown(KeyCode.E))
        {
            hit.transform.parent.GetComponent<CarController>().EnterDriveMode();
        }
    }

    public void ShowCarBackDoorControllInfo()
    {
        PressButtonText.gameObject.SetActive(true);
        PressButtonText.text = "Press E";
        interactionText.text = "Использовать багажник";

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!BackDoorOpener.doorOpen)
                OpenCarBackDoor?.Invoke();
            else
                CloseCarBackDoor?.Invoke();
        }
    }

    public void HideControllInfo()
    {
        PressButtonText.gameObject.SetActive(false);
    }
}
