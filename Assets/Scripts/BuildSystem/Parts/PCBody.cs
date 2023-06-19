using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCBody : MonoBehaviour, IConnectorDisplayable, IInstallable
{
    //IConnectorDisplayable Settings
    [field: SerializeField] public GameObject[] Connectors { get; set; }
    [field: SerializeField] public string[] connectorTags { get; set; }
    [field: SerializeField] public GameObject[] installedParts { get; set; }

    //ICollectable Settings
    [field: SerializeField] public Vector3 partConnector { get; set; }
    [field: SerializeField] public Quaternion partRotator { get; set; }
    [field: SerializeField] public bool installed { get; set; }

    //ICollectable Settings
    public bool equiped { get; set; }

    [SerializeField] private GameObject BodySideWall;
    private GameObject PlayerCamera;
    private bool sideWallOpen = false;
    public int price = 0;

    Collider Collider;

    private void Start()
    {
        PlayerCamera = Camera.main.gameObject;
        installed = false;
        equiped = false;

        Collider = GetComponent<Collider>();
    }

    private void Update()
    {
        if (StateController.assemblingMode && !sideWallOpen)
        {
            OpedSideWall();            
        }          
        else if ((!StateController.assemblingMode && sideWallOpen) && equiped)
        {
            CloseSideWall();
            DeactivateAllConnectors();
        }

        if (StateController.assemblingMode && ItemCollector.Part != null)
        {
            ActivateConnectors();
        }

        if(equiped)
            SearchConnector();
    }


    //Beginning IConnectorDisplayable 
    public void ActivateConnectors()
    {
        for (int i = 0; i < Connectors.Length; i++)
        {
            if (ItemCollector.Part.tag == connectorTags[i] && !installedParts[i])
            {
                Connectors[i].SetActive(true);
            }
            else
            {
                Connectors[i].SetActive(false);
            }
        }
    }

    public void DeactivateAllConnectors()
    {
        for (int i = 0; i < Connectors.Length; i++)
        {
            Connectors[i].SetActive(false);
        }
    }

    private void OpedSideWall()
    {
        sideWallOpen = true;
        BodySideWall.SetActive(true);
    }

    private void CloseSideWall()
    {
        sideWallOpen = false;
        BodySideWall.SetActive(false);
    }
    //End IConnectorDisplayable 

    //Beginning IInstallable
    public void SearchConnector()
    {
        RaycastHit hit;

        if (Physics.Raycast(RaycastSystem.ray, out hit, RaycastSystem.maxUsableDistance))
        {
            switch (hit.collider.gameObject.tag)
            {
                case "PartConnector":
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        print("22");
                        InstallPart(hit.collider.gameObject);
                    }
                    break;
            }
        }
    }

    public void InstallPart(GameObject hitGO)
    {
        GameObject hitGOParent = hitGO.transform.parent.gameObject;

        CollectableItem CI = hitGO.GetComponent<CollectableItem>();
        PartBuildLogic ParentPBL = hitGOParent.GetComponent<PartBuildLogic>();
        ItemCollector IC = PlayerCamera.GetComponent<ItemCollector>();

        ClearParent();
        IC.ClearTakedPart();
        FindInstallId(hitGOParent, hitGO, ParentPBL, CI);
        SetPartPos(hitGO, hitGOParent);

        hitGO.SetActive(false);

        SetInstalledAttribute();
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

    public void ClearParent()
    {
        transform.parent = null;
    }

    public void FindInstallId(GameObject hitGOParent, GameObject hitGO, PartBuildLogic ParentPBL, CollectableItem CI)
    {
        for (int i = 0; i < ParentPBL.Connectors.Length; i++)
        {
            if (ParentPBL.Connectors[i] == hitGO)
            {
                HideAllParentConnectors(hitGOParent, ParentPBL);
                ParentPBL.installedParts[i] = gameObject;
                break;
            }
        }
    }

    public void HideAllParentConnectors(GameObject hitGOParent, PartBuildLogic ParentPBL)
    {
        for (int i = 0; i < Connectors.Length; i++)
            ParentPBL.Connectors[i].SetActive(false);
    }

    public void SetInstalledAttribute()
    {
        Collider.isTrigger = true;
        equiped = false;
        installed = true;
    }
    //End IInstallable
}