using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLevelCanvas : UICanvas
{
    [SerializeField] private int gateNum;
    [SerializeField] private List<LevelBtn> lvBtn = new List<LevelBtn>();
    [SerializeField] private SpawnLevel child;

    private void OnEnable()
    {
        GetInGameCanvas();
        LoadLevel();
    }

    public void GetInGameCanvas()
    {
        UIManager.Ins.SelectLevelCanvas = this;
    }

    public void LoadLevel()
    {
        child.Check();
    }

    public void ResetAllGate()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        lvBtn.Clear();
        for (int i = 0; i < child.transform.childCount; i++)
        {
            LevelBtn levelBtn = child.transform.GetChild(i).GetComponent<LevelBtn>();
            if (levelBtn != null)
            {
                lvBtn.Add(levelBtn);
            }
        }
    }
}
