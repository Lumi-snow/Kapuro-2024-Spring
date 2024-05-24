namespace AudioController
{
    using System;
    using System.Security.Claims;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System.Runtime.Versioning;


#if UNITY_EDITOR
        using UnityEditor;
#endif

    public class AudioControllerSetting : ScriptableObject
    {
        //外部からアクセスするような実体、初めてアクセスするときにLoadする
        private static AudioControllerSetting entity = null;
        public static AudioControllerSetting Entity
        {
            get
            {
                if(entity == null)
                {
                    entity = Resources.Load<AudioControllerSetting>("AudioControllerSetting");
                }

                return entity;
            }
        }

        [Header("オーディオファイルへのパスを定数で管理するクラスを自動更新するか")]
        [SerializeField] private bool isAutoUpdateAudioPath = true;
        [SerializeField] public bool IsAutoUpdateAudioPath => isAutoUpdateAudioPath;

        [Header("同時再生可能数")]
        [SerializeField] private int bgmAudioPlayerNum = 3;
        [SerializeField] private int seAudioPlayerNum = 10;
        [SerializeField] public int BGMAudioPlayerNum => bgmAudioPlayerNum;
        [SerializeField] public int SEAudioPlayerNum => seAudioPlayerNum;

        [Header("基準のボリューム")]
        [SerializeField] private float bgmBaseVolume = 1.0f;
        [SerializeField] private float seBaseVolume = 1.0f;
        [SerializeField] public float BGMBaseVolume => bgmBaseVolume;
        [SerializeField] public float SEBaseVolume => seBaseVolume;

        [Header("SEのボリュームの倍率調整をするか")]
        [SerializeField] private bool shouldAdjustSEVolumeRate = true;
        [SerializeField] public bool ShouldAdjustSEVolumeRate => shouldAdjustSEVolumeRate;

        [Header("BGMController, SEControllerを自動生成するか")]
        [SerializeField] private bool isAutoGenerateBGMController = true;
        [SerializeField] private bool isAutoGenerateSEController = true;
        [SerializeField] public bool IsAutoGenerateBGMController => isAutoGenerateBGMController;
        [SerializeField] public  bool IsAutoGenerateSEController => isAutoGenerateSEController;

        [Header("BGMController, SEControllerを破棄するか")]
        [SerializeField] private bool isDestroyBGMController = false;
        [SerializeField] private bool isDestroySEController = false;
        [SerializeField] public bool IsDestroyBGMController => isDestroyBGMController;
        [SerializeField] public bool IsDestroySEController => isDestroySEController;


        [Header("AudioClipのキャッシュ設定")]
        [SerializeField] private AudioCacheType bgmCacheType = AudioCacheType.ALL;
        [SerializeField] private AudioCacheType seCacheType = AudioCacheType.ALL;
        [SerializeField] public AudioCacheType BGMCacheType => bgmCacheType;
        [SerializeField] public AudioCacheType SECacheType => seCacheType;

        [SerializeField] private bool isReleaseBGMCache = false;
        [SerializeField] private bool isReleaseSECache = false;
        [SerializeField] public bool IsReleaseBGMCache => isReleaseBGMCache;
        [SerializeField] public bool IsReleaseSECache => isReleaseSECache;

        [Header("オーディオファイルの自動設定")]
        [SerializeField] private bool isAutoUpdateBGMSetting = true;
        [SerializeField] private bool isAutoUpdateSESetting = true;
        [SerializeField] public bool IsAutoUpdateBGMSetting => isAutoUpdateBGMSetting;
        [SerializeField] public bool IsAutoUpdateSESetting => isAutoUpdateSESetting;

        [SerializeField] private bool forceToMonoForBGM = true;
        [SerializeField] private bool forceToMonoForSE = true;
        [SerializeField] public bool ForceToMonoForBGM => forceToMonoForBGM;
        [SerializeField] public bool ForceToMonoForSE => forceToMonoForSE;

        [SerializeField] private bool normalizeForBGM = true;
        [SerializeField] private bool normalizeForSE = true;
        [SerializeField] public bool NormalizeForBGM => normalizeForBGM;
        [SerializeField] public bool NormalizeForSE => normalizeForSE;

        [SerializeField] private bool ambisonicForBGM = false;
        [SerializeField] private bool ambisonicForSE = false;
        [SerializeField] public bool AmbisonicForBGM => ambisonicForBGM;
        [SerializeField] public bool AmbisonicForSE => ambisonicForSE;

        [SerializeField] private bool loadInBackgroundForBGM = false;
        [SerializeField] private bool loadInBackgroundForSE = false;
        [SerializeField] public bool LoadInBackgroundForBGM => loadInBackgroundForBGM;
        [SerializeField] public bool LoadInBackgroundForSE => loadInBackgroundForSE;

        [SerializeField] private AudioClipLoadType loadTypeForBGM = AudioClipLoadType.Streaming;
        [SerializeField] private AudioClipLoadType loadTypeForSE = AudioClipLoadType.CompressedInMemory;
        [SerializeField] public AudioClipLoadType LoadTypeForBGM => loadTypeForBGM;
        [SerializeField] public AudioClipLoadType LoadTypeForSE => loadTypeForSE;

        [SerializeField] private float qualityForBGM = 0.3f;
        [SerializeField] private float qualityForSE = 0.3f;
        [SerializeField] public float QualityForBGM => qualityForBGM;
        [SerializeField] public float QualityForSE => qualityForSE;

        [SerializeField] private AudioCompressionFormat compressionFormatForBGM = AudioCompressionFormat.Vorbis;
        [SerializeField] private AudioCompressionFormat compressionFormatForSE = AudioCompressionFormat.Vorbis;
        [SerializeField] public AudioCompressionFormat CompressionFormatForBGM => compressionFormatForBGM;
        [SerializeField] public AudioCompressionFormat CompressionFormatForSE => compressionFormatForSE;

        #if UNITY_EDITOR
        [SerializeField] private AudioSampleRateSetting sampleRateSettingForBGM = AudioSampleRateSetting.OptimizeSampleRate;
        [SerializeField] private AudioSampleRateSetting sampleRateSettingForSE = AudioSampleRateSetting.OptimizeSampleRate;
        [SerializeField] public AudioSampleRateSetting SampleRateSettingForBGM => sampleRateSettingForBGM;
        [SerializeField] public AudioSampleRateSetting SampleRateSettingForSE => sampleRateSettingForSE;
        #endif
    }
}
