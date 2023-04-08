using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    [SerializeField] private GameObject pauseObject;

    void Start()
    {
        pauseObject = GameObject.Find("UI/Pause").GetComponent<Transform>().gameObject;
    }

    private void Update()
    {
        if(pauseObject == null)
        {
            pauseObject = GameObject.Find("UI/Pause").GetComponent<Transform>().gameObject;
        }

        if(Input.GetKeyDown(KeyCode.Escape) && GameOverController.gameContinue == true)
        {
            Time.timeScale = 0;
            pauseObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}