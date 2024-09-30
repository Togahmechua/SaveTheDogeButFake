using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoObserver : MonoBehaviour
{
    void Start()
    {
        Observer.AddObserver("Wait", WaitASec);
    }

    private void OnDestroy()
    {
        Observer.RemoveListener("Wait", WaitASec);
    }

    private void WaitASec(object[] datas)
    {
        if (datas.Length < 2 || !(datas[1] is Action))
        {
            Debug.LogError("Invalid parameters passed to WaitASec");
            return;
        }

        float interval = (float)datas[0];
        Action callback = (Action)datas[1];

        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(interval);
        sequence.AppendCallback(() =>
        {
            callback();
        });

        sequence.Play();
    }
}
