using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopCanvas : UICanvas
{
    [SerializeField] private TextMeshProUGUI moneyTxt;

    private void OnEnable()
    {
        LevelManager.Ins.LoadMoney(moneyTxt);
    }

}
