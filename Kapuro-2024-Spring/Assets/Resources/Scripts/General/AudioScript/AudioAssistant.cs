using System;
using System.Diagnostics.Contracts;
using AudioController;
using UnityEngine;

public abstract class AudioAssistant : MonoBehaviour
{
    //�Đ�����Ώ�
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

    //�����ōĐ����邩
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

    [SerializeField] protected float volumeRate = 1.0f; //�{�����[���{��
    [SerializeField] protected float delay = 0.0f; //�Đ��J�n�̒x������
    [SerializeField] protected float pitch = 1.0f; //�s�b�`
    [SerializeField] protected float fadeInDuration = 0.0f; //�t�F�[�h�C������(0�Ȃ�t�F�[�h�C�����Ȃ�)
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

    //������
    protected virtual void Start()
    {
        if(isAutoPlay == true)
        {
            Play();
        }
    }

    //�Đ�
    public abstract void Play();
}

