using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Vulcano : MonoBehaviour
{
    [Header("References")]
    [SerializeField] VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer.loopPointReached += OnEndReached;
    }

    void OnEndReached(UnityEngine.Video.VideoPlayer vp)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}