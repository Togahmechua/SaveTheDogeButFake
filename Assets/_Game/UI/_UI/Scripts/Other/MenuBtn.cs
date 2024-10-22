using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBtn : MonoBehaviour
{
    [SerializeField] private Button btn;

    private void Start()
    {
        btn.onClick.AddListener(LoadSelectLevelCv);
    }

    private void LoadSelectLevelCv()
    {
        SoundFXMNG.Ins.PlaySFX(SoundFXMNG.Ins.click);
        UIManager.Ins.CloseUI<MainMenuCanvas>();
        UIManager.Ins.OpenUI<ChangeSceneCanvas>();
        Observer.Notify("Wait", 2f, new Action(ChangeScene));
    }

    private void ChangeScene()
    {
        UIManager.Ins.OpenUI<SelectLevelCanvas>();
        UIManager.Ins.CloseUI<ChangeSceneCanvas>();
    }
}
