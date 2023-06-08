using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PartInfo
{
    public int id;
    public string Name;
    public int price;
    public int ratioPrice;
    public Sprite Image;
    public GameObject PartGO;
}

[System.Serializable]
public class Slot
{
    public int slotId;
    public int partId;
    public string partName;
    public int partPrice;
    public Sprite PartImage;
    public GameObject PartGO;
}