using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameCanvas : UICanvas
{
    [SerializeField] private Image img;
    [SerializeField] private Image fillBar;
    [SerializeField] private Sprite[] spriteArr;

    private void Start()
    {
        OnINit();
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

    private void OnINit()
    {
        fillBar.fillAmount = 1;
    }
}
