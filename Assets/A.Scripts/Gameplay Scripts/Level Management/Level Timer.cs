using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelTimer : MonoBehaviour
{
    private float timer = 600;
    public float minutes;
    private float seconds;

    [SerializeField] private TextMeshProUGUI timerText;

    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        minutes = Mathf.FloorToInt(timer / 60F);
        seconds = Mathf.FloorToInt(timer - minutes * 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
