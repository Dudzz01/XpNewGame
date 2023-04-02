using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasTwo : MonoBehaviour
{

    [Header("Preferences")]

    [SerializeField] GameObject player4,player5,player6,enemy,diamond;
    [SerializeField] Image blackCanvas;
    [SerializeField] SpriteRenderer bullet;
    
    private void Start() 
    {
        closeFirstPanel();
    }

   public void closeFirstPanel()
    {
        Sequence ndCanvas = DOTween.Sequence();

        ndCanvas.SetEase(Ease.Linear);

        ndCanvas.Append(blackCanvas.DOFade(0f,1f));

        ndCanvas.AppendCallback(()=>
        {
            
            player4.GetComponent<Animator>().SetBool("isWalking",false);
            player6.transform.DOMoveX(player6.transform.position.x+10,1f).OnComplete(()=>diamond.GetComponent<Image>().DOFade(0f,1f));
            bullet.transform.DOMoveX(-9.76f,1f).OnComplete(()=> {
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
