using System;
using TMPro;
using UnityEngine;

public class ControllInfoController : MonoBehaviour
{
    public TextMeshProUGUI PressButtonText;
    [SerializeField] private GameObject ComputerControllerGO;
    [SerializeField] private TextMeshProUGUI interactionText;
    [SerializeField] private GameObject Controller;
    [SerializeField] private GameObject SellSlotGo;

    ComputerController CC;
    DayController DC;
    SellSlot SellSlot;

    public static Action OpenCarBackDoor;
    public static Action CloseCarBackDoor;

    private void Start()
    {
        CC = ComputerControllerGO.GetComponent<ComputerController>();
        DC = Controller.GetComponent<DayController>();
        SellSlot =SellSlotGo.GetComponent<SellSlot>();
    }

    private void Update()
    {
        if (CarController.driveMode)
        {
            HideControllInfo();
        }
    }

    public void ShowCollectableControllInfo()
    {
        PressButtonText.gameObject.SetActive(true);
        PressButtonText.text = "Press E";
        interactionText.text = "Подобрать";
    }

    public void ShowControllersControllInfo()
    {
        PressButtonText.gameObject.SetActive(true);
        PressButtonText.text = "Press E";
        interactionText.text = "Использовать";    
    }

    public void ShowSlotsControllInfo()
    {
        PressButtonText.gameObject.SetActive(true);
        PressButtonText.text = "Press F";
        interactionText.text = "Установить";
    }

    public void HideControllInfo()
    {
        PressButtonText.gameObject.SetActive(false);
    }
}
