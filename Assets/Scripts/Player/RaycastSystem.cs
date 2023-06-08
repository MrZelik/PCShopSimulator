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

    private void Update()
    {
        ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, maxUsableDistance))
        {
            switch (hit.collider.gameObject.tag)
            {
                case "Computer":
                    PressButtonText.gameObject.SetActive(true);
                    PressButtonText.text = "Press E";
                    interactionText.text = "������������ ���������";

                    if (Input.GetKeyDown(KeyCode.E) && !StateController.pcMode)
                    {
                        ComputerControllerGO.GetComponent<ComputerController>().ChangePcMode();
                        PressButtonText.gameObject.SetActive(false);
                    }
                    else if (Input.GetKeyDown(KeyCode.Escape) && StateController.pcMode)
                    {
                        ComputerControllerGO.GetComponent<ComputerController>().ChangePcMode();
                    }
                    break;

                case "PartConnector":
                    PressButtonText.gameObject.SetActive(true);
                    PressButtonText.text = "Press F";
                    interactionText.text = "���������� ������";
                    break;

                case "Bed":
                    PressButtonText.gameObject.SetActive(true);
                    PressButtonText.text = "Press E";
                    interactionText.text = "���� �����";

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Controller.GetComponent<DayController>().GoToSleep();
                    }
                    break;

                case "SellPoint":

                    if (Camera.main.transform.GetChild(0).gameObject.GetComponent<PCSellInfo>().pcAssembled)
                    {
                        PressButtonText.gameObject.SetActive(true);
                        PressButtonText.text = "Press E";
                        interactionText.text = "��������� �� �������";

                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            Controller.GetComponent<PutUpForSaleSystem>().StartSale(hit.collider.gameObject);
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
                    PressButtonText.gameObject.SetActive(true);
                    PressButtonText.text = "Press E";
                    interactionText.text = "���������";
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
