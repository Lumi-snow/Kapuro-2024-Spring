namespace AudioController
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    //オーディオキャッシュの種類
    public enum AudioCacheType
    {
        NONE,
        ALL,
        USED
    }

    public abstract class AudioController<TYpe> : SingletonWithMonoBehaviour<TYpe> where TYpe : MonoBehaviour 
    {
        private AudioCacheType cacheType; //キャッシュの種類

        private Dictionary<string, AudioClip> audioClipDict = new Dictionary<string, AudioClip>(); //キャッシュしているAudioClip

        protected readonly List<AudioPlayer> audioPlayerList = new List<AudioPlayer>(); //実際にオーディオを再生するクラス

        private int nextAudioPlayerNo = 0; //次に再生するプレイヤーの番号

        protected abstract int audioPlayerNum { get; } //AudioPlayerの数(同時に再生できる数)
        public int AudioPlayerNum => audioPlayerNum; 

        private float baseVolume = 1.0f; //ボリュームの基準と倍率

        //ここで初期化
        protected override void onAwakeProcess()
        {
            base.onAwakeProcess();

            for(int i = 0; i < audioPlayerNum; i++)
            {
                audioPlayerList.Add(new AudioPlayer(gameObject.AddComponent<AudioSource>()));
            }
        }

        //指定したディレクトリにあるAudioClipをロードして、キャッシュする
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

        //ここでアップデート
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

        //ボリュームの基準を変更する
        public void ChangeBaseVolume(float baseVolume)
        {
            this.baseVolume = baseVolume;
            audioPlayerList.Where(player => player.CurrentState != AudioPlayer.State.WAIT).ToList()
                .ForEach(player => player.ChangeVolume(this.baseVolume));
        }

        //再生中のオーディオの名前をすべて取得
        public List<string> GetCurrentAudioNames()
        {
            return audioPlayerList.Where(player => player.CurrentState != AudioPlayer.State.WAIT).Select(player => player.CurrentAudioName).ToList();
        }

        //再生しているものがあるか
        public bool IsPlaying()
        {
            return GetCurrentAudioNames().Count > 0;
        }

        /*オーディオ再生関係*/
        //再生
        protected void RunPlayer(AudioClip audioClip, float volumeRate, float delay, float pitch, bool isLoop, Action callback = null)
        {
            GetNextAudioPlayer().Play(audioClip, baseVolume, volumeRate, delay, pitch, isLoop, callback);
        }

        //再生
        protected void RunPlayer(string audioPath, float volumeRate, float delay, float pitch, bool isLoop, Action callback = null)
        {
            RunPlayer(GetAudioClip(audioPath), volumeRate, delay, pitch, isLoop, callback);
        }

        //オーディオのパスを名前に変換
        protected string PathToName(string audioPath)
        {
            return Path.GetFileNameWithoutExtension(audioPath);
        }

        //指定したパスのAudioClipを取得
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

        //次に再生するAudioPlayerを取得
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

        /*オーディオ再生終了*/
        //指定されたパスまたは、名前のものが再生されていたら再生停止
        public void Stop(string audioPathOrName)
        {
            var audioName = PathToName(audioPathOrName);
            audioPlayerList.ForEach(player => player.Stop(audioName));
        }

        //すべての再生を停止
        public void Stop()
        {
            audioPlayerList.ForEach(player => player.Stop());
        }

        /*フェード関係*/
        //指定されたパスまたは、名前のものが再生されていたらフェードする
        public void Fade(string audioPathOrName, float duration, float from, float to, Action callback)
        {
            var audioName = PathToName(audioPathOrName);
            audioPlayerList.ForEach(player => player.Fade(audioName, duration, from, to, callback));
        }

        //指定されたパスまたは、名前のものが再生されていたらフェードアウトする
        public void FadeOut(string audioPathOrName, float duration = 1.0f, Action callback = null)
        {
            var audioName = PathToName(audioPathOrName);
            Fade(audioName, duration, 1, 0, callback);
        }

        //指定されたパスまたは、名前のものが再生されていたらフェードインする
        public void FadeIn(string audioPathOrName, float duration = 1.0f, Action callback = null)
        {
            var audioName = PathToName(audioPathOrName);
            Fade(audioName, duration, 0, 1, callback);
        }

        //現在再生しているものをフェードする
        public void Fade(float duration, float from, float to, Action callback)
        {
            audioPlayerList.ForEach(player => player.Fade(duration, from, to, callback));
        }

        //現在再生しているものをフェードアウトする
        public void FadeOut(float duration = 1.0f, Action callback = null)
        {
            Fade(duration, 1, 0, callback);
        }

        //現在再生しているものをフェードインする
        public void FadeIn(float duration = 1.0f, Action callback = null)
        {
            Fade(duration, 0, 1, callback);
        }

        /*一時停止・再開*/
        //指定されたパスまたは、名前のものが再生されていたら一時停止
        public void Pause(string audioPathOrName)
        {
            var audioName = PathToName(audioPathOrName);
            audioPlayerList.ForEach(player => player.Pause(audioName));
        }

        //すべての再生を一時停止
        public void Pause()
        {
            audioPlayerList.ForEach(player => player.Pause());
        }

        //指定されたパスまたは、名前のものが一時停止されていたら再開
        public void UnPause(string audioPathOrName)
        {
            var audioName = PathToName(audioPathOrName);
            audioPlayerList.ForEach(player => player.UnPause(audioName));
        }

        //一時停止しているものをすべて再開
        public void UnPause()
        {
            audioPlayerList.ForEach(player => player.UnPause());
        }
    }
}
