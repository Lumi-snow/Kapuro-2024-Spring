namespace AudioController
{
    using System;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class BGMAssistant : AudioAssistant
    {
        //フェードアウトの長さ(0ならフェードアウトしない)
        [SerializeField] private float fadeOutDuration = 0;
        public float FadeOutDuration
        {
            get
            {
                return fadeOutDuration;
            }
            set
            {
                fadeOutDuration = value;
            }
        }

        //ループ再生するか
        [SerializeField] private bool isLoop = true;
        public bool IsLoop
        {
            get
            {
                return isLoop;
            }
            set
            {
                isLoop = value;
            }
        }

        //再生を自動で停止するか
        [SerializeField] private bool isAutoStop = true;
        public bool IsAutoStop
        {
            get
            {
                return isAutoStop;
            }
            set
            {
                isAutoStop = value;
            }
        }

        /*初期化*/
        protected override void Start()
        {
            base.Start();

            //シーンが破棄されるタイミングで自動で停止
            if (isAutoStop == true)
            {
                SceneManager.sceneUnloaded += OnUnloadedScene;
            }
        }

        //シーンが破棄されたとき
        private void OnUnloadedScene(Scene scene)
        {
            SceneManager.sceneUnloaded -= OnUnloadedScene;
            if(fadeOutDuration > 0)
            {
                BGMController.Instance.FadeOut(fadeOutDuration);
            }
            else
            {
                BGMController.Instance.Stop();
            }
        }

        /*再生関係*/
        //BGM再生
        public override void Play()
        {
            if(audioClip == null)
            {
                Debug.LogWarning(gameObject.name + "のBGMAssistantにAudioClipが設定されていません");
                return;
            }

            BGMController.Instance.Play(audioClip, volumeRate, delay, pitch, isLoop, allowsDuplicate: fadeInDuration > 0);
            if(fadeInDuration > 0)
            {
                BGMController.Instance.FadeIn(audioClip.name, fadeInDuration);
            }
        }
    }
}
