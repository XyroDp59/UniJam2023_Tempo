using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class TempoManager : MonoBehaviour
{
	

	public UnityEvent onTimeUp;
	private float _timer;
	public float duration = 5f;
	private bool _shouldPlaySound = true;
	[SerializeField] private float previewSoundDuration;

	public float timeSurvived;
	public int score;

    //event listener -> activer les fcts de pleins d'objets	

    private void Start(){
		_timer = duration;
	}
	
	private void FixedUpdate(){
		_timer -= Time.deltaTime;

		if (Mathf.RoundToInt(timeSurvived + Time.deltaTime) > Mathf.RoundToInt(timeSurvived)) score += 5;
		
		timeSurvived += Time.deltaTime;


		if(_timer <= previewSoundDuration && _shouldPlaySound){
			//play sound
			_shouldPlaySound = false;
		}
		if(_timer <= 0){
			onTimeUp.Invoke();
			_shouldPlaySound = true;
			_timer = duration;

			print("Time : "+ timeSurvived + "     Score : "+ score);
		}
	}
}
