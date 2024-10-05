using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLevel : MonoBehaviour
{
    [SerializeField] private Transform spawnPos;
    [SerializeField] private int spawnCount;
    [SerializeField] private LevelBtn levelPrefab;
    [SerializeField] private List<LevelBtn> levelBtnList = new List<LevelBtn>();

    private void Awake()
    {
        foreach (Transform child in spawnPos)
        {
            Destroy(child.gameObject);
        }

        levelBtnList.Clear();

        for (int i = 0; i < spawnCount; i++)
        {
            LevelBtn levelBtn = Instantiate(levelPrefab, spawnPos);
            levelBtnList.Add(levelBtn);
            levelBtn.id = i;

            if (levelBtn.id == 0)
            {
                levelBtn.img.sprite = levelBtn.spr[1];
                levelBtn.txt.text = (levelBtn.id + 1).ToString();
            }
        }
    }
}
