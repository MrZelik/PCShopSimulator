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
            Debug.DrawLine(ray.origin, hit.point, Color.yellow);
            switch (hit.collider.gameObject.tag)
            {
                case "Computer":
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
                    break;

                case "PartConnector":
                    PressButtonText.gameObject.SetActive(true);
                    PressButtonText.text = "Press F";
                    interactionText.text = "Установить деталь";
                    break;

                case "Bed":
                    PressButtonText.gameObject.SetActive(true);
                    PressButtonText.text = "Press E";
                    interactionText.text = "Лечь спать";

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        DC.GoToSleep();
                    }
                    break;

                case "SellPoint":

                    if (Camera.main.transform.GetChild(0).gameObject.GetComponent<PCSellInfo>().pcAssembled)
                    {
                        PressButtonText.gameObject.SetActive(true);
                        PressButtonText.text = "Press E";
                        interactionText.text = "Выставить на продажу";

                        if (Input.GetKeyDown(KeyCode.E))
                        {
                           PUFSS.StartSale(hit.collider.gameObject);
                        }
                    }
                    break;

                case "PowerUnit":
                case "VideoCard":
                case "MotherBoard":
                case "RAM":
                case "CPU":
                case "Body":
                case "Storage":
                case "CPUFan":
                    PressButtonText.gameObject.SetActive(true);
                    PressButtonText.text = "Press E";
                    interactionText.text = "Подобрать";
                    break;

                default:
                    PressButtonText.gameObject.SetActive(false);
                    break;
            }
        }
        else
        {
            PressButtonText.gameObject.SetActive(false);
        }
    }
}
