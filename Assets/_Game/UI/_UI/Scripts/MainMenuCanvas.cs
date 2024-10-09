using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCanvas : UICanvas
{
    [SerializeField] private Animator anim;
    private void OnEnable()
    {
        LevelManager.Ins.lineRendererObj.enabled = false;
        anim.Rebind();
    }
}
