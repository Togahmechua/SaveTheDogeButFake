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
    [SerializeField] private bool isChadPlayed;

    private void OnEnable()
    {
        OnInit();    
    }

    private void Update()
    {
        HandleAnimations();
    }

    private void HandleAnimations()
    {
        if (LevelManager.Ins.timesUp && !isDed && !isChadPlayed)
        {
            AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("Idle") || stateInfo.normalizedTime >= 1f)
            {
                anim.SetTrigger(CacheString.TAG_Chad);
                Debug.Log("Chad");
                isChadPlayed = true;
            }
        }
        else
        {
            if (isNear && !previousIsNear && !isDed)
            {
                anim.SetTrigger(CacheString.TAG_IsSuprised);
                Debug.Log("Suprised");
                previousIsNear = true;
            }
            else if (!isNear && previousIsNear && !isDed)
            {
                anim.SetTrigger(CacheString.TAG_IDLE);
                Debug.Log("Idle");
                previousIsNear = false;
            }
        }
    }

    public void SetIsNear(bool value)
    {
        isNear = value;
    }

    public void OnInit()
    {
        Debug.Log("Reset");
        isNear = false;
        previousIsNear = false;
        isChadPlayed = false; 
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
            Debug.Log("IsAttacked");
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
