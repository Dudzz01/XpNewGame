using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class CanvasOne : MonoBehaviour
{   
    [Header("Preferences")]

    [SerializeField] GameObject player1,player2,player3;
    [SerializeField] Image blackCanvas;
    [SerializeField] Button mouseClick;

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
    }

    void ButtonClick()
    {
        //mouseClick.onClick.AddListener();
    }
    

    
}
