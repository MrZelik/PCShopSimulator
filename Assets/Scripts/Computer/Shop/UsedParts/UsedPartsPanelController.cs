using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsedPartsPanelController : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] GameObject PowerUnitsPanel;
    [SerializeField] GameObject MotherBoardsPanel;
    [SerializeField] GameObject RamsPanel;
    [SerializeField] GameObject VideoCardsPanel;
    [SerializeField] GameObject StoragesPanel;
    [SerializeField] GameObject CPUsPanel;
    [SerializeField] GameObject CPUFansPanel;
    [SerializeField] GameObject BodysPanel;

    public void ClosePartPanels()
    {
        PowerUnitsPanel.SetActive(false);
        MotherBoardsPanel.SetActive(false);
        RamsPanel.SetActive(false);
        VideoCardsPanel.SetActive(false);
        StoragesPanel.SetActive(false);
        CPUsPanel.SetActive(false);
        CPUFansPanel.SetActive(false);
        BodysPanel.SetActive(false);
    }

    public void OpenPowerUnitsPanel()
    {
        ClosePartPanels();
        PowerUnitsPanel.SetActive(true);
    }

    public void OpenMotherBoardsPanel()
    {
        ClosePartPanels();
        MotherBoardsPanel.SetActive(true);
    }

    public void OpenRamsPanel()
    {
        ClosePartPanels();
        RamsPanel.SetActive(true);
    }

    public void OpenVideoCardsPanel()
    {
        ClosePartPanels();
        VideoCardsPanel.SetActive(true);
    }

    public void OpenStoragesPanel()
    {
        ClosePartPanels();
        StoragesPanel.SetActive(true);
    }

    public void OpenCPUsPanel()
    {
        ClosePartPanels();
        CPUsPanel.SetActive(true);
    }

    public void OpenCPUFansPanel()
    {
        ClosePartPanels();
        CPUFansPanel.SetActive(true);
    }

    public void OpenBodysPanel()
    {
        ClosePartPanels();
        BodysPanel.SetActive(true);
    }
}
