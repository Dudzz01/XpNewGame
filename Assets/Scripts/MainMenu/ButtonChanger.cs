using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonChanger : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Button myButton;

    void Start()
    {
        StartCoroutine(Waiter());
    }

    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Menu");
    }

/*    void Awake()
    {
        myButton.onClick.AddListener(ChangeToScene);    
        print("iniciou");
    }

    public void ChangeToScene()
    {
        SceneManager.LoadScene("Menu");
        Debug.Log("Clicou?");
    }
*/
}