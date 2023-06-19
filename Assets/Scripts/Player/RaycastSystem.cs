using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Experimental.GraphView;

public class RaycastSystem : MonoBehaviour
{
    public static float maxUsableDistance = 3;
    
    public static Ray ray;

    ControllInfoController controllInfoController;
    CursorController cursorController;
    ItemCollector itemCollector;

    private void Start()
    {
        controllInfoController = GetComponent<ControllInfoController>();
        cursorController = GetComponent<CursorController>();
        itemCollector = GetComponent<ItemCollector>();
    }

    private void LateUpdate()
    {
        ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (CarController.driveMode)
        {
            cursorController.HideCursor();
            controllInfoController.HideControllInfo();
            return;
        }

        if (Physics.Raycast(ray, out hit, maxUsableDistance, 1 << 8 | 1 << 9 | 1 << 10 | 1 << 11))
        {
            if (hit.collider.gameObject.GetComponent<CollectableItem>())
            {
                controllInfoController.CheckCollectableIteComponent();
                cursorController.SetCursorGreenColor();

                if (Input.GetKeyDown(KeyCode.E) && ItemCollector.Part == null)
                {
                    itemCollector.TakePart(hit.collider.gameObject);
                }
                return;
            }

            switch (hit.collider.gameObject.tag)
            {
                case "Computer":
                    controllInfoController.ShowComputerControllInfo();
                    cursorController.SetCursorGreenColor();
                    break;

                case "PartConnector":
                    controllInfoController.ShowPartConnectorControllInfo();
                    cursorController.SetCursorGreenColor();
                    break;

                case "Bed":
                    controllInfoController.ShowBedControllInfo();
                    cursorController.SetCursorGreenColor();
                    break;

                case "SellPoint":
                    controllInfoController.ShowSellPointControllInfo(hit.collider.gameObject);
                    cursorController.SetCursorGreenColor();
                    break;

                case "Car":
                    controllInfoController.ShowCarControllInfo(hit.collider.gameObject);
                    cursorController.SetCursorGreenColor();
                    break;

                case "CarBackDoor":
                    controllInfoController.ShowCarBackDoorControllInfo();
                    cursorController.SetCursorGreenColor();
                    break;

                default:
                    controllInfoController.HideControllInfo();
                    cursorController.SetCursorWhiteColor();
                    break;
            }
        }
        else
        {
            controllInfoController.HideControllInfo();
            cursorController.SetCursorWhiteColor();
        }
    }
}
