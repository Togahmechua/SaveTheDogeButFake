using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCanvas : UICanvas
{
    private void OnEnable()
    {
        LevelManager.Ins.lineRendererObj.enabled = false;   
    }
}
