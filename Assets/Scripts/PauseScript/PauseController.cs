using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject pauseObject;

    void Start()
    {
        pauseObject.SetActive(false);
    }

    void Update()
    {
        AssignObject();

        if(Input.GetKeyDown(KeyCode.Escape) && GameOverController.gameContinue == true)
        {
            Time.timeScale = 0;
            pauseObject.SetActive(true);
        }
    }

    void AssignObject()
    {
        if(pauseObject == null)
        {
            pauseObject = GameObject.Find("UI/PauseHolder").transform.gameObject;
            pauseObject.SetActive(false);
        }
    }
}