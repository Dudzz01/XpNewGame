using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChooseOptionsMenu : MonoBehaviour
{
    
    public void SelectPlay()
    {
         
        SceneManager.LoadScene(""); // Dudu: Setar os nomes das cenas quando forem criadas
    }

     public void TutorialComandos()
     {
          SceneManager.LoadScene("");  // Dudu: Setar os nomes das cenas quando forem criadas
     }

    public void Creditos()
    {
        SceneManager.LoadScene(""); // Dudu: Setar os nomes das cenas quando forem criadas
    }

    public void Exit()
    {
        Application.Quit();
    }
}
