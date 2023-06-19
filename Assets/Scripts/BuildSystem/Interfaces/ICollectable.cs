using UnityEngine;

interface ICollectable
{
    void ClearParent();
    void SetPartPos(GameObject hitGO, GameObject hitGOParent);
}
