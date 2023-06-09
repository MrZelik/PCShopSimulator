using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private Vector3 PartPos;
    [SerializeField] private GameObject Player;

    public GameObject ItemControllInfo;

    RaycastSystem raycastSystem;
    ConnectorsContainer connectorsContainer;

    public GameObject Part = null;
    

    private void Start()
    {
        raycastSystem = GetComponent<RaycastSystem>();
        connectorsContainer = GetComponent<ConnectorsContainer>();
    }

    private void Update()
    {
        if(Part != null && Input.GetKeyDown(KeyCode.Q))
        {
            DropPart();
        }

        connectorsContainer.FindPartsType(Part);
    }

    public void TakePart(GameObject hitGO)
    {
        Part = hitGO;
        CollectableItem CI = Part.gameObject.GetComponent<CollectableItem>();

        if (CI.PCSellCheck() || (Part.gameObject.tag != "Body" && !StateController.assemblingMode && Part.GetComponent<PartBuildLogic>().installed))
        {
            Part = null;
            return;
        }

        CI.RemovalPart();
        CI.TakePart();
        CI.SetPosition(PartPos);
        SetParentForPart(hitGO);
        CI.SetLocalPosition(PartPos);
        CI.FindPCPrice();
        
        ItemControllInfo.SetActive(true);
    }

    private void SetParentForPart(GameObject hitGO)
    {
        hitGO.transform.SetParent(transform, false);
    }

    private void DropPart()
    {
        CollectableItem CI = Part.gameObject.GetComponent<CollectableItem>();

        CI.DropPart();
        ClearTakedPart();
    }

    public void SellPC()
    {
        CollectableItem CI = Part.gameObject.GetComponent<CollectableItem>();

        CI.SellPC();

        Part = null;
        ItemControllInfo.SetActive(false);
    }

    public void ClearTakedPart()
    {
        Part = null;
        ItemControllInfo.SetActive(false);
    }
}
