using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class UIManager : MonoBehaviour
{
    
    private float globalTime;
    public float timeRemaining;
    private float lastSecond;

    public int score = 0;
    private int scorePerSecond = 5;

    public TMPro.TextMeshProUGUI timerText;
    public TMPro.TextMeshProUGUI scoreText;
    public TMPro.TextMeshProUGUI scoreIncreaseText;

    // Start is called before the first frame update
    void Start()
    {
        DisplayScore();
        lastSecond = timeRemaining;
        globalTime = timeRemaining;
        scoreIncreaseText.color = UnityEngine.Color.red;

    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
    }


  


    public void IncreaseScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreIncreaseText.text = "+" + scoreToAdd + "!";
        scoreIncreaseText.canvasRenderer.SetAlpha(1);
        scoreIncreaseText.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-10, 10));

        UnityEngine.Color currentColor = scoreIncreaseText.color;

        if (currentColor == UnityEngine.Color.red)
            scoreIncreaseText.color = UnityEngine.Color.green;
        else if(currentColor == UnityEngine.Color.green)
            scoreIncreaseText.color = UnityEngine.Color.blue;
        else if(currentColor == UnityEngine.Color.blue)
            scoreIncreaseText.color = UnityEngine.Color.red;


        scoreIncreaseText.CrossFadeAlpha(0, 0.7f, true);
        DisplayScore();
    }

    void DisplayScore()
    {
        scoreText.text = "Score\n" + score;
    }
    void UpdateTimer()
    {
        float timeToDisplay = timeRemaining + 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        if (seconds > lastSecond)
        {
            IncreaseScore(scorePerSecond);
            lastSecond = seconds;

        }

        timerText.text = "Time\n" + string.Format("{0:0}:{1:00}", minutes, seconds);

        timeRemaining += Time.deltaTime;


    }
}

