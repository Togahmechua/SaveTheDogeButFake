using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class BeeRangeDetector : MonoBehaviour
{
    [Header("Bee List")]
    [SerializeField] private List<Bee> beeList = new List<Bee>();

    [Header("Collider2D")]
    [SerializeField] private CircleCollider2D box;

    [Header("Animator")]
    public bool isNear;

    [SerializeField] private Animator anim;

    private void Update()
    {
        if (isNear)
        {
            anim.SetTrigger(CacheString.TAG_IsSuprised);
            isNear = false;
        }
    }

    public void ChangeAnim(string currentAnim, bool isActive)
    {
        if (anim != null)
        {
            anim.SetBool(currentAnim, isActive);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Bee bee = Cache.GetBee(other);
        if (bee != null)
        {
            beeList.Add(bee);
            anim.SetTrigger(CacheString.TAG_IsAttacked);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Bee bee = Cache.GetBee(other);
        if (bee != null)
        {
            beeList.Remove(bee);
        }
    }
}
