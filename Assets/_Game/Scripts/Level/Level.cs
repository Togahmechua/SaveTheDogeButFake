using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : GameUnit
{
    public int id;
    public PlayerCtrl playerCtrl;
    public Beehive beehive;
    public Transform pos;
    public ELevel eLevl;

    private bool win;

    private void OnEnable()
    {
        Debug.Log("Transform player to pos");
        playerCtrl.transform.position = pos.position;
        playerCtrl.transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    private void Update()
    {
        if (LevelManager.Ins.timesUp)
        {
            // So sánh và cập nhật trạng thái thắng của map
            if (eLevl == LevelManager.Ins.mapSO.mapList[LevelManager.Ins.curMap].eLevel &&
                !LevelManager.Ins.mapSO.mapList[LevelManager.Ins.curMap].isWon)
            {
                LevelManager.Ins.mapSO.mapList[LevelManager.Ins.curMap].isWon = true;
                SaveWinState(LevelManager.Ins.curMap);
                Debug.Log("Map " + LevelManager.Ins.curMap + " is won.");
                LevelManager.Ins.curMap++;
            }
            win = true;
            SetCurMap();
        }
    }

    private void SetCurMap()
    {
        PlayerPrefs.SetInt("CurrentMap", LevelManager.Ins.curMap);
        PlayerPrefs.Save();
    }

    private void SaveWinState(int mapIndex)
    {
        string key = "MapWin_" + mapIndex;
        PlayerPrefs.SetInt(key, 1); // Lưu lại trạng thái thắng của map
        PlayerPrefs.Save();
        LevelManager.Ins.mapSO.LoadWinStates();
    }
}
