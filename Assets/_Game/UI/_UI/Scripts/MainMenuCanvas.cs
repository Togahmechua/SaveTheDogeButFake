using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuCanvas : UICanvas
{
    [Header("-------------Anim-------------")]
    [SerializeField] private Animator anim;
    [Header("-------------UI-------------")]
    [SerializeField] private TextMeshProUGUI moneyTxt;
    [SerializeField] private Button fxBtn;
    [SerializeField] private Image soundImg;
    [SerializeField] private Sprite[] soundSpr;
    [SerializeField] private bool isOff;

    private void OnEnable()
    {
        LevelManager.Ins.lineRendererObj.enabled = false;
        anim.Rebind();
        LevelManager.Ins.LoadMoney(moneyTxt);
    }

    private void Start()
    {
        fxBtn.onClick.AddListener(Turn);
    }

    private void Turn()
    {
        isOff = !isOff;
        soundImg.sprite = soundSpr[isOff ? 1 : 0];
        if (isOff)
        {
            SoundFXMNG.Ins.TurnOff();
        }
        else
        {
            SoundFXMNG.Ins.TurnOn();
        }
    }
}
