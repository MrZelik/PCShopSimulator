using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UI;

public class CursorController : MonoBehaviour
{
    [SerializeField] private Image CursorImage;
    public CursorMode cursorMode = CursorMode.Auto;

    RaycastSystem RaycastSystem;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        RaycastSystem= GetComponent<RaycastSystem>();
    }

    private void Update()
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

        RaycastHit hit;

        if (Physics.Raycast(RaycastSystem.ray, out hit, RaycastSystem.maxUsableDistance))
        {
            switch (hit.collider.gameObject.tag) 
            {
                case "PowerUnit":
                case "VideoCard":
                case "MotherBoard":
                case "RAM":
                case "CPU":
                case "Body":
                case "Storage":
                case "Computer":
                case "PartConnector":
                case "Bed":
                case "SellPoint":
                    CursorImage.color = Color.green;
                    break;

                default:
                    CursorImage.color = Color.white;
                    break;
            }
        }
        else
        {
            CursorImage.color = Color.white;
        }
    }
}
