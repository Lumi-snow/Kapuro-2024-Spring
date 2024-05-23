namespace AudioController
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    //�I�[�f�B�I�L���b�V���̎��
    public enum AudioCacheType
    {
        NONE,
        ALL,
        USED
    }

    public abstract class AudioController<TYpe> : SingletonWithMonoBehaviour<TYpe> where TYpe : MonoBehaviour 
    {
        private AudioCacheType cacheType; //�L���b�V���̎��

        private Dictionary<string, AudioClip> audioClipDict = new Dictionary<string, AudioClip>(); //�L���b�V�����Ă���AudioClip

        protected readonly List<AudioPlayer> audioPlayerList = new List<AudioPlayer>(); //���ۂɃI�[�f�B�I���Đ�����N���X

        private int nextAudioPlayerNo = 0; //���ɍĐ�����v���C���[�̔ԍ�

        protected abstract int audioPlayerNum { get; } //AudioPlayer�̐�(�����ɍĐ��ł��鐔)
        public int AudioPlayerNum => audioPlayerNum; 

        private float baseVolume = 1.0f; //�{�����[���̊�Ɣ{��

        //�����ŏ�����
        protected override void onAwakeProcess()
        {
            base.onAwakeProcess();

            for(int i = 0; i < audioPlayerNum; i++)
            {
                audioPlayerList.Add(new AudioPlayer(gameObject.AddComponent<AudioSource>()));
            }
        }

        //�w�肵���f�B���N�g���ɂ���AudioClip�����[�h���āA�L���b�V������
        protected void LoadAudioClip(string directryPath, AudioCacheType cacheType, bool isReleaseBGMCache)
        {
            this.cacheType = cacheType;
            if(this.cacheType == AudioCacheType.ALL)
            {
                audioClipDict = Resources.LoadAll<AudioClip>(directryPath).ToDictionary(clip => clip.name, clip => clip);
            }

            if(this.cacheType == AudioCacheType.USED && isReleaseBGMCache)
            {
                SceneManager.sceneUnloaded += (scene) =>
                {
                    audioClipDict.Clear();
                };
            }
        }

        //�����ŃA�b�v�f�[�g
        private void Update()
        {
            foreach(var audioPlayer in audioPlayerList)
            {
                if(audioPlayer.CurrentState != AudioPlayer.State.WAIT)
                {
                    audioPlayer.Update();
                }
            }
        }

        //�{�����[���̊��ύX����
        public void ChangeBaseVolume(float baseVolume)
        {
            this.baseVolume = baseVolume;
            audioPlayerList.Where(player => player.CurrentState != AudioPlayer.State.WAIT).ToList()
                .ForEach(player => player.ChangeVolume(this.baseVolume));
        }

        //�Đ����̃I�[�f�B�I�̖��O�����ׂĎ擾
        public List<string> GetCurrentAudioNames()
        {
            return audioPlayerList.Where(player => player.CurrentState != AudioPlayer.State.WAIT).Select(player => player.CurrentAudioName).ToList();
        }

        //�Đ����Ă�����̂����邩
        public bool IsPlaying()
        {
            return GetCurrentAudioNames().Count > 0;
        }

        /*�I�[�f�B�I�Đ��֌W*/
        //�Đ�
        protected void RunPlayer(AudioClip audioClip, float volumeRate, float delay, float pitch, bool isLoop, Action callback = null)
        {
            GetNextAudioPlayer().Play(audioClip, baseVolume, volumeRate, delay, pitch, isLoop, callback);
        }

        //�Đ�
        protected void RunPlayer(string audioPath, float volumeRate, float delay, float pitch, bool isLoop, Action callback = null)
        {
            RunPlayer(GetAudioClip(audioPath), volumeRate, delay, pitch, isLoop, callback);
        }

        //�I�[�f�B�I�̃p�X�𖼑O�ɕϊ�
        protected string PathToName(string audioPath)
        {
            return Path.GetFileNameWithoutExtension(audioPath);
        }

        //�w�肵���p�X��AudioClip���擾
        private AudioClip GetAudioClip(string audioPath)
        {
            string audioName = PathToName(audioPath);

            if (audioClipDict.ContainsKey(audioName))
            {
                return audioClipDict[audioName];
            }

            var audioClip = Resources.Load<AudioClip>(audioPath);
            if(audioClip == null)
            {
                Debug.LogError(audioPath + " not found.");
            }

            if(cacheType == AudioCacheType.USED)
            {
                audioClipDict[audioName] = audioClip;
            }

            return audioClip;
        }

        //���ɍĐ�����AudioPlayer���擾
        private AudioPlayer GetNextAudioPlayer()
        {
            var audioPlayer = audioPlayerList[nextAudioPlayerNo];

            nextAudioPlayerNo++;
            if (nextAudioPlayerNo >= audioPlayerList.Count)
            {
                nextAudioPlayerNo = 0;
            }

            return audioPlayer;
        }

        /*�I�[�f�B�I�Đ��I��*/
        //�w�肳�ꂽ�p�X�܂��́A���O�̂��̂��Đ�����Ă�����Đ���~
        public void Stop(string audioPathOrName)
        {
            var audioName = PathToName(audioPathOrName);
            audioPlayerList.ForEach(player => player.Stop(audioName));
        }

        //���ׂĂ̍Đ����~
        public void Stop()
        {
            audioPlayerList.ForEach(player => player.Stop());
        }

        /*�t�F�[�h�֌W*/
        //�w�肳�ꂽ�p�X�܂��́A���O�̂��̂��Đ�����Ă�����t�F�[�h����
        public void Fade(string audioPathOrName, float duration, float from, float to, Action callback)
        {
            var audioName = PathToName(audioPathOrName);
            audioPlayerList.ForEach(player => player.Fade(audioName, duration, from, to, callback));
        }

        //�w�肳�ꂽ�p�X�܂��́A���O�̂��̂��Đ�����Ă�����t�F�[�h�A�E�g����
        public void FadeOut(string audioPathOrName, float duration = 1.0f, Action callback = null)
        {
            var audioName = PathToName(audioPathOrName);
            Fade(audioName, duration, 1, 0, callback);
        }

        //�w�肳�ꂽ�p�X�܂��́A���O�̂��̂��Đ�����Ă�����t�F�[�h�C������
        public void FadeIn(string audioPathOrName, float duration = 1.0f, Action callback = null)
        {
            var audioName = PathToName(audioPathOrName);
            Fade(audioName, duration, 0, 1, callback);
        }

        //���ݍĐ����Ă�����̂��t�F�[�h����
        public void Fade(float duration, float from, float to, Action callback)
        {
            audioPlayerList.ForEach(player => player.Fade(duration, from, to, callback));
        }

        //���ݍĐ����Ă�����̂��t�F�[�h�A�E�g����
        public void FadeOut(float duration = 1.0f, Action callback = null)
        {
            Fade(duration, 1, 0, callback);
        }

        //���ݍĐ����Ă�����̂��t�F�[�h�C������
        public void FadeIn(float duration = 1.0f, Action callback = null)
        {
            Fade(duration, 0, 1, callback);
        }

        /*�ꎞ��~�E�ĊJ*/
        //�w�肳�ꂽ�p�X�܂��́A���O�̂��̂��Đ�����Ă�����ꎞ��~
        public void Pause(string audioPathOrName)
        {
            var audioName = PathToName(audioPathOrName);
            audioPlayerList.ForEach(player => player.Pause(audioName));
        }

        //���ׂĂ̍Đ����ꎞ��~
        public void Pause()
        {
            audioPlayerList.ForEach(player => player.Pause());
        }

        //�w�肳�ꂽ�p�X�܂��́A���O�̂��̂��ꎞ��~����Ă�����ĊJ
        public void UnPause(string audioPathOrName)
        {
            var audioName = PathToName(audioPathOrName);
            audioPlayerList.ForEach(player => player.UnPause(audioName));
        }

        //�ꎞ��~���Ă�����̂����ׂčĊJ
        public void UnPause()
        {
            audioPlayerList.ForEach(player => player.UnPause());
        }
    }
}
