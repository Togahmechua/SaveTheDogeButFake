using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBtn : MonoBehaviour
{
    [SerializeField] private Button btn;

    private void Start()
    {
        btn.onClick.AddListener(LoadSelectLevelCv);
    }

    private void LoadSelectLevelCv()
    {
        UIManager.Ins.CloseUI<MainMenuCanvas>();
        UIManager.Ins.OpenUI<SelectLevelCanvas>();
    }
}