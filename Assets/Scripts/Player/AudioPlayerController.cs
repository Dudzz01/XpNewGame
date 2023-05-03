using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayerController : MonoBehaviour
{
    private GameObject playerReference;

    #region AudioPlayerVariables

    [Header("Audio")]
    [SerializeField] private AudioSource[] soundSource = new AudioSource[3];
    [SerializeField] private AudioClip[] soundClipPlayer = new AudioClip[3];

    private const float timeAudioStep = 0.2f;

    private float countTimeAudioStep;

    #endregion


    private void Update()
    {
        if(PlayerController.Instance == null)
        {
            Debug.Log("Player Reference é igual a null");
            return;
        }

        playerReference = PlayerController.Instance.gameObject;

        ManageSoundsPlayer();
    }

    public void ManageSoundsPlayer()
    {
        var player = playerReference.GetComponent<PlayerController>();

        if(PlayerController.playerIsAlive)
        {
            StepSoundPlayer(player);
            JumpSoundPlayer(player);

        
        }
        else
        {
            DeadSoundPlayer();
        }
            
        
           
        
    }

    public void StepSoundPlayer(PlayerController player)
    {

        if(player.IsGround && player.directionPlayerH != 0 && !soundSource[0].isPlaying) // se estiver no chao, em movimento, e estiver vivo, e o sfx nao estiver tocando...
        {
            soundSource[0].PlayOneShot(soundClipPlayer[0]);
        }
    }

    public void JumpSoundPlayer(PlayerController player)
    {
        if(Input.GetKeyDown(KeyCode.W) )
        {
            soundSource[1].PlayOneShot(soundClipPlayer[1]);
        }
    }

    public void DeadSoundPlayer()
    {
        soundSource[2].PlayOneShot(soundClipPlayer[2]);
    }
}
