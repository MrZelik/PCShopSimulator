using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PCAssembler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI assemblingModeText;

    public static bool CanSell = false;

    private void Start()
    {
        CanSell = false;
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
