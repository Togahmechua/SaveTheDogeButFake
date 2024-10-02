using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public BeeRangeDetector beeRangeDetector;
    public Rigidbody2D rb;

    [SerializeField] private float approachRadius;

    public Vector3 RandomPlayerPos()
    {
        Vector2 randomOffset = Random.insideUnitCircle * approachRadius;
        Vector3 randomPos = transform.position + new Vector3(randomOffset.x, randomOffset.y, 0);

        return randomPos;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, approachRadius);
    }
}
