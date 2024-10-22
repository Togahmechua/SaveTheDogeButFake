using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopCanvas : UICanvas
{
    [Header("----------List Item----------")]
    [SerializeField] private List<ShopItem> listShopIT = new List<ShopItem>();
        
    [Header("-----------References-----------")]
    [SerializeField] private TextMeshProUGUI moneyTxt;
    [SerializeField] private Transform shopContainer;
    [SerializeField] private Animator anim;
    [SerializeField] private int count;

    private GameObject ItemTemplate;
    private GameObject g;
    private Button buyBtn;

    [Serializable]
    public class ShopItem
    {
        public ShopItemConfig shopItemConfig;
        public bool isBought;
        public bool isEquip;
    }

    private void OnEnable()
    {
        LoadShopState();
        LevelManager.Ins.LoadMoney(moneyTxt);
    }

    private void Start()
    {
        count = PlayerPrefs.GetInt("Num", 0);
        GenerateShopItem();
    }

    private void GenerateShopItem()
    {
        ItemTemplate = shopContainer.GetChild(0).gameObject;
        int len = listShopIT.Count;

        for (int i = 0; i < len; i++)
        {
            g = Instantiate(ItemTemplate, shopContainer);

            // Load saved states
            listShopIT[i].isBought = PlayerPrefs.GetInt("ShopItemBought_" + i, 0) == 1;
            listShopIT[i].isEquip = PlayerPrefs.GetInt("ShopItemEquip_" + i, 0) == 1;

            if (i == 0 && count == 0)
            {
                listShopIT[i].isBought = true;
                listShopIT[i].isEquip = true;
                PlayerPrefs.SetInt("ShopItemBought_" + i, 1);
                PlayerPrefs.SetInt("ShopItemEquip_" + i, 1);
                count++;
                PlayerPrefs.SetInt("Num", count);
                PlayerPrefs.Save();
            }

            // Update UI
            g.transform.GetChild(0).GetComponent<Image>().sprite = listShopIT[i].shopItemConfig.bgr;
            g.transform.GetChild(2).gameObject.SetActive(!listShopIT[i].isBought);
            g.transform.GetChild(0).gameObject.SetActive(listShopIT[i].isEquip);

            // Button setup
            buyBtn = g.transform.GetChild(1).GetComponent<Button>();
            buyBtn.image.sprite = listShopIT[i].shopItemConfig.spr;
            buyBtn.AddEventListener(listShopIT[i].shopItemConfig.id, OnShopItemBtnClicked);
        }

        Destroy(ItemTemplate);
    }

    private void OnShopItemBtnClicked(int itemIndex)
    {
        Debug.Log(itemIndex);
        SoundFXMNG.Ins.PlaySFX(SoundFXMNG.Ins.click);
        // Purchase logic
        if (LevelManager.Ins.money >= 500 && !listShopIT[itemIndex].isBought)
        {
            LevelManager.Ins.money -= 500;
            LevelManager.Ins.LoadMoney(moneyTxt);
            listShopIT[itemIndex].isBought = true;
            PlayerPrefs.SetInt("ShopItemBought_" + itemIndex, 1);

            shopContainer.GetChild(itemIndex).GetChild(2).gameObject.SetActive(false);
        }
        else if (LevelManager.Ins.money < 500)
        {
            anim.SetTrigger(CacheString.TAG_NoCoin);
            return;
        }

        // Equip logic
        for (int i = 0; i < listShopIT.Count; i++)
        {
            Transform tf = shopContainer.GetChild(i);
            listShopIT[i].isEquip = false;
            PlayerPrefs.SetInt("ShopItemEquip_" + i, 0);
            tf.GetChild(0).gameObject.SetActive(false);
        }

        // Equip selected item
        listShopIT[itemIndex].isEquip = true;
        PlayerPrefs.SetInt("ShopItemEquip_" + itemIndex, 1);
        shopContainer.GetChild(itemIndex).GetChild(0).gameObject.SetActive(true);

        count = itemIndex;
        TransInfoToLvMNG();
        PlayerPrefs.SetInt("Num", count);
        PlayerPrefs.Save();
    }

    private void LoadShopState()
    {
        bool anyItemEquipped = false;

        for (int i = 0; i < listShopIT.Count; i++)
        {
            listShopIT[i].isBought = PlayerPrefs.GetInt("ShopItemBought_" + i, 0) == 1;
            listShopIT[i].isEquip = PlayerPrefs.GetInt("ShopItemEquip_" + i, 0) == 1;

            if (listShopIT[i].isEquip)
            {
                anyItemEquipped = true;
            }
        }

        if (!anyItemEquipped)
        {
            listShopIT[0].isBought = true;
            listShopIT[0].isEquip = true;
            PlayerPrefs.SetInt("ShopItemBought_0", 1);
            PlayerPrefs.SetInt("ShopItemEquip_0", 1);
        }
    }

    private void TransInfoToLvMNG()
    {
        LevelManager.Ins.LoadIDForPlayer(count);
    }
}
