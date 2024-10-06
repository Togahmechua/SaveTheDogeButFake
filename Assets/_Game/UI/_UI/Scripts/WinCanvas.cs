using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinCanvas : UICanvas
{
    [SerializeField] private ParticleSystem par;
    [SerializeField] private Image img;
    [SerializeField] private Sprite[] spr;

    private void OnEnable()
    {
        if (par != null)
        {
            par.Play();
        }

        //LevelManager.Ins.lineRendererObj.SetActive(false);
        LoadSpr();
    }

    private void LoadSpr()
    {
        if (UIManager.Ins.InGameCanvas.num == 0)
        {
            img.sprite = spr[0];
        }
        else if(UIManager.Ins.InGameCanvas.num == 1)
        {
            img.sprite = spr[1];
        }
        else if (UIManager.Ins.InGameCanvas.num == 2)
        {
            img.sprite = spr[2];
        }

    }
}
