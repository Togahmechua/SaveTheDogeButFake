using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    [SerializeField] private Text txt;

    private void OnEnable()
    {
        StartCoroutine(CountDown());
    }

    private IEnumerator CountDown()
    {
        int countdownTime = 10;
        while (countdownTime > 0)
        {
            // Update the UI text
            txt.text = countdownTime.ToString();

            yield return new WaitForSeconds(1f); 
            countdownTime--;
        }

        if (countdownTime <= 0)
        {
            SoundFXMNG.Ins.PlaySFX(SoundFXMNG.Ins.time);
            countdownTime = 0;
            txt.text = countdownTime.ToString();
            LevelManager.Ins.timesUp = true;
            if (!LevelManager.Ins.isDed)
            {
                UIManager.Ins.OpenUI<WinCanvas>();
            }
        }
    }
}
