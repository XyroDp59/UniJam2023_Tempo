using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
	[SerializeField] private AudioClip MusicTitle;
	public void StartGame(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
	
	void Start(){
		SoundManager.Instance.SetMusicSource(MusicTitle);
	}
}
