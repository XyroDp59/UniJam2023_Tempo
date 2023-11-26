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

    private void Start()
    {
        SoundManager.Instance.SetMusicSource(MusicTitle);
        StartCoroutine(Wait(0.66f));
        anim = GetComponent<Animator>();
        anim.Play("GameOverAnimation");
        parent = transform.parent.gameObject;
    }

    public void EndCredit()
    {
        GetComponent<Renderer>().enabled = false;
        StartCoroutine(Wait(0.5f));
        for (int i = 1; i <= 3; i += 1)
        {
           parent.transform.GetChild(i).gameObject.SetActive(true); 
        }
    }
    
    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
