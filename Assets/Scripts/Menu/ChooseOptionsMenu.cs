using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class ChooseOptionsMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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
        Vector3 negRot = new Vector3(0f, 0f, -5.125f);

        seq.Append(lightFocus.DOFade(.5f, 1f).SetEase(Ease.InOutBounce));

        seq.Join(gameName.rectTransform.DOLocalRotate(myRot, 1f).SetEase(Ease.InOutSine));
        seq.Append(gameName.rectTransform.DOLocalRotate(negRot, 1f).SetEase(Ease.InOutSine));
    }

    public void SelectPlay()
    {
        SceneManager.LoadScene("Level1"); // Dudu: Setar os nomes das cenas quando forem criadas
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("TutorialOne");  // Dudu: Setar os nomes das cenas quando forem criadas
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits"); // Dudu: Setar os nomes das cenas quando forem criadas
    }

    public void Exit()
    {
        Application.Quit();
    }

    void ChangeButtonTextColor()
    {

    }

    public void OnPointerEnter(PointerEventData data)
    {
        if(data != null)
        {
            var myText = data.pointerEnter.GetComponent<TextMeshProUGUI>();

            myText.transform.parent.DOLocalMoveX(-100f, .5f).SetEase(Ease.Linear);
        }
    }

    public void OnPointerExit(PointerEventData data)
    {
        if(data != null)
        {
            var myText = data.pointerEnter.GetComponent<TextMeshProUGUI>();

            myText.transform.parent.DOLocalMoveX(-128f, .5f).SetEase(Ease.Linear);
        }
    }
}