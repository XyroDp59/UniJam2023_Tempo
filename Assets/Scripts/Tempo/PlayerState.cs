using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
	private List<GameObject> stateObjectList = new List<GameObject>();
	private List<GameObject> newStateObjectList = new List<GameObject>();
	private int currentState; // 0 = R ; 1 = V ; 2 = B
	
	[SerializeField] private AudioClip Music1, Music2, Music0;
	
	
    // Start is called before the first frame update
    void Awake()
    {
        for(int i = 0; i <=2 ; i++) stateObjectList.Add(transform.GetChild(i).gameObject);
    }

	void Start(){
		SoundManager.Instance.SetMusicSource(Music0);
		}

	public void UpdatePlayerState()
	{
		List<int> PossibleStates = new List<int>();
		for (int i = 0; i <=2; i++)
			if (i != currentState) PossibleStates.Add(i);
		
		int newState = PossibleStates[Random.RandomRange(0,2)];
		SetState(newState);
		currentState = newState;
		
		switch(currentState) 
		{
		  case 0:
			SoundManager.Instance.SetMusicSource(Music0);
			break;
		  case 1:
			SoundManager.Instance.SetMusicSource(Music1);
			break;
		  case 2:
			SoundManager.Instance.SetMusicSource(Music2);
			break;
		  default:
			SoundManager.Instance.SetMusicSource(Music0);
			break;
		}
	}


	public void SetState(int state){
		Debug.Log(state);
		for(int i = 0; i<=2;i++)
			if(i == state)transform.GetChild(i).gameObject.SetActive(true);
			else transform.GetChild(i).gameObject.SetActive(false);
			
		currentState = state;
	}
}
