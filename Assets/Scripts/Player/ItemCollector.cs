using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private Vector3 PartPos;
    [SerializeField] private GameObject Player;

    public GameObject ItemControllInfo;

    RaycastSystem raycastSystem;

    public GameObject Part = null;

    private void Start()
    {
        raycastSystem = GetComponent<RaycastSystem>();
    }

    private void Update()
    {
        if(Part != null && Input.GetKeyDown(KeyCode.Q))
        {
            DropPart();
        }

        CheckPart();
    }


    private void CheckPart()
    {
        RaycastHit hit;

        if (Physics.Raycast(raycastSystem.ray, out hit, raycastSystem.maxUsableDistance))
        {
            switch (hit.collider.tag)
            {
                case "PowerUnit":
                case "VideoCard":
                case "MotherBoard":
                case "RAM":
                case "CPU":
                case "Body":
                case "Storage":
                    if (Input.GetKeyDown(KeyCode.E) && Part == null)
                    {
                        TakePart(hit.collider.gameObject);
                    }
                    break;
            }
        }
        else
        {
            raycastSystem.PressButtonText.gameObject.SetActive(false);
        }
    }

    private void TakePart(GameObject hit)
    {
        Part = hit;

        if (Part.GetComponent<PCSellInfo>() && Part.GetComponent<PCSellInfo>().pcSell)
        {
            Part = null;
            return;
        }
            

        if (Part.GetComponent<PartBuildLogic>().installed)
        {
            for (int i = 0; i < Part.transform.parent.gameObject.GetComponent<PartBuildLogic>().installedParts.Length; i++)
            {
                if (Part.transform.parent.gameObject.GetComponent<PartBuildLogic>().installedParts[i] == Part)
                {
                    Part.transform.parent.gameObject.GetComponent<PartBuildLogic>().installedParts[i] = null;
                    Part.GetComponent<PartBuildLogic>().installed = false;
                }
            }
        }

       
        Part.transform.position = PartPos;   //тут надо сделать вызов функций
        Part.transform.SetParent(Player.transform, false);

        if (Part.GetComponent<CollectableItem>())
        {
            Part.transform.localPosition -= Part.GetComponent<CollectableItem>().TakePos.localPosition;
        }

        if (Part.GetComponent<PCSellInfo>())
        {
            Part.GetComponent<PCSellInfo>().FindParts();
        }
        
        ItemControllInfo.SetActive(true);
    }

    private void DropPart()
    {
        Part.GetComponent<Collider>().isTrigger = false;
        Part.GetComponent<Rigidbody>().isKinematic = false;
        Part.GetComponent<Rigidbody>().AddForce(transform.forward * 100);
        Part.GetComponent<PartBuildLogic>().equiped = false;
        Part.transform.parent = null;
        Part = null;
        ItemControllInfo.SetActive(false);
    }

    public void SellPC()
    {
        Part.GetComponent<PartBuildLogic>().equiped = false;
        Part = null;
        ItemControllInfo.SetActive(false);
    }
}
