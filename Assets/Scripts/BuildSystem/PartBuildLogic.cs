using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartBuildLogic : MonoBehaviour
{
    [SerializeField] private Vector3 partConnector;
    [SerializeField] private Quaternion partRotator;
    public GameObject[] Connectors;

    [SerializeField] private string[] connectorTags;

    public GameObject[] installedParts;

    private GameObject Player;

    public int price = 0;
    public int installedId;

    public bool isBody = false;
    public GameObject BodySideWall;

    public bool equiped = false;
    public bool installed = false;

    Collider Collider;

    private void Start()
    {
        Player = Camera.main.gameObject;

        Collider = GetComponent<Collider>();
    }

    private void Update()
    {
        if (equiped)
        {
            SearchConnector();
        }
        else if (StateController.assemblingMode && !equiped)
        {
            ActivateConnectors();
        }
        else
        {
            DeactivateConnectors();
        }
    }

    private void SearchConnector()
    {
        RaycastHit hit;

        if (Physics.Raycast(RaycastSystem.ray, out hit, RaycastSystem.maxUsableDistance))
        {
            switch (hit.collider.gameObject.tag)
            {
                case "PartConnector":
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        InstallPart(hit.collider.gameObject);
                    }
                    break;
            }
        }
    }

    private void InstallPart(GameObject hitGO)
    {
        GameObject hitGOParent = hitGO.transform.parent.gameObject;

        CollectableItem CI = hitGO.GetComponent<CollectableItem>();
        PartBuildLogic ParentPBL = hitGOParent.GetComponent<PartBuildLogic>();
        ItemCollector IC = Player.GetComponent<ItemCollector>();

        ClearParent();
        IC.ClearTakedPart();
        FindInstallId(hitGOParent, hitGO, ParentPBL, CI);
        SetPartPos(hitGO, hitGOParent);

        hitGO.SetActive(false);

        SetInstalledAttribute();
    }

    private void ActivateConnectors()
    {
        if (isBody)
        {
            BodySideWall.SetActive(false);
        }

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

    private void DeactivateConnectors()
    {
        if (isBody)
        {
            BodySideWall.SetActive(true);
        }

        for (int i = 0; i < Connectors.Length; i++)
        {
            Connectors[i].SetActive(false);
        }
    }

    public void ClearParent()
    {
        transform.parent = null;
    }

    private void FindInstallId(GameObject hitGOParent, GameObject hitGO, PartBuildLogic ParentPBL, CollectableItem CI)
    {
        for (int i = 0; i < ParentPBL.Connectors.Length; i++)
        {
            if (ParentPBL.Connectors[i] == hitGO)
            {
                HideAllParentConnectors(hitGOParent, ParentPBL);
                ParentPBL.installedParts[i] = gameObject;
                installedId = i;
                break;
            }
        }
    }

    private void HideAllParentConnectors(GameObject hitGOParent, PartBuildLogic ParentPBL)
    {
        for (int i = 0; i < Connectors.Length; i++)
            ParentPBL.Connectors[i].SetActive(false);
    }

    public void SetPartPos(GameObject hitGO, GameObject hitGOParent)
    {
        Vector3 connectPos = hitGO.transform.localPosition;
        transform.position = connectPos;

        transform.SetParent(hitGOParent.transform, false);

        Vector3 scale = new Vector3(transform.localScale.x / hitGOParent.transform.localScale.x, transform.localScale.y / hitGOParent.transform.localScale.y, transform.localScale.z / hitGOParent.transform.localScale.z);
        transform.localScale = scale;

        transform.rotation = hitGO.transform.rotation;

        Vector3 pos = transform.localPosition;
        pos += partConnector;
        transform.localPosition = pos;

        transform.localRotation = partRotator;
    }

    private void SetInstalledAttribute()
    {
        Collider.isTrigger = true;
        equiped = false;
        installed = true;
    }
}
