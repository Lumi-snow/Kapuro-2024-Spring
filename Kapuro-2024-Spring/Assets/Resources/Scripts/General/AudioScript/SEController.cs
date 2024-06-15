namespace AudioController
{
    using System;
    using System.Security.Cryptography.X509Certificates;
    using UnityEngine;

    public class SEController : AudioController<SEController>
    {
        protected override int audioPlayerNum => AudioControllerSetting.Entity.SEAudioPlayerNum; //AudioPlayerの数(同時再生可能数)
        public static readonly string AUDIO_DIRECTORY_PATH = "Audio/SE";

        [SerializeField] private bool shouldAdjustVolumeRate = true;

        /*初期化*/
        //起動時に実行
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Initialize()
        {
            if (AudioControllerSetting.Entity.IsAutoGenerateSEController)
            {
                new GameObject("SEController", typeof(SEController));
            }
        }

        protected override void onAwakeProcess()
        {
            base.onAwakeProcess();
            var setting = AudioControllerSetting.Entity;

            LoadAudioClip(AUDIO_DIRECTORY_PATH, setting.SECacheType, setting.IsReleaseSECache);

            shouldAdjustVolumeRate = setting.ShouldAdjustSEVolumeRate;
            ChangeBaseVolume(setting.SEBaseVolume);
            if(setting.IsDestroySEController == false)
            {
                DontDestroyOnLoad(gameObject);
            }
        }

        /*再生関係*/
        //再生
        public void Play(AudioClip audioClip, float volumeRate = 1, float delay = 0, float pitch = 1, bool isLoop = false, Action callback = null)
        {
            volumeRate = AdjustVolumeRate(volumeRate, audioClip.name);
            if (volumeRate > 0)
            {
                RunPlayer(audioClip, volumeRate, delay, pitch, isLoop, callback);
            }
        }

        //再生
        public void Play(string audioPath, float volumeRate = 1, float delay = 0, float pitch = 1, bool isLoop = false, Action callback = null)
        {
            volumeRate = AdjustVolumeRate(volumeRate, audioPath);
            if (volumeRate > 0)
            {
                RunPlayer(audioPath, volumeRate, delay, pitch, isLoop, callback);
            }
        }

        /*取得*/
        //ボリュームの倍率を調整(同じものが再生されていたらボリュームを下げて、音割れしないようにする)
        private float AdjustVolumeRate(float volumeRate, string audioPathOrName)
        {
            if (shouldAdjustVolumeRate == false)
            {
                return volumeRate;
            }

            var audioName = PathToName(audioPathOrName);

            //指定したものと同じものを再生しているプレイヤーを取得、なければそのままのボリュームを返す
            var targetAudioPlayers = audioPlayerList.FindAll(player => player.CurrentAudioName == audioName);
            if (targetAudioPlayers.Count == 0)
            {
                return volumeRate;
            }

            //同じSEが鳴ってすぐならボリュームを下げる
            foreach (var targetAudioPlayer in audioPlayerList.FindAll(player => player.CurrentAudioName == audioName))
            {
                if (targetAudioPlayer.CurrentVolume <= 0)
                {
                    continue;
                }

                float playedTime = targetAudioPlayer.PlayedTime;
                if (targetAudioPlayer.CurrentState == AudioPlayer.State.DELAY)
                {
                    playedTime += targetAudioPlayer.ElapsedDelay;
                }

                if (playedTime <= 0.025f)
                {
                    return 0;
                }
                else if (playedTime <= 0.05f)
                {
                    volumeRate *= 0.8f;
                }
                else if (playedTime <= 0.1f)
                {
                    volumeRate *= 0.9f;
                }
            }

            return volumeRate;
        }
    }
}