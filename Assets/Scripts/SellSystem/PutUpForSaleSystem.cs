using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PutUpForSaleSystem : MonoBehaviour
{
    [SerializeField] private GameObject[] SellPoints;
    [SerializeField] private GameObject[] PCOnSell;
    [SerializeField] private GameObject PutUpForm;
    [SerializeField] private TextMeshProUGUI RecPriceText;
    [SerializeField] private TMP_InputField inputPrice;

    private int pcPrice;

    private GameObject StandPos;
    private GameObject PC;

    ItemCollector itemCollector;

    private void Start()
    {
        itemCollector = Camera.main.gameObject.GetComponent<ItemCollector>();
    }

    private void Update()
    {
        if (Camera.main.transform.childCount >= 1)
        {
            if (Camera.main.transform.GetChild(0).gameObject.GetComponent<PCSellInfo>().pcAssembled)
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

    public void StartSale(GameObject hitGO)
    {
        StandPos = hitGO;
        PC = Camera.main.transform.GetChild(0).gameObject.GetComponent<PCSellInfo>().Body;
        inputPrice.text = PC.GetComponent<PCSellInfo>().pcPrice.ToString();

        ChangeSellMode();
    }

    public void PutUpForSale()
    {
        PCSellInfo PSI = PC.GetComponent<PCSellInfo>();

        pcPrice = int.Parse(inputPrice.text);

        SetPCPosition(PC);
        PSI.SetSellAttributes(pcPrice);
        CheckStandPos(PC);

        MoneyController.money += pcPrice;
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

    private void SetPCPosition(GameObject PC)
    {
        PC.transform.parent = null;
        PC.transform.position = StandPos.transform.position;
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
