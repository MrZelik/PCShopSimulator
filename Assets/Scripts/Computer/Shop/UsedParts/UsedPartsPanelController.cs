
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
    [SerializeField] GameObject BodysPanel;

    public void ClosePartPanel()
    {
        PowerUnitsPanel.SetActive(false);
        MotherBoardsPanel.SetActive(false);
        RamsPanel.SetActive(false);
        VideoCardsPanel.SetActive(false);
        StoragesPanel.SetActive(false);
        CPUsPanel.SetActive(false);
        BodysPanel.SetActive(false);
    }

    public void OpenPowerUnitsPanel()
    {
        ClosePartPanel();
        PowerUnitsPanel.SetActive(true);
    }

    public void OpenMotherBoardsPanel()
    {
        ClosePartPanel();
        MotherBoardsPanel.SetActive(true);
    }

    public void OpenRamsPanel()
    {
        ClosePartPanel();
        RamsPanel.SetActive(true);
    }

    public void OpenVideoCardsPanel()
    {
        ClosePartPanel();
        VideoCardsPanel.SetActive(true);
    }

    public void OpenStoragesPanel()
    {
        ClosePartPanel();
        StoragesPanel.SetActive(true);
    }

    public void OpenCPUsPanel()
    {
        ClosePartPanel();
        CPUsPanel.SetActive(true);
    }

    public void OpenBodysPanel()
    {
        ClosePartPanel();
        BodysPanel.SetActive(true);
    }
}
