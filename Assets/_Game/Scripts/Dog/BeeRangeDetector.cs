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
    private bool previousIsNear;
    private bool isDed;

    [SerializeField] private Animator anim;

    private void Update()
    {
        HandleAnimations();
    }

    private void HandleAnimations()
    {
        if (isNear && !previousIsNear && !isDed)
        {
            anim.SetTrigger(CacheString.TAG_IsSuprised);
            previousIsNear = true;
        }
        else if (!isNear && previousIsNear && !isDed)
        {
            anim.SetTrigger(CacheString.TAG_IDLE);
            previousIsNear = false;
        }
    }

    public void SetIsNear(bool value)
    {
        isNear = value;
    }

    public void OnInit()
    {
        anim.SetTrigger(CacheString.TAG_IDLE);
        isNear = false;
        previousIsNear = false;
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
            ChangeAnim(CacheString.TAG_IsAttacked, true);
            isDed = true;
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
