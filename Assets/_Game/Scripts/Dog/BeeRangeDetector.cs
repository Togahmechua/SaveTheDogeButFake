using DG.Tweening;
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
    private bool flag;
    private bool flag2;
    private bool touchAxit;

    [Header("Animator")]
    public bool isNear;
    [SerializeField] private bool previousIsNear;
    [SerializeField] private bool isDed;

    [SerializeField] private Animator anim;
    [SerializeField] private bool isChadPlayed;

    private void OnEnable()
    {
        OnInit();
        flag = true;
        flag2 = true;
        touchAxit = false;
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
                //Debug.Log("Chad");
                isChadPlayed = true;
            }
        }
        else
        {
            if (isNear && !previousIsNear && !isDed)
            {
                anim.SetTrigger(CacheString.TAG_IsSuprised);
                //Debug.Log("Suprised");
                previousIsNear = true;
            }
            else if (!isNear && previousIsNear && !isDed)
            {
                anim.SetTrigger(CacheString.TAG_IDLE);
                //Debug.Log("Idle");
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
        //Debug.Log("Reset");
        isNear = false;
        previousIsNear = false;
        isChadPlayed = false; 
        isDed = false;
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
        if (LevelManager.Ins.timesUp)
            return;
        Rock rock = Cache.GetRock(other);
        Bee bee = Cache.GetBee(other);
        if (bee != null || rock != null)
        {
            beeList.Add(bee);
            ChangeAnim(CacheString.TAG_IsAttacked, true);
            if (flag2)
            {
                SoundFXMNG.Ins.PlaySFX(SoundFXMNG.Ins.hit);
                SoundFXMNG.Ins.PlaySFX(SoundFXMNG.Ins.lose);
                flag2 = false;
            }
            
            //Debug.Log("IsAttacked");
            isDed = true;
            LevelManager.Ins.isDed = true;
            if (flag)
            {
                Sequence mySequence = DOTween.Sequence();
                mySequence.AppendInterval(3f);
                mySequence.AppendCallback(() =>
                {
                    LevelManager.Ins.ResetMap();
                    if (touchAxit)
                    {
                        transform.parent.gameObject.SetActive(true);
                    }
                });
                mySequence.Play();
                flag = false;
            }

            return;
        }

        Acid acit = Cache.GetAxit(other);
        if (acit != null)
        {
            touchAxit = true;
            ParticlePool.Play(ParticleType.SmokeEff, other.transform.position, Quaternion.identity);
            SoundFXMNG.Ins.PlaySFX(SoundFXMNG.Ins.acid);
            LevelManager.Ins.isDed = true;
            isDed = true;

            if (flag)
            {
                Sequence mySequence = DOTween.Sequence();

                mySequence.AppendCallback(() =>
                {
                    transform.parent.gameObject.SetActive(false);
                });
                mySequence.AppendInterval(1.5f);
                mySequence.AppendCallback(() =>
                {
                    LevelManager.Ins.ResetMap();
                });
                mySequence.AppendInterval(0.5f);
                mySequence.AppendCallback(() =>
                {
                    transform.parent.gameObject.SetActive(true);
                });

                mySequence.Play();
                flag = false;
            }
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
