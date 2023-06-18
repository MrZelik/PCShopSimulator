using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    [SerializeField] GameObject ColliderGO;

    public bool isBody = false;
    public Transform TakePos;
    public Quaternion TakeRotate;

    PartBuildLogic partBuildLogic;
    Rigidbody rigidBody;
    Collider Collider;
    PCSellInfo pcSellInfo;

    GameObject Player;

    public bool PartSell;

    private void Awake()
    {
        partBuildLogic = GetComponent<PartBuildLogic>();
        rigidBody = GetComponent<Rigidbody>();
        Collider = GetComponent<Collider>();
        pcSellInfo = GetComponent<PCSellInfo>();

        Player = Camera.main.gameObject;
    }

    public void RemovalPart()
    {
        if (partBuildLogic.installed)
        {
            transform.parent.GetComponent<PartBuildLogic>().installedParts[partBuildLogic.installedId] = null;
            partBuildLogic.installed = false;
        }
    }

    public void TakePart()
    {
        if (isBody)
            ColliderGO.SetActive(false);
        else
            Collider.isTrigger = true;
        
        rigidBody.isKinematic = true;
        partBuildLogic.equiped = true;
        transform.parent = null;
        transform.rotation = TakeRotate;
    }

    public void SetPosition(Vector3 PartPos)
    {
        transform.position = PartPos;
        transform.SetParent(Player.transform, false);
        transform.localPosition -= TakePos.localPosition;
    }

    public void FindPCPrice()
    {
        pcSellInfo.FindParts();
    }

    public void DropPart()
    {
        if (isBody)
            ColliderGO.SetActive(true);
        else
            Collider.isTrigger = false;

        rigidBody.isKinematic = false;
        rigidBody.AddForce(transform.parent.transform.forward * 150);
        partBuildLogic.equiped = false;
        transform.parent = null;
    }

    public void SetInstalledID(int id)
    {
        partBuildLogic.installedId = id;
    }

    public void SellPC()
    {
        partBuildLogic.equiped = false;
        pcSellInfo.SellParts();
    }

    public bool PCSellCheck()
    {
        if (isBody)
        {
            if (pcSellInfo.pcSell)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (PartSell)
                return true;
            else
                return false;
        }
    }
}
