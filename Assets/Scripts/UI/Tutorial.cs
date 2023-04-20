using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using TMPro;

public class Tutorial : MonoBehaviour
{
    [Header("References")]
    [SerializeField] VideoPlayer video;
    [SerializeField] TextMeshProUGUI description;
    [Space(10)]
    [SerializeField] Button rightArrow;
    [SerializeField] Button leftArrow;
    [Space(10)]
    [SerializeField] Button menuButton;

    [Header("Properties")]
    [SerializeField] VideoClip[] videos;
    [SerializeField] string[] descriptions;
    [Space(10)]
    [SerializeField] int tipNumber;

    void Awake()
    {
        AssignReferences();
    }

    void Update()
    {
        ChangeTip();
        SetTipsLimit();
    }

    void AssignReferences()
    {
        leftArrow.onClick.AddListener(()=> tipNumber--);
        rightArrow.onClick.AddListener(()=> tipNumber++);
        menuButton.onClick.AddListener(ReturnMenu);
    }

    void ChangeTip()
    {
        video.clip = videos[tipNumber];
        description.text = descriptions[tipNumber];
    }

    void SetTipsLimit()
    {
        if(tipNumber == 4)
        {
            rightArrow.gameObject.SetActive(false);
            menuButton.gameObject.SetActive(true);
        }
        else if(tipNumber == 0)
        {
            leftArrow.gameObject.SetActive(false);
        }
        else
        {
            rightArrow.gameObject.SetActive(true);
            leftArrow.gameObject.SetActive(true);
            menuButton.gameObject.SetActive(false);
        }
    }

    void ReturnMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}