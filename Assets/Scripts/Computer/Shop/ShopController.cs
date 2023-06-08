using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    [SerializeField] GameObject StartPanel;
    [SerializeField] GameObject UsedPartsPanel;
    [SerializeField] GameObject NewPartsPanel;

    public void OpenUsedPartsPanel()
    {
        StartPanel.SetActive(false);
        UsedPartsPanel.SetActive(true);
    }

    public void OpenNewPartsPanel()
    {
        StartPanel.SetActive(false);
        NewPartsPanel.SetActive(true);
    }

    public void BackToStartPanel()
    {
        UsedPartsPanel.SetActive(false);
        StartPanel.SetActive(true);
    }
}
