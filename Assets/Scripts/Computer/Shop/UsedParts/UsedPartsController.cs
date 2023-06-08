using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UsedPartsController : MonoBehaviour
{
    [Header("Info")]
    [SerializeField] private GameObject[] PostPositions;
    public List<Slot> Slots = new List<Slot>();
    [SerializeField] private PartInfo[] Parts;
    [SerializeField] private GameObject MessagePanel;

    [Header("SlotInfo")]
    [SerializeField] private TextMeshProUGUI[] NameText;
    [SerializeField] private TextMeshProUGUI[] PriceText;
    [SerializeField] private Image[] PartImage;

    private int countPosts;

    private void Start()
    {
        countPosts = Random.Range(PostPositions.Length/5, PostPositions.Length);

        for (int i = 0; i < countPosts; i++)
        {
            AddPostInfo(i);
        }
    }

    private void AddPostInfo(int postId)
    {
        PostPositions[postId].SetActive(true);

        int partId = Random.Range(0,Parts.Length);

        Slot slot = new Slot();

        slot.slotId = postId;
        slot.partId = Parts[partId].id;
        slot.partName = Parts[partId].Name;
        float price = Parts[partId].price;
        float ratio = price * Random.Range(1, Parts[partId].ratioPrice) / 100;

        if (Random.Range(0, 2) == 0)
        {
            price += ratio;
        }
        else
        {
            price -= ratio;
        }

        slot.partPrice = (int)price;
        slot.PartImage = Parts[partId].Image;
        slot.PartGO = Parts[partId].PartGO;

        Slots.Add(slot);

        WritePostInfo(slot,postId);
    }

    private void WritePostInfo(Slot slot, int postId)
    {
        PostPositions[postId].SetActive(true);
        NameText[postId].text = slot.partName;
        PriceText[postId].text = slot.partPrice + "Ð";
        PartImage[postId].sprite = slot.PartImage;
    }

    public void DeletePost(int postId)
    {
        PostPositions[postId].SetActive(false); 
    }
}
