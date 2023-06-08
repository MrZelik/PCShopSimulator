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

    private void Update()
    {
        if (Camera.main.transform.childCount >= 1)
        {
            if (Camera.main.transform.GetChild(0).gameObject.GetComponent<PCSellInfo>().pcAssembled)
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
            else
            {
                for (int i = 0; i < SellPoints.Length; i++)
                {
                    SellPoints[i].SetActive(false);
                }
            }
        }
        else
        {
            for (int i = 0; i < SellPoints.Length; i++)
            {
                SellPoints[i].SetActive(false);
            }
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

    public void StartSale(GameObject hitGO)
    {
        StandPos = hitGO;
        GameObject PC = Camera.main.transform.GetChild(0).gameObject.GetComponent<PCSellInfo>().Body;
        inputPrice.text = PC.GetComponent<PCSellInfo>().pcPrice.ToString();

        ChangeSellMode();
    }

    public void PutUpForSale()
    {
        pcPrice = int.Parse(inputPrice.text);
        GameObject PC = Camera.main.transform.GetChild(0).gameObject.GetComponent<PCSellInfo>().Body;
        PC.transform.parent = null;
        PC.transform.position = StandPos.transform.position;
        PC.transform.rotation = StandPos.transform.rotation;
        PC.transform.localScale = new Vector3(0.7f, 1f, 1f);
        PC.GetComponent<Collider>().isTrigger = true;
        PC.GetComponent<Rigidbody>().useGravity = false;
        PC.GetComponent<PCSellInfo>().pcPrice = pcPrice;
        PC.GetComponent<PCSellInfo>().pcSell = true;

        for (int i = 0; i < SellPoints.Length; i++)
        {
            if(StandPos == SellPoints[i])
                PCOnSell[i] = PC;
        }

        StandPos = null;

        MoneyController.money += pcPrice;
        ChangeSellMode();

        Camera.main.gameObject.GetComponent<ItemCollector>().SellPC();
    }
}
