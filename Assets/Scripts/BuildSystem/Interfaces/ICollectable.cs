using UnityEngine;

interface ICollectable
{
    bool equiped { get; set; }
    void ClearParent();
    
}
