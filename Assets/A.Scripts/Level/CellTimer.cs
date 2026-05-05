using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CellTimer : MonoBehaviour
{
    private float timer = 180;
    private float minutes;
    private float seconds;

    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Animator cellAnimator;

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        minutes = Mathf.FloorToInt(timer / 60F);
        seconds = Mathf.FloorToInt(timer - minutes * 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (timer <= 0)
        {
            timer = 0;

            //play animation for elevator
            cellAnimator.SetTrigger("Open");
        }
    }
}
