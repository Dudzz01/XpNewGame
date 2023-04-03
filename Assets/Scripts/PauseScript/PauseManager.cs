using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseManager : MonoBehaviour
{
    public void ContinueGame()
    {
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
    }

    public void GoMenu()
    {
        Time.timeScale = 1;
        //SpawnDiamonds.DiamondsPlayerCollect = 0;
        SceneManager.LoadScene("Menu");
    }
}
