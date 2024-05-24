namespace AudioController
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class AudioPlayer
    {
        private readonly AudioSource audioSource; //再生用のソース

        public float PlayedTime => audioSource.time; //再生した時間

        public string CurrentAudioName => audioSource.clip == null ? "" : audioSource.clip.name; //再生中のオーディオの名前

        private Action callback; //再生終了後の処理

        //状態
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

        //ボリュームの基準と倍率
        private float baseVolume;
        private float volumeRate;
        public float CurrentVolume => baseVolume * volumeRate;

        //再生までの待ち時間
        private float initialDelay;
        private float currentDelay;
        public float ElapsedDelay => initialDelay - currentDelay;

        //フェード関係
        private float fadeProgress;
        private float fadeDuration;
        private float fadeFrom;
        private float fadeTo;
        private Action fadeCallback;

        /*初期化*/
        public AudioPlayer(AudioSource audioSource)
        {
            this.audioSource = audioSource;
            this.audioSource.playOnAwake = false;
        }

        /*アップデート*/
        public void Update()
        {
            //終了判定
            if(currentState == State.PLAYING && !audioSource.isPlaying && Mathf.Approximately(audioSource.time, 0))
            {
                Finish();
            }
            //再生前の待機
            else if(currentState == State.DELAY)
            {
                Delay();
            }
            //フェード
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

        /*設定など*/
        //ボリュームを変更
        public void ChangeVolume(float baseVolume)
        {
            this.baseVolume = baseVolume;
            audioSource.volume = GetVolume();
        }

        //volumeRateを変更
        public void ChangeVolumeRate(float volumeRate)
        {
            this.volumeRate = volumeRate;
            audioSource.volume = GetVolume();
        }

        //ボリュームを取得
        private float GetVolume()
        {
            return baseVolume * volumeRate;
        }

        /*再生開始*/
        public void Play(AudioClip audioClip, float baseVolume, float volumeRate, float delay, float pitch, bool isLoop, Action callback = null)
        {
            //停止中でなければ停止
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

            //ループ再生でなければ、再生修了のチェックをする
            if (audioSource.loop)
            {
                return;
            }

            //ポーズされていたらすぐに止める
            if(currentState == State.PAUSE)
            {
                Pause();
            }
        }

        /*再生終了*/
        //指定された名前のものを再生していたら停止
        public void Stop(string audioName)
        {
            if(audioName == CurrentAudioName)
            {
                Stop();
            }
        }

        //再生を停止
        public void Stop()
        {
            callback = null;
            Finish();
        }

        //再生終了
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

        /*一時停止と再開*/
        //指定された名前のものを再生していたら一時停止
        public void Pause(string audioName)
        {
            if(audioName == CurrentAudioName)
            {
                Pause();
            }
        }

        //再生していたら一時停止
        public void Pause()
        {
            if(currentState == State.PLAYING || currentState == State.FADING)
            {
                audioSource.Pause();
            }

            currentState = State.PAUSE;
        }

        //指定された名前の物を一時停止していたら再開
        public void UnPause(string audioName)
        {
            if(audioName == CurrentAudioName)
            {
                UnPause();
            }
        }

        //一時停止していたら再開
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

        /*フェード関係*/
        //指定された名前のものを再生していたらフェード
        public void Fade(string audioName, float duration, float from, float to, Action callback = null)
        {
            if(audioName == CurrentAudioName)
            {            
                Fade(duration, from, to, callback);               
            }
        }

        //フェード
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
