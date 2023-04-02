using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MobilePlatforms : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] Vector2 destination;
    [SerializeField] float duration;
    [SerializeField] bool isAutomatic;

    void Start()
    {
        MovePlatform();
    }

    void MovePlatform()
    {
        if(isAutomatic)
        {
            transform.DOMove(destination, duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(isAutomatic == false)
        {
            if(col.collider.name == "Player")
            {
                transform.DOMove(destination, duration).SetEase(Ease.Linear);
            }
        }
    }
}