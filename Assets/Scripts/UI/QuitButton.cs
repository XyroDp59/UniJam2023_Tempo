using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitButton : MonoBehaviour
{
    [SerializeField] private AudioClip MusicTitle;
    public void QuitGame()
    {
        Application.Quit();
    }
}