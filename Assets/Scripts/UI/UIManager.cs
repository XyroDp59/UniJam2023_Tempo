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


    public TMPro.TextMeshProUGUI timerText;


    // Start is called before the first frame update
    void Start()
    {
        lastSecond = timeRemaining;
        globalTime = timeRemaining;


    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
    }


  
    
    void UpdateTimer()
    {
        float timeToDisplay = timeRemaining + 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        if (seconds > lastSecond)
        { 
            lastSecond = seconds;

        }

        timerText.text = "Time\n" + string.Format("{0:0}:{1:00}", minutes, seconds);

        timeRemaining += Time.deltaTime;


    }
}

