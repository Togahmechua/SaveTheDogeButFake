using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InGameCanvas : UICanvas
{
    public Image fillBar;

    [SerializeField] private Image img;
    [SerializeField] private Sprite[] spriteArr;

    private float fillBarDecreaseRate = 0.003f;

    private void Start()
    {
        OnIniT();
        GetInGameCanvas();
    }

    public void OnIniT()
    {
        fillBar.fillAmount = 1f;
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
                img.sprite = spriteArr[0];
            }
            else if (fillBar.fillAmount <= 0.7f && fillBar.fillAmount > 0.35f)
            {
                img.sprite = spriteArr[1];
            }
            else if (fillBar.fillAmount <= 1f && fillBar.fillAmount > 0.7f)
            {
                img.sprite = spriteArr[2];
            }
        }
    }

    public void DecreaseFillAmount()
    {
        fillBar.fillAmount -= fillBarDecreaseRate;
    }
}
