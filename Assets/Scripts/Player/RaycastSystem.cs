using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RaycastSystem : MonoBehaviour
{
    public float maxUsableDistance;
    public TextMeshProUGUI PressButtonText;
    [SerializeField] private GameObject ComputerControllerGO;
    [SerializeField] private TextMeshProUGUI interactionText;
    [SerializeField] private GameObject Controller;

    public Ray ray;

    ComputerController CC;
    DayController DC;
    PutUpForSaleSystem PUFSS;

    private void Start()
    {
        CC = ComputerControllerGO.GetComponent<ComputerController>();
        DC = Controller.GetComponent<DayController>();
        PUFSS = Controller.GetComponent<PutUpForSaleSystem>();
    }

    private void Update()
    {
        ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, maxUsableDistance))
        {
            if (hit.collider.gameObject.GetComponent<CollectableItem>())
            {
                CheckCollectableIteComponent();
                return;
            }

            switch (hit.collider.gameObject.tag)
            {
                case "Computer":
                    ShowComputerControllInfo();
                    break;

                case "PartConnector":
                    ShowPartConnectorControllInfo();
                    break;

                case "Bed":
                    ShowBedControllInfo();
                    break;

                case "SellPoint":
                    ShowSellPointControllInfo(hit.collider.gameObject);     
                    break;

                default:
                    HideControllInfo();
                    break;
            }
        }
        else
        {
            HideControllInfo();
        }
    }

    private void CheckCollectableIteComponent()
    {
        PressButtonText.gameObject.SetActive(true);
        PressButtonText.text = "Press E";
        interactionText.text = "Подобрать";
    }

    private void ShowComputerControllInfo()
    {
        PressButtonText.gameObject.SetActive(true);
        PressButtonText.text = "Press E";
        interactionText.text = "Использовать компьютер";

        if (Input.GetKeyDown(KeyCode.E) && !StateController.pcMode)
        {
            CC.ChangePcMode();
            PressButtonText.gameObject.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && StateController.pcMode)
        {
            CC.ChangePcMode();
        }
    }

    private void ShowPartConnectorControllInfo()
    {
        PressButtonText.gameObject.SetActive(true);
        PressButtonText.text = "Press F";
        interactionText.text = "Установить деталь";
    }

    private void ShowBedControllInfo()
    {
        PressButtonText.gameObject.SetActive(true);
        PressButtonText.text = "Press E";
        interactionText.text = "Лечь спать";

        if (Input.GetKeyDown(KeyCode.E))
        {
            DC.GoToSleep();
        }
    }

    private void ShowSellPointControllInfo(GameObject hit)
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

    private void HideControllInfo()
    {
        PressButtonText.gameObject.SetActive(false);
    }
}
