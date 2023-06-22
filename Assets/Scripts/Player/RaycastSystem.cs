using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Experimental.GraphView;

public class RaycastSystem : MonoBehaviour
{
    public float maxUsableDistance;
    
    public Ray ray;

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
        

        if (CarController.driveMode)
        {
            cursorController.HideCursor();
            controllInfoController.HideControllInfo();
            return;
        }

        Raycast();
    }

    private void Raycast()
    {
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxUsableDistance, 1 << 8 | 1 << 9 | 1 << 10 | 1 << 11))
        {
            if (hit.collider.TryGetComponent(out ICollectable collectable))
            {
                controllInfoController.ShowCollectableControllInfo();
                cursorController.SetCursorGreenColor();

                if (Input.GetKeyDown(KeyCode.E) && itemCollector.Part == null)
                {
                    collectable.Interact();
                }
                return;
            }
            else if (hit.collider.TryGetComponent(out IController controller))
            {
                controllInfoController.ShowControllersControllInfo();
                cursorController.SetCursorGreenColor();

                if (Input.GetKeyDown(KeyCode.E))
                {
                    controller.Interact();
                }
                return;
            }
            else if(hit.collider.transform.parent.TryGetComponent(out ISlot slot))
            {
                controllInfoController.ShowSlotsControllInfo();
                cursorController.SetCursorGreenColor();

                if (Input.GetKeyDown(KeyCode.F))
                {
                    slot.Interact(hit.collider.gameObject);
                }
                return;
            }
        }
        else
        {
            controllInfoController.HideControllInfo();
            cursorController.SetCursorWhiteColor();
        }
    }
}
