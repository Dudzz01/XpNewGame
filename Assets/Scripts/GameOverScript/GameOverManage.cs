using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverManage : MonoBehaviour
{
    public void RestartGame()
    {
        Debug.Log("Restart");
        PlayerController.playerIsAlive = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoMenu()
    {
        PlayerController.playerIsAlive = true;
        SceneManager.LoadScene("Menu");
    }
}
