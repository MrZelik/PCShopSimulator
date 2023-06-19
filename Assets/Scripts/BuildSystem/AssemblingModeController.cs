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
        assemblingModeText.text = "B - войти в режим сборки";
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
            assemblingModeText.text = "B - выйти из режима сборки";
            StateController.assemblingMode = true;
        }
        else
        {
            assemblingModeText.text = "B - войти в режим сборки";
            StateController.assemblingMode = false;
        }
    }
}
