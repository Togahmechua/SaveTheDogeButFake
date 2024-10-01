using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beehive : MonoBehaviour
{
    [Header("-------------------Bee List-------------------")]
    [SerializeField] private List<Bee> beeList = new List<Bee>();

    [Header("-------------------SpawnBeeSys-------------------")]
    [SerializeField] private Transform spawnPos;
    [SerializeField] private Bee bee;
    [SerializeField] private float amount;
    [SerializeField] private float spawnDelay;

    [Header("-------------------Other-------------------")]
    [SerializeField] private PlayerCtrl playerCtrl;
    [SerializeField] private float rangeToSpawn;

    private void Start()
    {
        StartCoroutine(SpawnBeesWithDelay());
    }

    private Vector3 RandomPoint()
    {
        Vector3 randomPosition = Random.insideUnitSphere * rangeToSpawn;
        Vector3 randomPos = new Vector3(randomPosition.x, randomPosition.z, 0);

        return transform.position + randomPos;
    }

    public void SpawnBee()
    {
        Vector3 spawnPosition = RandomPoint();
        Bee b = SimplePool.Spawn<Bee>(bee, spawnPosition, Quaternion.identity);
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

    private IEnumerator SpawnBeesWithDelay()
    {
        for (int i = 0; i < amount; i++)
        {
            SpawnBee();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(spawnPos.position, rangeToSpawn);
    }
}
