using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectorsContainer : MonoBehaviour
{
    public static List<PartBuildLogic> MotherBoards = new List<PartBuildLogic>();
    public static List<PartBuildLogic> Bodys = new List<PartBuildLogic>();
    public static List<PartBuildLogic> CPUs = new List<PartBuildLogic>();

    ItemCollector itemCollector;

    private void Start()
    {
        itemCollector = GetComponent<ItemCollector>();
        FindPartsType(null);
    }

    public void FindPartsType(GameObject Part)
    {
        if(Part != null && StateController.assemblingMode)
        {
            switch (Part.tag)
            {
                case "MotherBoard":
                case "PowerUnit":
                case "Storage":
                    ShowConnectors(Bodys);
                    break;
                case "VideoCard":
                case "RAM":
                case "CPU":
                    ShowConnectors(MotherBoards);
                    break;
                case "CPUFan":
                    ShowConnectors(CPUs);
                    break;
            }
        }
        else
        {
            HideAllConnectors(Bodys);
            HideAllConnectors(MotherBoards);
            HideAllConnectors(CPUs);
        }
        
    }

    private void ShowConnectors(List<PartBuildLogic> partsList)
    {
        for (int i = 0; i < partsList.Count; i++)
        {
            partsList[i].ActivateConnectors();
        }
    }

    private void HideAllConnectors(List<PartBuildLogic> partsList)
    {
        for (int i = 0; i < partsList.Count; i++)
        {
            partsList[i].DeactivateConnectors();
        }
    }

}
