using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinCanvas : UICanvas
{
    [SerializeField] private ParticleSystem par;
    [SerializeField] private Image img;
    [SerializeField] private Sprite[] spr;
    [SerializeField] private Text txt;
    [SerializeField] private TextMeshProUGUI moneyTxt;
    [SerializeField] private Button collectBtn;
    [SerializeField] private Animator anim;

    private int[] rewards = { 25, 30, 40 };
    private bool isWin;

    private void Start()
    {
        if (collectBtn != null)
        {
            collectBtn.onClick.AddListener(CollectMoney);
        }
    }

    private void OnEnable()
    {
        if (isWin)
        {
            collectBtn.enabled = true;
            isWin = false;
        }

        if (par != null)
        {
            par.Play();
        }

        LoadSpr();
        LevelManager.Ins.LoadMoney(moneyTxt);
        SoundFXMNG.Ins.PlaySFX(SoundFXMNG.Ins.win);
    }

    private void CollectMoney()
    {
        SoundFXMNG.Ins.PlaySFX(SoundFXMNG.Ins.collectMoney);
        isWin = true;
        collectBtn.enabled = false;
        int num = UIManager.Ins.InGameCanvas.num;

        if (num < 0 || num >= rewards.Length)
        {
            Debug.LogWarning("Invalid num value in InGameCanvas.");
            return;
        }

        Sequence mySequence = DOTween.Sequence();
        mySequence.AppendCallback(() =>
        {
            anim?.SetTrigger(CacheString.TAG_Collect);
        });
        mySequence.AppendInterval(1.5f);
        mySequence.AppendCallback(() =>
        {
            LevelManager.Ins.money += rewards[num];
            LevelManager.Ins.LoadMoney(moneyTxt);
            SoundFXMNG.Ins.PlaySFX(SoundFXMNG.Ins.moneyEff);
        });
        mySequence.AppendInterval(1.3f);
        mySequence.AppendCallback(() =>
        {
            UIManager.Ins.CloseUI<WinCanvas>();
            UIManager.Ins.CloseUI<InGameCanvas>();
            LevelManager.Ins.DespawnMap();
            UIManager.Ins.OpenUI<ChangeSceneCanvas>();
            Observer.Notify("Wait", 2f, new Action(ChangeScene));
        });
        mySequence.Play();
    }

    private void ChangeScene()
    {
        if (LevelManager.Ins.curId < LevelManager.Ins.levelList.Count - 1)
        {
            // Load the next level
            LevelManager.Ins.LoadMapByID(++LevelManager.Ins.curId);
            UIManager.Ins.OpenUI<InGameCanvas>().OnIniT();
            UIManager.Ins.CloseUI<ChangeSceneCanvas>();
        }
        else
        {
            // Reached the last level
            Debug.Log("All levels completed!");
            UIManager.Ins.OpenUI<MainMenuCanvas>();
            UIManager.Ins.CloseUI<ChangeSceneCanvas>();
        }
    }


    private void LoadSpr()
    {
        int num = UIManager.Ins.InGameCanvas.num;

        if (num < 0 || num >= spr.Length)
        {
            Debug.LogWarning("Invalid num value or missing sprite in InGameCanvas.");
            return;
        }

        img.sprite = spr[num];
        txt.text = "x" + rewards[num];
    }
}
