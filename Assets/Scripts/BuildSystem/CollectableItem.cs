using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    public Transform TakePos;

    PartBuildLogic partBuildLogic;
    Rigidbody rigidbody;
    Collider collider;
    PCSellInfo pcSellInfo;

    private void Start()
    {
        partBuildLogic = GetComponent<PartBuildLogic>();
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        pcSellInfo = new PCSellInfo();
    }

    public void TakePart()
    {
        collider.isTrigger = true;
        rigidbody.isKinematic = true;
        partBuildLogic.equiped = true;
        transform.parent = null;
        transform.rotation = new Quaternion(0, 0, 0, 0);
        transform.localScale = partBuildLogic.startScale;
    }

    public void DropPart()
    {
        
    }


}
