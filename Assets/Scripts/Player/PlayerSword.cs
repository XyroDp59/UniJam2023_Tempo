using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    [SerializeField] private float attackSpeed = 0.5f;
    private float attackTiming = 0.2f;
    private float rechargeTime;
	private GameObject swingObj;
	private Animator animSwing;
    private bool swinging;
	[SerializeField] private AudioClip sound;
	[SerializeField] private float volume;
  
	void Start(){
		swingObj = gameObject.transform.GetChild(0).gameObject;
		animSwing = swingObj.GetComponent<Animator>();
	}
	
    void Update()
    {
        rechargeTime += Time.deltaTime;
      
        if (Input.GetMouseButton(0) && rechargeTime >= attackSpeed)
        {
			SoundManager.Instance.PlaySound(sound, volume);
            swingObj.SetActive(true);
            rechargeTime = 0;
            swinging = true;
        }
		if (swinging) animSwing.Play("SwordSlash");


        if (rechargeTime >= attackTiming && swinging)
        {
            swingObj.SetActive(false);
            swinging = false;
        }
    }
}
