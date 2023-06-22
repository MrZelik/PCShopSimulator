using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class SellSlot : MonoBehaviour, ISlot
{
    [SerializeField] private GameObject[] SellPoints;
    [SerializeField] private GameObject[] PCOnSell;
    [SerializeField] private GameObject PutUpForm;
    [SerializeField] private TextMeshProUGUI RecPriceText;
    [SerializeField] private TMP_InputField inputPrice;

    public static Action<int> onSale;

    private int pcPrice;

    private GameObject StandPos;
    private GameObject PC;

    public ItemCollector itemCollector;

    private void Start()
    {
        itemCollector = Camera.main.gameObject.GetComponent<ItemCollector>();
    }

    private void Update()
    {
        if (Camera.main.gameObject.transform.childCount != 0)
        {
            if (Camera.main.transform.GetChild(0).gameObject.GetComponent<PCInfo>().pcAssembled)
            {
                ShowSellPoints();
            }
            else
            {
                HideSellPoints();
            }
        }
        else
        {
            HideSellPoints();
        }
    }

    public void Interact(GameObject hitGO)
    {
        StartSale(hitGO);
    }

    public void StartSale(GameObject hitGO)
    {
        StandPos = hitGO;
        PC = Camera.main.transform.GetChild(0).gameObject.GetComponent<PCInfo>().Body;
        inputPrice.text = PC.GetComponent<PCInfo>().pcPrice.ToString();

        ChangeSellMode();
    }

    public void PutUpForSale()
    {
        PCInfo PSI = PC.GetComponent<PCInfo>();

        pcPrice = int.Parse(inputPrice.text);

        SetPCPosition(PC, PSI);
        PSI.SetSellAttributes(pcPrice);
        CheckStandPos(PC);

        onSale?.Invoke(pcPrice);

        ChangeSellMode();

        itemCollector.SellPC();
    }

    private void ShowSellPoints()
    {
        for (int i = 0; i < SellPoints.Length; i++)
        {
            if (!PCOnSell[i])
            {
                SellPoints[i].SetActive(true);
            }
            else
            {
                SellPoints[i].SetActive(false);
            }
        }
    }

    private void HideSellPoints()
    {
        for (int i = 0; i < SellPoints.Length; i++)
        {
            SellPoints[i].SetActive(false);
        }
    }

    public void ChangeSellMode()
    {
        if (!StateController.sellMode)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            StateController.sellMode = true;
            StateController.canMove = false;
            PutUpForm.SetActive(true);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            StateController.sellMode = false;
            StateController.canMove = true;
            PutUpForm.SetActive(false);
        }
    }

    private void SetPCPosition(GameObject PC, PCInfo PSI)
    {
        PC.transform.parent = null;
        PC.transform.position = StandPos.transform.position + PSI.SellPos;
        PC.transform.rotation = StandPos.transform.rotation;
    }

    private void CheckStandPos(GameObject PC)
    {
        for (int i = 0; i < SellPoints.Length; i++)
        {
            if (StandPos == SellPoints[i])
                PCOnSell[i] = PC;
        }

        StandPos = null;
    }


}
