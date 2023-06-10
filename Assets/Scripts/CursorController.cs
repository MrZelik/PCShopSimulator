using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UI;

public class CursorController : MonoBehaviour
{
    [SerializeField] private Image CursorImage;

    RaycastSystem RaycastSystem;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        RaycastSystem= GetComponent<RaycastSystem>();
    }

    private void Update()
    {
        ChangeCursorMode();

        RaycastHit hit;

        if (Physics.Raycast(RaycastSystem.ray, out hit, RaycastSystem.maxUsableDistance))
        {
            if (hit.collider.gameObject.GetComponent<CollectableItem>())
            {
                CheckCollectableIteComponent();
                return;
            }

            switch (hit.collider.gameObject.tag) 
            {
                case "Computer":
                case "PartConnector":
                case "Bed":
                case "SellPoint":
                    SetCursorGreenColor();
                    break;

                default:
                    SetCursorWhiteColor();
                    break;
            }   
        }
        else
        {
            SetCursorWhiteColor();
        }
    }

    private void ChangeCursorMode()
    {
        if (StateController.pcMode)
        {
            CursorImage.gameObject.SetActive(false);
            return;
        }
        else
        {
            CursorImage.gameObject.SetActive(true);
        }
    }

    private void CheckCollectableIteComponent()
    {
        SetCursorGreenColor();
    }

    private void SetCursorWhiteColor()
    {
        CursorImage.color = Color.white;
    }

    private void SetCursorGreenColor()
    {
        CursorImage.color = Color.green;
    }
}
