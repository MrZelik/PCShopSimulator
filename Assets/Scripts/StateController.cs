using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    public static bool canMove = true;
    public static bool pcMode = false;
    public static bool assemblingMode = false;
    public static bool sellMode = false;

    private void Start()
    {
        canMove = true;
        pcMode = false;
        assemblingMode = false;
        sellMode = false;
    }
}
