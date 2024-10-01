using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : GameUnit
{
    public PlayerCtrl player;

    [SerializeField] private GameObject model;
    [SerializeField] private float radius;

    private bool isWithinRadius = false;

    private void Update()
    {
        CheckPlayerDistance();
    }

    private void MoveToPlayer()
    {

    }

    private void CheckPlayerDistance()
    {
        float distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);

        if (distanceToPlayer < radius)
        {
            FaceToDog();
            if (!isWithinRadius)
            {
                isWithinRadius = true;
                player.beeRangeDetector.SetIsNear(true);
            }
        }
        else if (distanceToPlayer >= radius && isWithinRadius)
        {
            isWithinRadius = false;
            player.beeRangeDetector.SetIsNear(false);
        }
    }

    private void FaceToDog()
    {
        Vector3 playerPos = player.transform.position;
        Vector2 distance = playerPos - transform.position;
        float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;
        model.transform.rotation = Quaternion.Euler(new Vector3(0f, angle >= -90f && angle < 90f ? 0f : 180f, 90f));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}