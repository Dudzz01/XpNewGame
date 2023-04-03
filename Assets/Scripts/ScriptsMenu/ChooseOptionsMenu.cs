using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ChooseOptionsMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Image gameName;
    [SerializeField] Image lightFocus;

    void Start()
    {
        AnimDisplay();
    }

    void AnimDisplay()
    {
        Sequence seq = DOTween.Sequence();
        //seq.SetEase(Ease.InOutSine);
        seq.SetLoops(-1, LoopType.Yoyo);

        Vector3 myRot = new Vector3(0f, 0f, 5.125f);

        seq.Append(lightFocus.DOFade(.5f, 1f).SetEase(Ease.InOutBounce));
        seq.Join(gameName.rectTransform.DOLocalRotate(myRot, 1f).SetEase(Ease.InOutSine).OnComplete(()=> gameName.rectTransform.DOLocalRotate(-myRot, 1f).SetEase(Ease.InOutSine)));
        seq.Join(gameName.rectTransform.DOLocalMoveY(18f, 1f).SetEase(Ease.Linear));
    }

    public void SelectPlay()
    {
        SceneManager.LoadScene("Introduction"); // Dudu: Setar os nomes das cenas quando forem criadas
    }

     public void TutorialComandos()
     {
        SceneManager.LoadScene("CanvasOne");  // Dudu: Setar os nomes das cenas quando forem criadas
     }

    public void Creditos()
    {
        SceneManager.LoadScene("Credits"); // Dudu: Setar os nomes das cenas quando forem criadas
    }

    public void Exit()
    {
        Application.Quit();
    }
}