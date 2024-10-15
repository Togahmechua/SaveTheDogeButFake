using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopCanvas : UICanvas
{
    [Header("----------List Item----------")]
    [SerializeField] private ShopItemConfig[] shopItemConfig;

    [Header("-----------References-----------")]
    [SerializeField] private TextMeshProUGUI moneyTxt;
    [SerializeField] private Transform shopContainer;
    [SerializeField] private ShopItem shopItem;

    private void OnEnable()
    {
        LevelManager.Ins.LoadMoney(moneyTxt);
    }

    private void Start()
    {
        GenerateShopItem();
    }

    private void GenerateShopItem()
    {
        for (int i = 0; i < shopItemConfig.Length; i++)
        {
            ShopItem item = Instantiate(shopItem, shopContainer);
            item.bgr.sprite = shopItemConfig[i].bgr;
            item.avt.image.sprite = shopItemConfig[i].spr;
        }
    }
}
