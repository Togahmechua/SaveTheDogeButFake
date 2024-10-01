using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bee : GameUnit
{
    public PlayerCtrl player;

    [SerializeField] private GameObject model;
    [SerializeField] private float radius;
    [SerializeField] private float speed;
    [SerializeField] private bool isActive;
    [SerializeField] private Rigidbody2D rb;

    private bool isWithinRadius = false;
    private float distanceToPlayer;

    private void Update()
    {
        CheckPlayerDistance();
        if (isActive)
        {
            MoveToPlayer();
        }
    }

    private void MoveToPlayer()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        rb.velocity = direction * speed;
    }

    private void CheckPlayerDistance()
    {
        distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);

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