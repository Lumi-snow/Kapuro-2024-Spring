namespace AudioController
{
    using UnityEngine;

    public class BGMController : AudioController<BGMController>
    {
        protected override int audioPlayerNum => AudioControllerSetting.Entity.BGMAudioPlayerNum; //AudioPlayerの数(同時再生可能数)

        private AudioPlayer audioPlayer => audioPlayerList[0]; //再生に使っているプレイヤークラス

        public static readonly string AUDIO_DIRECTORY_PATH = "Audio/BGM"; //オーディオファイルが入っているディレクトリへのパス

        /*初期化*/
        //起動時に実行
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Initialize()
        {
            if(AudioControllerSetting.Entity.IsAutoGenerateBGMController)
            {
                new GameObject("BGMController", typeof(BGMController));
            }
        }

        protected override void onAwakeProcess()
        {
            base.onAwakeProcess();
            var setting = AudioControllerSetting.Entity;

            LoadAudioClip(AUDIO_DIRECTORY_PATH, setting.BGMCacheType, setting.IsReleaseBGMCache);

            ChangeBaseVolume(setting.BGMBaseVolume);
            if(setting.IsDestroyBGMController == false)
            {
                DontDestroyOnLoad(gameObject);
            }
        }

        /*再生関係*/
        //再生
        public void Play(AudioClip audioClip, float volumeRate = 1, float delay = 0, float pitch = 1, bool isLoop = true, bool allowsDuplicate = false)
        {
            //重複が許可されていなかったらすでに再生しているものを止める
            if(allowsDuplicate)
            {
                Stop();
            }

            RunPlayer(audioClip, volumeRate, delay, pitch, isLoop);
        }

        //再生
        public void Play(string audioPath, float volumeRate = 1, float delay = 0, float pitch = 1, bool isLoop = true, bool allowsDuplicate = false)
        {
            if(allowsDuplicate)
            {
                Stop();
            }

            RunPlayer(audioPath, volumeRate, delay, pitch, isLoop);
        }
    }
}
