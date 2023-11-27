using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    
    public static UIManager Instance { get; private set; }
    
    private float globalTime;
    public float timeRemaining;
    private float lastSecond;


    public TMPro.TextMeshProUGUI timerText;
    
    [SerializeField] private Slider healthBar;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    
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
		
		PlayerPrefs.SetFloat("CurrentTimeMinutes",minutes);
		PlayerPrefs.SetFloat("CurrentTimeSeconds",seconds);
        timerText.text = "Time\n" + string.Format("{0:0}:{1:00}", minutes, seconds);

        timeRemaining += Time.deltaTime;
    }

    public void InitializeHealth(int maxHealth)
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
    }
    
    public void UpdateHealth(int playerHealth)
    {
        healthBar.value = playerHealth;
    }
}

