using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DayController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dayCountText;
    
    public static int dayCount = 1;

    private void Start()
    {
        dayCountText.text = "����: " + dayCount.ToString();    
    }

    public void GoToSleep()
    {
        dayCount++;
        SceneManager.LoadScene(0);
    }
}
