using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackBtn : MonoBehaviour
{
    [SerializeField] private Button btn;

    private void Start()
    {
        btn.onClick.AddListener(Back);
    }

    private void Back()
    {
        UIManager.Ins.CloseUI<InGameCanvas>();
        UIManager.Ins.OpenUI<MainMenuCanvas>();
        LevelManager.Ins.DespawnMap();
    }
}
