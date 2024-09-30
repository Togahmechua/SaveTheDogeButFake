using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager ins;
    public static LevelManager Ins => ins;

    private void Awake()
    {
        LevelManager.ins = this;
    }
}
