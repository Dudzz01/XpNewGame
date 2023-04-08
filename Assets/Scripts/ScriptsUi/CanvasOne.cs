using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CanvasOne : MonoBehaviour
{   
    [Header("Preferences")]

    [SerializeField] GameObject player1,player2,player3,player4,player5,player6,enemy,diamond;
    [SerializeField] Image blackCanvas;
    [SerializeField] Button mouseClick;

    [SerializeField] CanvasTwo canvasTwo;

    private void Awake()
    {
        ButtonClick();
    }
    
    private void Start()
    {
        blackCanvas.DOFade(0f,1f).OnComplete(()=>
        {
            showPresentation();
        });

        
    }

    void showPresentation()
    {
        Sequence intro = DOTween.Sequence();
        intro.SetEase(Ease.Linear);

        intro.AppendCallback(()=>
        {
            player1.GetComponent<Animator>().SetBool("isWalking",true);
            player2.GetComponent<Animator>().SetBool("isJumping",true);
            player3.GetComponent<Animator>().SetBool("isSliding",true);

        });

        intro.OnComplete(()=> StartCoroutine(passProxScene()));
    }   


    public void ButtonClick()
    {
        mouseClick.onClick.AddListener(()=> Debug.Log("CLicando"));
        
    }

    IEnumerator passProxScene()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("CanvasTwo");
        yield return null;
    }
    
    

    
}
