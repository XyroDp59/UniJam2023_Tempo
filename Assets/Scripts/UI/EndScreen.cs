using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndScreen : MonoBehaviour
{
    [SerializeField] private AudioClip MusicTitle;
    private Animator anim;
    public GameObject parent;
	private TMPro.TextMeshProUGUI scoreText;
	

    private void Start()
    {
        SoundManager.Instance.SetMusicSource(MusicTitle);
        StartCoroutine(Wait(0.66f));
        anim = GetComponent<Animator>();
        anim.Play("GameOverAnimation");
        parent = transform.parent.gameObject;
		scoreText = parent.transform.GetChild(1).gameObject.GetComponent<TMPro.TextMeshProUGUI>();
		UpdateScore();
    }

    public void EndCredit()
    {
        GetComponent<Renderer>().enabled = false;
        StartCoroutine(Wait(0.5f));
        for (int i = 1; i <= 4; i += 1)
        {
           parent.transform.GetChild(i).gameObject.SetActive(true); 
        }
    }
    
    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }
	
	private void UpdateScore(){
		float currMinutes, currSeconds;
		currMinutes = PlayerPrefs.GetFloat("CurrentTimeMinutes", 0f);
		currSeconds = PlayerPrefs.GetFloat("CurrentTimeSeconds", 0f);
		
		float highMinutes, highSeconds;
		highMinutes = PlayerPrefs.GetFloat("HighestTimeMinutes", 0f);
		highSeconds = PlayerPrefs.GetFloat("HighestTimeSeconds", 0f);
		
		if ( 60 * currMinutes + currSeconds >= 60 * highMinutes + highSeconds || highMinutes + highSeconds == 0){
			PlayerPrefs.SetFloat("HighestTimeMinutes",currMinutes);
			PlayerPrefs.SetFloat("HighestTimeSeconds",currSeconds);
			scoreText.text = "New Highest Time !\nTime :\n" + string.Format("{0:0}:{1:00}", highMinutes, highSeconds);
		}
		
		scoreText.text = "Highest Time :\n" + string.Format("{0:0}:{1:00}", highMinutes, highSeconds) + "\nTime :\n" + string.Format("{0:0}:{1:00}", currMinutes, currSeconds);
		
	}
}
