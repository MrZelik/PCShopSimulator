using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using System.Net.NetworkInformation;

public class AssemblingModeController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI assemblingModeText;

    private void Start()
    {    
        assemblingModeText.text = "B - ����� � ����� ������";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) 
            ChangeAssemblingMode();
    }

    public void ChangeAssemblingMode()
    {
        if (!StateController.assemblingMode)
        {
            assemblingModeText.text = "B - ����� �� ������ ������";
            StateController.assemblingMode = true;
        }
        else
        {
            assemblingModeText.text = "B - ����� � ����� ������";
            StateController.assemblingMode = false;
        }
    }
}
