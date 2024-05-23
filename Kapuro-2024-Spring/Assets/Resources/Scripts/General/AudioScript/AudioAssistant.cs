using System;
using System.Diagnostics.Contracts;
using AudioController;
using UnityEngine;

public abstract class AudioAssistant : MonoBehaviour
{
    //再生する対象
    [SerializeField] protected AudioClip audioClip = null;
    public AudioClip AudioClip
    {
        get
        {
            return AudioClip;
        }
        set
        {
            AudioClip = value;
        }
    }

    //自動で再生するか
    [SerializeField] protected bool isAutoPlay = false;
    public bool IsAudioPlay
    {
        get
        {
            return isAutoPlay;
        }
        set
        {
            IsAudioPlay = value;
        }
    }

    [SerializeField] protected float volumeRate = 1.0f; //ボリューム倍率
    [SerializeField] protected float delay = 0.0f; //再生開始の遅延時間
    [SerializeField] protected float pitch = 1.0f; //ピッチ
    [SerializeField] protected float fadeInDuration = 0.0f; //フェードイン時間(0ならフェードインしない)
    public float VolumeRate
    {
        get
        {
            return volumeRate;
        }
        set
        {
            volumeRate = value;
        }
    }

    public float Delay
    {
        get
        {
            return delay;
        }
        set
        {
            delay = value;
        }
    }

    public float Pitch
    {
        get
        {
            return pitch;
        }
        set
        {
            pitch = value;
        }
    }

    public float FadeInDuration
    {
        get
        {
            return fadeInDuration;
        }
        set
        {
            fadeInDuration = value;
        }
    }

    //初期化
    protected virtual void Start()
    {
        if(isAutoPlay == true)
        {
            Play();
        }
    }

    //再生
    public abstract void Play();
}

