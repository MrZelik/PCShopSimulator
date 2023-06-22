using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartBuildLogic : MonoBehaviour, ISlot
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
    RaycastSystem raycastSystem;

    private void Awake()
    {
        AddPartToContainer();
    }

    private void Start()
    {
        Player = Camera.main.gameObject;
        raycastSystem = Player.GetComponent<RaycastSystem>();

        Collider = GetComponent<Collider>();
    }

    private void AddPartToContainer()
    {
        switch (gameObject.tag)
        {
            case "Body":
                ConnectorsContainer.Bodys.Add(this);
                break;
            case "MotherBoard":
                ConnectorsContainer.MotherBoards.Add(this);
                break;
            case "CPU":
                ConnectorsContainer.CPUs.Add(this);
                break;
        }
    }

    private void OnDestroy()
    {
        switch (gameObject.tag)
        {
            case "Body":
                ConnectorsContainer.Bodys.Remove(this);
                break;
            case "MotherBoard":
                ConnectorsContainer.MotherBoards.Remove(this);
                break;
            case "CPU":
                ConnectorsContainer.CPUs.Remove(this);
                break;
        }
    }

    private void Update()
    {
        if (equiped)
        {
            SearchConnector();
        }

        if (isBody)
        {
            if (StateController.assemblingMode)
                BodySideWall.SetActive(false);
            else
                BodySideWall.SetActive(true);
        }
    }


    private void SearchConnector()
    {
        RaycastHit hit;

        if (Physics.Raycast(raycastSystem.ray, out hit, raycastSystem.maxUsableDistance))
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

    public void Interact(GameObject hitGO)
    {

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

    public void ActivateConnectors()
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

    public void DeactivateConnectors()
    {

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
