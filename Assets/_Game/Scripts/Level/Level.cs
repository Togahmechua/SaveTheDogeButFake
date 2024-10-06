using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : GameUnit
{
    public int id;
    public PlayerCtrl playerCtrl;
    public Beehive beehive;
    public Transform pos;

    private void OnEnable()
    {
        Debug.Log("Transform player to pos");
        playerCtrl.transform.position = pos.position;
    }
}
