using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InGameCanvas : UICanvas
{
    public Image fillBar;
    public int num;

    [SerializeField] private Text txt;
    [SerializeField] private Image img;
    [SerializeField] private Sprite[] spriteArr;
    [SerializeField] private Clock clock;

    private float fillBarDecreaseRate = 0.003f;

    private void OnEnable()
    {
        OnIniT();
        GetInGameCanvas();
    }

    public void OnIniT()
    {
        fillBar.fillAmount = 1f;
        clock.gameObject.SetActive(false);
        LevelManager.Ins.lineRendererObj.enabled = true;
        txt.text = "Level " + (LevelManager.Ins.level.id + 1);
    }

    public void ActiveClock()
    {
        clock.gameObject.SetActive(true);
    }

    public void GetInGameCanvas()
    {
        UIManager.Ins.InGameCanvas = this;
    }

    public void Update()
    {
        if (img != null)
        {
            if (fillBar.fillAmount <= 0.35f)
            {
                //1 sao
                img.sprite = spriteArr[0];
                num = 0;
            }
            else if (fillBar.fillAmount <= 0.7f && fillBar.fillAmount > 0.35f)
            {
                //2 sao
                img.sprite = spriteArr[1];
                num = 1;
            }
            else if (fillBar.fillAmount <= 1f && fillBar.fillAmount > 0.7f)
            {
                //3 sao
                img.sprite = spriteArr[2];
                num = 2;
            }
        }
    }

    public void DecreaseFillAmount()
    {
        fillBar.fillAmount -= fillBarDecreaseRate;
    }
}
