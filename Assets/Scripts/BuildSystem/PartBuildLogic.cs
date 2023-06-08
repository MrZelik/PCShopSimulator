using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartBuildLogic : MonoBehaviour
{
    [SerializeField] private Vector3 partConnector;
    public GameObject[] Connectors;

    [SerializeField] private string[] connectorTags;

    public GameObject[] installedParts;

    private GameObject Player;

    public Vector3 startScale;

    public int price = 0;

    public bool equiped = false;
    public bool installed = false;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("MainCamera");

        startScale = transform.localScale;
    }

    private void Update()
    {
        if (equiped)
        {
            SearchConnector();
        }
        else if (StateController.assemblingMode && !equiped)
        {
            for (int i = 0; i < Connectors.Length; i++)
            {
                if (Player.transform.GetChild(Player.transform.childCount - 1).gameObject.tag == connectorTags[i] && !installedParts[i])
                {
                    Connectors[i].SetActive(true);
                }
                else
                {
                    Connectors[i].SetActive(false);
                }
            }
        }
        else
        {
            for (int i = 0; i < Connectors.Length; i++)
            {
                Connectors[i].SetActive(false);
            }
        }
    }

    private void HideAllConnectors(GameObject hitGOParent)
    {
        for (int i = 0; i < Connectors.Length; i++)
            hitGOParent.GetComponent<PartBuildLogic>().Connectors[i].SetActive(false);

    }
    
    private void SearchConnector()
    {
        RaycastHit hit;
        if (Physics.Raycast(Player.GetComponent<RaycastSystem>().ray, out hit, Player.GetComponent<RaycastSystem>().maxUsableDistance))
        {
            switch (hit.collider.gameObject.tag)
            {
                case "PartConnector":
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        InstalledPart(hit.collider.gameObject);
                    }
                    break;
            }
        }
    }

    private void InstalledPart(GameObject hitGO)
    {
        GameObject hitGOParent = hitGO.transform.parent.gameObject;

        transform.parent = null;
       
        Player.GetComponent<ItemCollector>().Part = null;
        Player.GetComponent<ItemCollector>().ItemControllInfo.SetActive(false);

        Vector3 connectPos = hitGO.transform.localPosition;

        for (int i = 0; i < hitGOParent.GetComponent<PartBuildLogic>().Connectors.Length; i++)
        {
            if (hitGOParent.GetComponent<PartBuildLogic>().Connectors[i] == hitGO)
            {
                HideAllConnectors(hitGOParent);
                hitGOParent.GetComponent<PartBuildLogic>().installedParts[i] = gameObject;
                break;
            }
        }

        transform.position = connectPos;
        transform.SetParent(hitGOParent.transform, false);
        transform.rotation = hitGO.transform.rotation;

        Vector3 pos = transform.localPosition;
        pos += partConnector;
        transform.localPosition = pos;

        hitGO.SetActive(false);

        gameObject.GetComponent<Collider>().isTrigger = true;

        equiped = false;
        installed = true;
    }
}
