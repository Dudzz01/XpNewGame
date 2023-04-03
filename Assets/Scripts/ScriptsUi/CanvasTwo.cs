using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasTwo : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject player4;
    [SerializeField] GameObject player5;
    [SerializeField] GameObject player6;
    [Space(10f)]
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject diamond;
    [SerializeField] Image blackCanvas;
    [SerializeField] SpriteRenderer bullet;
    
    private void Start() 
    {
        CloseFirstPanel();
    }

    public void CloseFirstPanel()
    {
        Sequence ndCanvas = DOTween.Sequence();

        ndCanvas.SetEase(Ease.Linear);

        ndCanvas.Append(blackCanvas.DOFade(0f,1f));

        ndCanvas.AppendCallback(()=>
        {
            player4.GetComponent<Animator>().SetBool("isWalking", false);
            player6.GetComponent<Animator>().SetBool("isWalking", true);
            player6.transform.DOMoveX(0f, 1f).OnComplete(()=> 
            {
                player6.GetComponent<Animator>().SetBool("isWalking", false);
                diamond.GetComponent<Image>().DOFade(0f, 1f);
            });

            bullet.transform.DOMoveX(-9.76f, 1f).OnComplete(()=> 
            {
                player5.GetComponent<Animator>().SetTrigger("dead");
                bullet.DOFade(0f,0.5f);
            });
        });

        ndCanvas.OnComplete(()=> StartCoroutine(passProxScene()));
    }

    IEnumerator passProxScene()
    {
        yield return new WaitForSeconds(6f);
        SceneManager.LoadScene("Introduction");
        yield return null;
    }
}