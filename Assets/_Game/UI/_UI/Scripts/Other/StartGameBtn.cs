using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGameBtn : MonoBehaviour
{
    [SerializeField] private Button startBtn;

    private void Start()
    {
        startBtn.onClick.AddListener(LoadCurMap);
    }

    public void LoadCurMap()
    {
        SoundFXMNG.Ins.PlaySFX(SoundFXMNG.Ins.click);
        UIManager.Ins.CloseUI<MainMenuCanvas>();
        UIManager.Ins.OpenUI<ChangeSceneCanvas>();
        Observer.Notify("Wait", 2f, new Action(ChangeScene));
    }

    private void ChangeScene()
    {
        LevelManager.Ins.LoadMapByID(LevelManager.Ins.curMap);
        UIManager.Ins.OpenUI<InGameCanvas>().OnIniT();
        UIManager.Ins.CloseUI<ChangeSceneCanvas>();
    }
}
