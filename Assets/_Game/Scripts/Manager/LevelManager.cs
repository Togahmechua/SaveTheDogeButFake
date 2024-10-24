using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager ins;
    public static LevelManager Ins => ins;

    [Header("------------Map&&Level------------")]
    public MapSO mapSO;
    public Level level;
    public List<Level> levelList = new List<Level>();
    public int curMap;
    [Header("------------Win&&Lose------------")]
    public LineCtrl lineRendererObj;
    public bool timesUp;
    public int curId;
    public bool isDed;
    [Header("------------GameDetails------------")]
    //Set avt for dog
    public int id;
    public int money;
    public bool isDrawed;

    private List<Level> curLevelList = new List<Level>();

    private void Awake()
    {
        id = PlayerPrefs.GetInt("ID", 0);
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
        // Reset data
        for (int i = 0; i < mapSO.mapList.Count; i++)
        {
            mapSO.mapList[i].isWon = false;
        }

        Debug.Log("Reset all win states");
    }

    public void StartGame()
    {
        level.playerCtrl.rb.simulated = true;
        for (int i = 0;i < level.ListBeehive.Count;i++)
        {
            level.ListBeehive[i].isActive = true;
        }
    }

    public void RestartGame()
    {
        level.playerCtrl.rb.simulated = false;
        for (int i = 0; i < level.ListBeehive.Count; i++)
        {
            level.ListBeehive[i].isActive = false;
        }
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
            SoundFXMNG.Ins.StopSFX();
            foreach (Level lv in curLevelList)
            {
                SimplePool.Despawn(lv);
            }
            lineRendererObj.gameObject.SetActive(false);
            for (int i = 0; i < level.ListBeehive.Count; i++)
            {
                level.ListBeehive[i].DeleteBee();
            }
            curLevelList.Clear();
            level = null;
            timesUp = false;
        }
    }

    public void LoadMoney(TextMeshProUGUI moneyText)
    {
        if (money >= 10000)
        {
            money = 9999;
        }
        moneyText.text = "x" + money.ToString();
        //Save
        PlayerPrefs.SetInt("Money", money);
        PlayerPrefs.Save();
    }

    public void LoadIDForPlayer(int num)
    {
        PlayerPrefs.SetInt("ID", num);
        PlayerPrefs.Save();  
        id = num; 
    }
}
