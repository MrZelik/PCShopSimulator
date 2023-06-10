using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCSellInfo : MonoBehaviour
{
    public Vector3 SellPos;

    public int pcPrice;

    public GameObject WiresPrefab;
    public GameObject Body;
    public GameObject PowerUnit;
    public GameObject MotherBoard;
    public GameObject VideoCard;
    public List<GameObject> RAM = new List<GameObject>();
    public GameObject CPU;
    public GameObject CPUFan;
    public List<GameObject> Storage = new List<GameObject>();

    public bool pcAssembled = false;
    public bool pcSell = false;

    Collider Collider;
    Rigidbody rigidBody;
    PCSellInfo pcSellInfo;

    private void Start()
    {
        Collider = GetComponent<Collider>();
        rigidBody = GetComponent<Rigidbody>();
        pcSellInfo = GetComponent<PCSellInfo>();
    }

    public void Update()
    {
        if (Body && PowerUnit && MotherBoard && VideoCard && CPU && RAM[0] && Storage[0])
        {
            pcAssembled = true;
        }
        else
        {
            pcAssembled = false;
        }
    }

    public void FindParts()
    {
        ClearAllParts();

        Body = gameObject;

        SearchPartsOnBody();

        FindPrice();
    }

    private void SearchPartsOnBody()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            switch (transform.GetChild(i).tag)
            {
                case "PowerUnit":
                    PowerUnit = transform.GetChild(i).gameObject;
                    break;
                case "MotherBoard":
                    MotherBoard = transform.GetChild(i).gameObject;
                    SearchPartsOnMotherBoards();
                    break;
                case "Storage":
                    Storage.Add(transform.GetChild(i).gameObject);
                    break;
            }
        }
    }

    private void SearchPartsOnMotherBoards()
    {
        for (int j = 0; j < MotherBoard.transform.childCount; j++)
        {
            switch (MotherBoard.transform.GetChild(j).tag)
            {
                case "RAM":
                    RAM.Add(MotherBoard.transform.GetChild(j).gameObject);
                    break;
                case "CPU":
                    CPU = MotherBoard.transform.GetChild(j).gameObject;

                    for (int i = 0; i < CPU.transform.childCount; i++)
                    {
                        if(CPU.transform.GetChild(i).tag == "CPUFan")
                            CPUFan = CPU.transform.GetChild(i).gameObject;
                    }

                    break;
                case "VideoCard":
                    VideoCard = MotherBoard.transform.GetChild(j).gameObject;
                    break;
            }
        }
    }

    public int FindPrice()
    {
        pcPrice = 0;
        pcPrice += PowerUnit.GetComponent<PartBuildLogic>().price;
        pcPrice += MotherBoard.GetComponent<PartBuildLogic>().price;
        pcPrice += Body.GetComponent<PartBuildLogic>().price;
        pcPrice += VideoCard.GetComponent<PartBuildLogic>().price;
        pcPrice += CPU.GetComponent<PartBuildLogic>().price;
        pcPrice += CPUFan.GetComponent<PartBuildLogic>().price;


        for (int i = 0; i < Storage.Count; i++)
        {
            pcPrice += Storage[i].GetComponent<PartBuildLogic>().price;
        }

        for (int i = 0; i < RAM.Count; i++)
        {
            pcPrice += RAM[i].GetComponent<PartBuildLogic>().price;
        }

        return pcPrice;
    }

    private void ClearAllParts()
    {
        Body = null;
        PowerUnit = null;
        MotherBoard = null;
        VideoCard = null;
        RAM.Clear();
        CPU = null;
        CPUFan = null;
        Storage.Clear();
        StateController.assemblingMode = false;
    }

    public void SetSellAttributes(int price)
    {
        Collider.isTrigger = true;
        rigidBody.useGravity = false;
        pcSellInfo.pcPrice = price;
        pcSellInfo.pcSell = true;
    }

    public void SellParts()
    {
        PowerUnit.GetComponent<CollectableItem>().PartSell = true;

        MotherBoard.GetComponent<CollectableItem>().PartSell = true;

        for (int i = 0; i < Storage.Count; i++)
        {
            Storage[i].GetComponent<CollectableItem>().PartSell = true;
        }
        for (int i = 0; i < RAM.Count; i++)
        {
            RAM[i].GetComponent<CollectableItem>().PartSell = true;
        }

        CPU.GetComponent<CollectableItem>().PartSell = true;

        CPUFan.GetComponent<CollectableItem>().PartSell = true;

        VideoCard.GetComponent<CollectableItem>().PartSell = true;
    }
}
