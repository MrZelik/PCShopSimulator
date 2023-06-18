using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private Vector3 PartPos;
    [SerializeField] private GameObject Player;

    public GameObject ItemControllInfo;

    RaycastSystem raycastSystem;
    ControllInfoController ControllInfoController;

    public GameObject Part = null;
    

    private void Start()
    {
        raycastSystem = GetComponent<RaycastSystem>();
        ControllInfoController = GetComponent<ControllInfoController>();
    }

    private void Update()
    {
        if(Part != null && Input.GetKeyDown(KeyCode.Q))
        {
            DropPart();
        }
    }

    public void TakePart(GameObject hitGO)
    {
        Part = hitGO;
        CollectableItem CI = Part.gameObject.GetComponent<CollectableItem>();

        if (CI.PCSellCheck())
        {
            Part = null;
            return;
        }

        CI.RemovalPart();
        CI.TakePart();
        CI.SetPosition(PartPos);
        CI.FindPCPrice();
        
        ItemControllInfo.SetActive(true);
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
