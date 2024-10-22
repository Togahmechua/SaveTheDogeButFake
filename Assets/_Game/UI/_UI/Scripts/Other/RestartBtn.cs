using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartBtn : MonoBehaviour
{
    [SerializeField] private Button btn;

    private bool flag;

    private void OnEnable()
    {
        flag = true;
    }

    void Start()
    {
        btn.onClick.AddListener(Rs);
    }

    private void Rs()
    {
        SoundFXMNG.Ins.PlaySFX(SoundFXMNG.Ins.click);
        LevelManager.Ins.ResetMap();
        flag = false;
    }
}
