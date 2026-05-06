using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelTimer : MonoBehaviour
{
    private float timer = GlobalTimerSettings.globalTimer;
    public float minutes;
    private float seconds;
    public int scalingTime;
    
    [SerializeField] private Animator elevatorAnimator;
    [SerializeField] private TextMeshProUGUI timerText;

    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if(minutes >= 7)
        {
            scalingTime = 30;
        }
        else if(minutes >= 4)
        {
            scalingTime = 25;
        }
        else if(minutes >= 1)
        {
            scalingTime = 15;
        }
        
        minutes = Mathf.FloorToInt(timer / 60F);
        seconds = Mathf.FloorToInt(timer - minutes * 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if(timer <= 0)
        {
            timer = 0;

            //play animation for elevator
            elevatorAnimator.SetTrigger("Open");
        }
    }
}
