using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuCanvas : UICanvas
{
    [SerializeField] private Animator anim;
    [SerializeField] private TextMeshProUGUI moneyTxt;
    private void OnEnable()
    {
        LevelManager.Ins.lineRendererObj.enabled = false;
        anim.Rebind();
        LevelManager.Ins.LoadMoney(moneyTxt);
    }
}
