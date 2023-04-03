using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class Congratulations : MonoBehaviour
{
    [Header("References")]
    [SerializeField] SpriteRenderer door;
    [SerializeField] Animator doorAnim;
    [SerializeField] GameObject player;
    [SerializeField] SpriteRenderer ruby;
    [SerializeField] TextMeshProUGUI textCong;

    void Start()
    {
        CongratsAnimation();
    }

    void CongratsAnimation()
    {
        Sequence seq = DOTween.Sequence();
        seq.SetEase(Ease.Linear);

        Vector2 myScale = new Vector2(20f, 20f);

        seq.Append(door.gameObject.transform.DOMoveY(-1.75f, 1f));
        seq.Join(door.gameObject.transform.DOScale(myScale, 1f));
        seq.AppendCallback(()=> doorAnim.enabled = true);
        seq.InsertCallback(2f, ()=> door.enabled = false);

        seq.Append(player.transform.DOMoveX(0f, 5f));
        seq.InsertCallback(3f, ()=> {
            player.GetComponent<Animator>().SetBool("isWalking", true);
            ruby.enabled = true;
        });

        seq.AppendCallback(()=> player.GetComponent<Animator>().SetBool("isWalking", false));
        seq.Append(textCong.DOFade(1f, 1f));
    }
}