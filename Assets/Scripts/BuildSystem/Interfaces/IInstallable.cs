using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInstallable
{
    bool installed { get; set; }
    Vector3 partConnector { get; set; }
    Quaternion partRotator { get; set; }
    void SearchConnector();
    void InstallPart(GameObject hitGO);
    void SetPartPos(GameObject hitGO, GameObject hitGOParent);
    void ClearParent();
    void FindInstallId(GameObject hitGOParent, GameObject hitGO, PartBuildLogic ParentPBL, CollectableItem CI);
    void SetInstalledAttribute();
}
