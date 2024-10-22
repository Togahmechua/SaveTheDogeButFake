using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBtn : MonoBehaviour
{
    [SerializeField] private Button btn;
    private void Start()
    {
        btn.onClick.AddListener(OpenShop);
    }

    private void OpenShop()
    {
        SoundFXMNG.Ins.PlaySFX(SoundFXMNG.Ins.click);
        UIManager.Ins.CloseUI<MainMenuCanvas>();
        UIManager.Ins.OpenUI<ChangeSceneCanvas>();
        Observer.Notify("Wait", 2f, new Action(ChangeScene));
    }

    private void ChangeScene()
    {
        UIManager.Ins.OpenUI<ShopCanvas>();
        UIManager.Ins.CloseUI<ChangeSceneCanvas>();
    }
}
