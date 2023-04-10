﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
    public static bool gameContinue; // esse é o game over
    [SerializeField]private GameObject gameOverObject;

    private float durGameOver;

    private void Start()
    {
        durGameOver = 0;
        gameOverObject.SetActive(false);
    }

    private void Update()
    {
        VerifyGameOver();
        AssignObject();
    }

    void AssignObject()
    {
        if(gameOverObject == null)
        {
            gameOverObject = GameObject.Find("UI/GameOverHolder").transform.gameObject;
            gameOverObject.SetActive(false);
        }
    }

    public void VerifyGameOver()
    {
        gameContinue = PlayerController.playerIsAlive;

        if(gameContinue == false && gameOverObject != null)
        {
            durGameOver += Time.deltaTime;

            if(durGameOver >= 1.7f) // se o player nao esta mais vivo, o game over receberá false
            {
                //Debug.Log(gameContinue);
                gameOverObject.SetActive(true);
            } 
        }
    }
}