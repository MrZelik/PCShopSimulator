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

    public void HideCursor()
    {
        CursorImage.gameObject.SetActive(false);
    }

    public void ShowCursor()
    {
        CursorImage.gameObject.SetActive(true);
    }

    public void SetCursorWhiteColor()
    {
        CursorImage.color = Color.white;
    }

    public void SetCursorGreenColor()
    {
        CursorImage.color = Color.green;
    }
}
