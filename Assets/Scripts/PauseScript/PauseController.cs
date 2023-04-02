using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    [SerializeField] private GameObject pauseObject;
    private void Update() {

        if(Input.GetKeyDown(KeyCode.Escape) && GameOverController.gameContinue == true)
        {
            Time.timeScale = 0;
            pauseObject.SetActive(true);
        }
        
    }
}
