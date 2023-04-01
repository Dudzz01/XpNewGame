using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MobilePlatforms : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] Vector2 destination;
    [SerializeField] float duration;

    void Start()
    {
        MovePlatform();
    }

    void MovePlatform()
    {
        transform.DOMove(destination, duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }
}