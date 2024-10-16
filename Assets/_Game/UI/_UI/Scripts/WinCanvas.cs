using DG.Tweening;
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

    private void Start()
    {
        if (collectBtn != null)
        {
            collectBtn.onClick.AddListener(CollectMoney);
        }
    }

    private void OnEnable()
    {
        if (par != null)
        {
            par.Play();
        }

        LoadSpr();
        LevelManager.Ins.LoadMoney(moneyTxt);
    }

    private void CollectMoney()
    {
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
        });
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
