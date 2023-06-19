using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IConnectorDisplayable
{
    GameObject[] Connectors { get; set; }
    string[] connectorTags { get; set; }
    GameObject[] installedParts { get; set; }
    void ActivateConnectors();
    void DeactivateAllConnectors();
} 
