namespace AudioController
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class AudioPlayer
    {
        private readonly AudioSource audioSource; //�Đ��p�̃\�[�X

        public float PlayedTime => audioSource.time; //�Đ���������

        public string CurrentAudioName => audioSource.clip == null ? "" : audioSource.clip.name; //�Đ����̃I�[�f�B�I�̖��O

        private Action callback; //�Đ��I����̏���

        //���
        public enum State
        {
            WAIT,
            DELAY,
            PLAYING,
            PAUSE,
            FADING
        }

        private State currentState = State.WAIT;
        public State CurrentState => currentState;

        //�{�����[���̊�Ɣ{��
        private float baseVolume;
        private float volumeRate;
        public float CurrentVolume => baseVolume * volumeRate;

        //�Đ��܂ł̑҂�����
        private float initialDelay;
        private float currentDelay;
        public float ElapsedDelay => initialDelay - currentDelay;

        //�t�F�[�h�֌W
        private float fadeProgress;
        private float fadeDuration;
        private float fadeFrom;
        private float fadeTo;
        private Action fadeCallback;

        /*������*/
        public AudioPlayer(AudioSource audioSource)
        {
            this.audioSource = audioSource;
            this.audioSource.playOnAwake = false;
        }

        /*�A�b�v�f�[�g*/
        public void Update()
        {
            //�I������
            if(currentState == State.PLAYING && !audioSource.isPlaying && Mathf.Approximately(audioSource.time, 0))
            {
                Finish();
            }
            //�Đ��O�̑ҋ@
            else if(currentState == State.DELAY)
            {
                Delay();
            }
            //�t�F�[�h
            else if(currentState == State.FADING)
            {
                Fade();
            }
        }

        private void Delay()
        {
            currentDelay -= Time.deltaTime;
            if(currentDelay > 0)
            {
                return;
            }

            audioSource.Play();

            if(fadeDuration > 0)
            {
                currentState = State.FADING;
                Update();
            }
            else
            {
                currentState = State.PLAYING;
            }
        }

        private void Fade()
        {
            fadeProgress += Time.deltaTime;
            float timeRate = Mathf.Min(fadeProgress / fadeDuration, 1);

            audioSource.volume = GetVolume() * (fadeFrom * (1 - timeRate) + fadeTo * timeRate);

            if (timeRate < 1)
            {
                return;
            }

            if(fadeTo <= 0)
            {
                Finish();
            }
            else
            {
                currentState = State.PLAYING;
            }

            fadeCallback?.Invoke();
        }

        /*�ݒ�Ȃ�*/
        //�{�����[����ύX
        public void ChangeVolume(float baseVolume)
        {
            this.baseVolume = baseVolume;
            audioSource.volume = GetVolume();
        }

        //volumeRate��ύX
        public void ChangeVolumeRate(float volumeRate)
        {
            this.volumeRate = volumeRate;
            audioSource.volume = GetVolume();
        }

        //�{�����[�����擾
        private float GetVolume()
        {
            return baseVolume * volumeRate;
        }

        /*�Đ��J�n*/
        public void Play(AudioClip audioClip, float baseVolume, float volumeRate, float delay, float pitch, bool isLoop, Action callback = null)
        {
            //��~���łȂ���Β�~
            if(currentState != AudioPlayer.State.WAIT)
            {
                Stop();
            }
            audioSource.Stop();

            this.volumeRate = volumeRate;
            ChangeVolume(baseVolume);

            initialDelay = delay;
            currentDelay = initialDelay;

            audioSource.pitch = pitch;
            audioSource.loop = isLoop;
            this.callback = callback;

            audioSource.clip = audioClip;
           
            currentState = currentDelay > 0 ? State.DELAY : State.PLAYING;
            if (currentState == State.PLAYING)
            {
                audioSource.Play();
            }

            //���[�v�Đ��łȂ���΁A�Đ��C���̃`�F�b�N������
            if (audioSource.loop)
            {
                return;
            }

            //�|�[�Y����Ă����炷���Ɏ~�߂�
            if(currentState == State.PAUSE)
            {
                Pause();
            }
        }

        /*�Đ��I��*/
        //�w�肳�ꂽ���O�̂��̂��Đ����Ă������~
        public void Stop(string audioName)
        {
            if(audioName == CurrentAudioName)
            {
                Stop();
            }
        }

        //�Đ����~
        public void Stop()
        {
            callback = null;
            Finish();
        }

        //�Đ��I��
        private void Finish()
        {
            currentState = State.WAIT;

            audioSource.Stop();
            audioSource.clip = null;

            initialDelay = 0;
            currentDelay = 0;
            fadeDuration = 0;

            callback?.Invoke();
        }

        /*�ꎞ��~�ƍĊJ*/
        //�w�肳�ꂽ���O�̂��̂��Đ����Ă�����ꎞ��~
        public void Pause(string audioName)
        {
            if(audioName == CurrentAudioName)
            {
                Pause();
            }
        }

        //�Đ����Ă�����ꎞ��~
        public void Pause()
        {
            if(currentState == State.PLAYING || currentState == State.FADING)
            {
                audioSource.Pause();
            }

            currentState = State.PAUSE;
        }

        //�w�肳�ꂽ���O�̕����ꎞ��~���Ă�����ĊJ
        public void UnPause(string audioName)
        {
            if(audioName == CurrentAudioName)
            {
                UnPause();
            }
        }

        //�ꎞ��~���Ă�����ĊJ
        public void UnPause()
        {
            if(currentState != State.PAUSE)
            {
                return;
            }

            if(audioSource.clip == null)
            {
                currentState = State.WAIT;
            }
            else if(currentDelay > 0)
            {
                currentState = State.DELAY;
            }
            else
            {
                audioSource.UnPause();
                currentState = fadeDuration > 0 ? State.FADING : State.PLAYING;
            }
        }

        /*�t�F�[�h�֌W*/
        //�w�肳�ꂽ���O�̂��̂��Đ����Ă�����t�F�[�h
        public void Fade(string audioName, float duration, float from, float to, Action callback = null)
        {
            if(audioName == CurrentAudioName)
            {            
                Fade(duration, from, to, callback);               
            }
        }

        //�t�F�[�h
        public void Fade(float duration, float from, float to, Action callback = null)
        {
            if(currentState != State.PLAYING && currentState != State.DELAY && currentState != State.FADING)
            {
                return;
            }

            fadeProgress = 0;
            fadeDuration = duration;
            fadeFrom = from;
            fadeTo = to;
            fadeCallback = callback;

            if(currentState == State.PLAYING)
            {
                currentState = State.FADING;
            }

            if (currentState == State.FADING)
            {
                Update();
            }
        }
    }
}
