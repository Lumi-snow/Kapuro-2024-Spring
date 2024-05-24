namespace AudioController
{
    using System;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class BGMAssistant : AudioAssistant
    {
        //�t�F�[�h�A�E�g�̒���(0�Ȃ�t�F�[�h�A�E�g���Ȃ�)
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

        //���[�v�Đ����邩
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

        //�Đ��������Œ�~���邩
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

        /*������*/
        protected override void Start()
        {
            base.Start();

            //�V�[�����j�������^�C�~���O�Ŏ����Œ�~
            if (isAutoStop == true)
            {
                SceneManager.sceneUnloaded += OnUnloadedScene;
            }
        }

        //�V�[�����j�����ꂽ�Ƃ�
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

        /*�Đ��֌W*/
        //BGM�Đ�
        public override void Play()
        {
            if(audioClip == null)
            {
                Debug.LogWarning(gameObject.name + "��BGMAssistant��AudioClip���ݒ肳��Ă��܂���");
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
