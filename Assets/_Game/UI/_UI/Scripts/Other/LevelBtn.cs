using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelBtn : MonoBehaviour
{
    public int id;
    public Image img;
    public Sprite[] spr;
    public Text txt;

    [SerializeField] private Button btn;

    private void Start()
    {
        btn.onClick.AddListener(LoadLevel);
    }

    private void LoadLevel()
    {
        LevelManager.Ins.LoadMapByID(id);
        UIManager.Ins.OpenUI<InGameCanvas>();
        UIManager.Ins.CloseUI<SelectLevelCanvas>();
    }
}