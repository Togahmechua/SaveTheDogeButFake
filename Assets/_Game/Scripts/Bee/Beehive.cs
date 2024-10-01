using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beehive : MonoBehaviour
{
    [SerializeField] private List<Bee> beeList = new List<Bee>();
    [SerializeField] private Transform spawnPos;
    [SerializeField] private Bee bee;
    [SerializeField] private PlayerCtrl playerCtrl;

    private void Start()
    {
        
    }


    public void SpawnBee()
    {
        Bee b = SimplePool.Spawn<Bee>(bee, spawnPos);
        b.player = playerCtrl;
        beeList.Add(b);
    }

    public void DeleteBee()
    {
        foreach (Bee b in beeList)
        {
            SimplePool.Despawn(b);
        }
        beeList.Clear();
    }
}
