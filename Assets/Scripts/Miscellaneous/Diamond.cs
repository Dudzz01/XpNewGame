using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Diamond : MonoBehaviour
{
    void OnEnable()
    {
        Sequence floatingSeq = DOTween.Sequence();

        floatingSeq.SetLoops(-1, LoopType.Yoyo);
        floatingSeq.SetEase(Ease.Linear);

        float dur = 0.25f;

        floatingSeq.Append(transform.DOLocalMoveY(transform.position.y - 0.1f, dur));
    }
}