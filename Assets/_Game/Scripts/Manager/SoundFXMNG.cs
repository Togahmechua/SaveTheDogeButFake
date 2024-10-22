using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXMNG : MonoBehaviour
{
    private static SoundFXMNG ins;
    public static SoundFXMNG Ins => ins;

    [Header("-----------Audio Source-----------")]
    public AudioSource MusicSource;
    public AudioSource SFXSource;

    [Header("-----------Audio Clip-----------")]
    public AudioClip bgr;
    public AudioClip acid;
    public AudioClip bee;
    public AudioClip click;
    public AudioClip collectMoney;
    public AudioClip moneyEff;
    public AudioClip time;
    public AudioClip dead;
    public AudioClip hit;
    public AudioClip lose;
    public AudioClip win;

    private void Awake()
    {
        SoundFXMNG.ins = this;
    }

    private void Start()
    {
        MusicSource.clip = bgr;
        MusicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void StopSFX()
    {
        SFXSource.Stop();
    }

    public void TurnOff()
    {
        //this.enabled = false;
        SFXSource.volume = 0f;
        MusicSource.volume = 0f;
    }

    public void TurnOn()
    {
        //this.enabled = true;
        SFXSource.volume = 1f;
        MusicSource.volume = 0.7f;
    }
}
