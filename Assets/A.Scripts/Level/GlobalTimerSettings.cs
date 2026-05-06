using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GlobalTimerSettings : MonoBehaviour
{
    public static float globalTimer = 300;
    [SerializeField] private TextMeshProUGUI timerText;

    void Update()
    {
        timerText.text = "Current Timer = " + globalTimer + " Seconds";
    }

    public void FiveMin()
    {
        globalTimer = 300;
    }

    public void TenMin() 
    {
        globalTimer = 600;
    }

    public void FifteenMin()
    {
        globalTimer = 900;
    }
}
