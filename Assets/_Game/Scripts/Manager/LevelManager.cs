using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager ins;
    public static LevelManager Ins => ins;
    public Level level;
    public List<Level> levelList = new List<Level>();

    private List<Level> curLevelList = new List<Level>();
    public int num;

    private void Awake()
    {
        LevelManager.ins = this;
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

    public void LoadMapByID(int id)
    {
        if (level != null)
        {
            DespawnMap();
        }

        foreach (Level lv in levelList)
        {
            if (lv.id == id)
            {
                level = SimplePool.Spawn<Level>(levelList[id]);
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
            curLevelList.Clear();
            level = null;
        }
    }
}
