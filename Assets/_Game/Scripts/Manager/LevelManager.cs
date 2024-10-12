using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager ins;
    public static LevelManager Ins => ins;

    public MapSO mapSO;
    public Level level;
    public List<Level> levelList = new List<Level>();
    public int curMap;
    public LineCtrl lineRendererObj;
    public bool timesUp;
    public bool isDed;
    public int curId;
    public int money;

    private List<Level> curLevelList = new List<Level>();

    private void Awake()
    {
        LevelManager.ins = this;
        OnInit();
    }

    public void OnInit()
    {
        curMap = PlayerPrefs.GetInt("CurrentMap", 0);
        money = PlayerPrefs.GetInt("Money", 0);
        mapSO.LoadWinStates();
    }

    public void ResetWinStates()
    {
        // Reset trạng thái chiến thắng cho tất cả các màn trong mapSO
        for (int i = 0; i < mapSO.mapList.Count; i++)
        {
            mapSO.mapList[i].isWon = false;
        }

        Debug.Log("Reset all win states");
    }

    public void StartGame()
    {
        level.playerCtrl.rb.simulated = true;
        level.beehive.isActive = true;
    }

    public void RestartGame()
    {
        level.playerCtrl.rb.simulated = false;
        level.beehive.isActive = false;
    }

    public void ResetMap()
    {
        DespawnMap();
        
        lineRendererObj.enabled = false;

        UIManager.Ins.CloseUI<InGameCanvas>();

        UIManager.Ins.OpenUI<ChangeSceneCanvas>();
        Observer.Notify("Wait", 2f, new Action(ChangeScene));
    }

    private void ChangeScene()
    {
        LoadMapByID(curId);

        UIManager.Ins.OpenUI<InGameCanvas>().OnIniT();
        UIManager.Ins.CloseUI<ChangeSceneCanvas>();
    }

    public void LoadMapByID(int id)
    {
        curId = id;
        isDed = false;
        if (level != null)
        {
            DespawnMap();
        }

        foreach (Level lv in levelList)
        {
            if (lv.id == id)
            {
                level = SimplePool.Spawn<Level>(levelList[id]);
                lineRendererObj.gameObject.SetActive(true);
                lineRendererObj.transform.position = new Vector3(0f, 0f, -2f);
                lineRendererObj.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                RestartGame();
                curLevelList.Add(level);
            }
        }
    }

    public void DespawnMap()
    {
        if (level != null)
        {
            foreach (Level lv in curLevelList)
            {
                SimplePool.Despawn(lv);
            }
            lineRendererObj.gameObject.SetActive(false);
            level.beehive.DeleteBee();
            curLevelList.Clear();
            level = null;
            timesUp = false;
        }
    }

    public void LoadMoney(TextMeshProUGUI moneyText)
    {
        moneyText.text = "x" + money.ToString();
        //Save
        PlayerPrefs.SetInt("Money", money);
    }
}
