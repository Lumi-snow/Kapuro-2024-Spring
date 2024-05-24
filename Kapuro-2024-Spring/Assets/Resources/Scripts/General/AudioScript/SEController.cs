namespace AudioController
{
    using System;
    using System.Security.Cryptography.X509Certificates;
    using UnityEngine;

    public class SEController : AudioController<SEController>
    {
        protected override int audioPlayerNum => AudioControllerSetting.Entity.SEAudioPlayerNum; //AudioPlayer�̐�(�����Đ��\��)
        public static readonly string AUDIO_DIRECTORY_PATH = "SE";

        [SerializeField] private bool shouldAdjustVolumeRate = true;

        /*������*/
        //�N�����Ɏ��s
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

        /*�Đ��֌W*/
        //�Đ�
        public void Play(AudioClip audioClip, float volumeRate = 1, float delay = 0, float pitch = 1, bool isLoop = false, Action callback = null)
        {
            volumeRate = AdjustVolumeRate(volumeRate, audioClip.name);
            if (volumeRate > 0)
            {
                RunPlayer(audioClip, volumeRate, delay, pitch, isLoop, callback);
            }
        }

        //�Đ�
        public void Play(string audioPath, float volumeRate = 1, float delay = 0, float pitch = 1, bool isLoop = false, Action callback = null)
        {
            volumeRate = AdjustVolumeRate(volumeRate, audioPath);
            if (volumeRate > 0)
            {
                RunPlayer(audioPath, volumeRate, delay, pitch, isLoop, callback);
            }
        }

        /*�擾*/
        //�{�����[���̔{���𒲐�(�������̂��Đ�����Ă�����{�����[���������āA�����ꂵ�Ȃ��悤�ɂ���)
        private float AdjustVolumeRate(float volumeRate, string audioPathOrName)
        {
            if (shouldAdjustVolumeRate == false)
            {
                return volumeRate;
            }

            var audioName = PathToName(audioPathOrName);

            //�w�肵�����̂Ɠ������̂��Đ����Ă���v���C���[���擾�A�Ȃ���΂��̂܂܂̃{�����[����Ԃ�
            var targetAudioPlayers = audioPlayerList.FindAll(player => player.CurrentAudioName == audioName);
            if (targetAudioPlayers.Count == 0)
            {
                return volumeRate;
            }

            //����SE�����Ă����Ȃ�{�����[����������
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