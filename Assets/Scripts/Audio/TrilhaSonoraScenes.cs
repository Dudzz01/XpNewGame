using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TrilhaSonoraScenes : MonoBehaviour
{
    [SerializeField] private AudioSource soundSource;
    [SerializeField] private AudioClip[] soundClip = new AudioClip[2];

    private bool switchMusic;
    private string scene;
    

    private void Update() {
        scene = SceneManager.GetActiveScene().name;

        DestroySoundObject();
    }

    public void DestroySoundObject()
    {
        if(scene == "Menu" || scene == "Tutorial" || scene == "Credits" )
        {
                if(switchMusic == false)
                {
                    soundSource.clip = soundClip[0];
                    soundSource.volume = 0.2f;
                    soundSource.Play();
                    switchMusic = true; 
                }
                
        }
        else
        {
                if(switchMusic == true)
                {
                    soundSource.clip = soundClip[1];
                    soundSource.volume = 0.1f;
                    soundSource.Play();
                    switchMusic = false;
                }
        }
         

    }

}
