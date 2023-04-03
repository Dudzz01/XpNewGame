using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonChanger : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Button button;

    [Header("Properties")]
    [SerializeField] string sceneName;

    void Start()
    {
        button.onClick.AddListener(ChangeToScene);    
    }

    public void ChangeToScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}