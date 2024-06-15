namespace AudioController
{
    using UnityEngine;

    public class BGMController : AudioController<BGMController>
    {
        protected override int audioPlayerNum => AudioControllerSetting.Entity.BGMAudioPlayerNum; //AudioPlayer�̐�(�����Đ��\��)

        private AudioPlayer audioPlayer => audioPlayerList[0]; //�Đ��Ɏg���Ă���v���C���[�N���X

        public static readonly string AUDIO_DIRECTORY_PATH = "Audio/BGM"; //�I�[�f�B�I�t�@�C���������Ă���f�B���N�g���ւ̃p�X

        /*������*/
        //�N�����Ɏ��s
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

        /*�Đ��֌W*/
        //�Đ�
        public void Play(AudioClip audioClip, float volumeRate = 1, float delay = 0, float pitch = 1, bool isLoop = true, bool allowsDuplicate = false)
        {
            //�d����������Ă��Ȃ������炷�łɍĐ����Ă�����̂��~�߂�
            if(allowsDuplicate)
            {
                Stop();
            }

            RunPlayer(audioClip, volumeRate, delay, pitch, isLoop);
        }

        //�Đ�
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
