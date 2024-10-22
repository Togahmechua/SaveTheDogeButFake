using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelBtn : MonoBehaviour
{
    public int id;
    public Image img;
    public Sprite[] spr;
    public Text txt;
    public Button btn;

    private void Start()
    {
        btn.onClick.AddListener(ChangeScene);
    }

    public void ChangeScene()
    {
        SoundFXMNG.Ins.PlaySFX(SoundFXMNG.Ins.click);
        UIManager.Ins.CloseUI<SelectLevelCanvas>();
        UIManager.Ins.OpenUI<ChangeSceneCanvas>();
        Observer.Notify("Wait", 2f, new Action(LoadLevel));
    }

    private void LoadLevel()
    {
        LevelManager.Ins.LoadMapByID(id);
        UIManager.Ins.OpenUI<InGameCanvas>().OnIniT();
        UIManager.Ins.CloseUI<ChangeSceneCanvas>();
    }
}
