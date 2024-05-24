namespace AudioController
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;

    public class AudioPostProcessor : AssetPostprocessor
    {
        /*�ύX�̊Ď�*/
        #if !UNITY_CLOUD_BUILD

        //�I�[�f�B�I�t�@�C���������Ă�f�B���N�g�����ύX���ꂽ��A�����Ŋe�X�N���v�g���쐬
        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            //UnityPackage�ōŏ��ɃC���|�[�g�������͂܂�null�Ȃ̂ł��̎��p
            if (AudioControllerSetting.Entity == null)
            {
                return;
            }

            string bgmDirectoryPath = GetBGMDirectoryPath(), seDirectoryPath = GetSEDirectoryPath();

            //�Ώۂ̃f�B���N�g�̃t�@�C�������邩�`�F�b�N
            var targetBGMPathList = new List<string>();
            var targetSEPathList = new List<string>();

            foreach (var path in importedAssets)
            {
                if (path.Contains(bgmDirectoryPath))
                {
                    targetBGMPathList.Add(path);
                }
                else if (path.Contains(seDirectoryPath))
                {
                    targetSEPathList.Add(path);
                }
            }

            foreach (var path in movedAssets)
            {
                if (path.Contains(bgmDirectoryPath) && !targetBGMPathList.Contains(path))
                {
                    targetBGMPathList.Add(path);
                }
                else if (path.Contains(seDirectoryPath) && !targetSEPathList.Contains(path))
                {
                    targetSEPathList.Add(path);
                }
            }

            if (AudioControllerSetting.Entity.IsAutoUpdateBGMSetting)
            {
                targetBGMPathList.ForEach(ChangeBGMSetting);
            }
            if (AudioControllerSetting.Entity.IsAutoUpdateSESetting)
            {
                targetSEPathList.ForEach(ChangeSESetting);
            }
        }

#endif

        /*�p�X�̎擾*/
        //�Ώۂ�BGM�������Ă�f�B���N�g���ւ̃p�X���擾
        private static string GetBGMDirectoryPath()
        {
            return GetTopDirectoryPath() + AudioPathCreator.BGM_DIRECTORY_PATH;
        }

        //�Ώۂ�SE�������Ă�f�B���N�g���ւ̃p�X���擾
        private static string GetSEDirectoryPath()
        {
            return GetTopDirectoryPath() + AudioPathCreator.SE_DIRECTORY_PATH;
        }

        //�Ώۂ̂������Ă�f�B���N�g���ւ̃p�X���擾
        private static string GetTopDirectoryPath()
        {
            //���̃X�N���v�g�����鏊�ւ̃p�X�擾���A��������p�X���v�Z
            string selfFileName = "AudioPostProcessor.cs";
            string selfPath = Directory.GetFiles("Assets", "*", System.IO.SearchOption.AllDirectories).FirstOrDefault(path => System.IO.Path.GetFileName(path) == selfFileName);

            var editorIndex = selfPath.LastIndexOf("Editor");
            return selfPath.Substring(0, editorIndex).Replace("\\", "/");
        }

        /*�ݒ�ύX*/
        //�S�I�[�f�B�I�t�@�C���̐ݒ���X�V����
        [MenuItem("Tools/AudioController/Update BGM&SE Setting")]
        private static void UpdateSetting()
        {
            UpdateBGMSetting();
            UpdateSESetting();
        }

        /// <summary>
        /// �SBGM�t�@�C���̐ݒ���X�V����
        /// </summary>
        [MenuItem("Tools/AudioController/Update BGM Setting")]
        public static void UpdateBGMSetting()
        {
            UpdateSetting(GetBGMDirectoryPath(), ChangeBGMSetting);
        }

        /// <summary>
        /// �SSE�t�@�C���̐ݒ���X�V����
        /// </summary>
        [MenuItem("Tools/AudioController/Update SE Setting")]
        public static void UpdateSESetting()
        {
            UpdateSetting(GetSEDirectoryPath(), ChangeSESetting);
        }

        //�S�I�[�f�B�I�t�@�C���̐ݒ���X�V����
        private static void UpdateSetting(string directoryPath, Action<string> changeSettingAction)
        {
            foreach (var filePath in Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories))
            {
                changeSettingAction(filePath);
            }
        }

        //BGM�t�@�C���̐ݒ��ύX����
        private static void ChangeBGMSetting(string audioPath)
        {
            var setting = AudioControllerSetting.Entity;
            ChangeSetting(audioPath, setting.ForceToMonoForBGM, setting.NormalizeForBGM, setting.AmbisonicForBGM, setting.LoadInBackgroundForBGM,
              setting.LoadTypeForBGM, setting.QualityForBGM, setting.CompressionFormatForBGM, setting.SampleRateSettingForBGM);
        }

        //SE�t�@�C���̐ݒ��ύX����
        private static void ChangeSESetting(string audioPath)
        {
            var setting = AudioControllerSetting.Entity;
            ChangeSetting(audioPath, setting.ForceToMonoForSE, setting.NormalizeForSE, setting.AmbisonicForSE, setting.LoadInBackgroundForSE,
              setting.LoadTypeForSE, setting.QualityForSE, setting.CompressionFormatForSE, setting.SampleRateSettingForSE);
        }

        //�I�[�f�B�I�t�@�C���̐ݒ��ύX����
        private static void ChangeSetting(string audioPath, bool forceToMono, bool normalize, bool ambisonic, bool loadInBackground, AudioClipLoadType loadType, float quality, AudioCompressionFormat compressionFormat, AudioSampleRateSetting sampleRateSetting)
        {
            if (AssetDatabase.LoadAssetAtPath<AudioClip>(audioPath) == null)
            {
                return;
            }
            var importer = AssetImporter.GetAtPath(audioPath) as AudioImporter;

            importer.forceToMono = forceToMono;

            var serializedObject = new SerializedObject(importer);
            var normalizeProperty = serializedObject.FindProperty("m_Normalize");
            normalizeProperty.boolValue = normalize;
            serializedObject.ApplyModifiedProperties();

            importer.ambisonic = ambisonic;
            importer.loadInBackground = loadInBackground;

            var settings = importer.defaultSampleSettings;
            settings.loadType = loadType;
            settings.quality = quality;
            settings.compressionFormat = compressionFormat;
            settings.sampleRateSetting = sampleRateSetting;

            importer.defaultSampleSettings = settings;

            Debug.Log(audioPath + "�̐ݒ��ύX���܂���");
        }
    }
}
