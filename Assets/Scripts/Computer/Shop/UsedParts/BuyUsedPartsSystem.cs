using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyUsedPartsSystem : MonoBehaviour
{
    [Header("Parts")]
    [SerializeField] GameObject[] PowerUnits;
    [SerializeField] GameObject[] MotherBoards;
    [SerializeField] GameObject[] Rams;
    [SerializeField] GameObject[] VideoCards;
    [SerializeField] GameObject[] Storages;

    [Header("Panels")]
    [SerializeField] GameObject NoEnoughtPanel;

    [Header("Other")]
    [SerializeField] GameObject SpawnZone;

    public void BuyPart(int slotId, GameObject Sender, int price)
    {
        Slot slot = Sender.GetComponent<UsedPartsController>().Slots[slotId];

        if (MoneyController.money - slot.partPrice >= 0)
        {
            GameObject part = Instantiate(slot.PartGO);
            part.transform.position = SpawnZone.transform.position;
            part.GetComponent<PartBuildLogic>().price = slot.partPrice;
            Sender.GetComponent<UsedPartsController>().DeletePost(slotId);

            MoneyController.money -= price;
        }
        else
        {
            StartCoroutine(ShowNoMoneyPanel());
        }
    }

    IEnumerator ShowNoMoneyPanel()
    {
        NoEnoughtPanel.SetActive(true);
        yield return new WaitForSeconds(1f);
        NoEnoughtPanel.SetActive(false);
    }

}
