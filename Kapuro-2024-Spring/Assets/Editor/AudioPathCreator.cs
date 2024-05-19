namespace AudioController
{
    using System;
    using System.IO;
    using UnityEditor;
    using UnityEngine;
    using System.Collections.Generic;
    using System.Linq;

    public class AudioPathCreator : AssetPostprocessor
    {
        public static readonly string BGM_DIRECTORY_PATH = "Resources/Audio" + BGMController.AUDIO_DIRECTORY_PATH;
        public static readonly string SE_DIRECTORY_PATH = "Resources/Audio" + SEController.AUDIO_DIRECTORY_PATH;

        //�ύX�̊Ď�
        #if !UNITY_CLOUD_BUILD

        //�I�[�f�B�I�t�@�C���������Ă�f�B���N�g�����ύX���ꂽ��A�����Ŋe�X�N���v�g���쐬
        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            if(AudioControllerSetting.Entity == null || AudioControllerSetting.Entity.IsAutoUpdateAudioPath == false)
            {
                return;
            }

            List<string[]> assetsList = new List<string[]>()
            {
                importedAssets, deletedAssets, movedAssets, movedFromAssetPaths
            };

            EditorApplication.delayCall += () =>
            {
                if (ExistsPathInAssets(assetsList, BGM_DIRECTORY_PATH))
                {
                    CreateBGMPath();
                }

                if (ExistsPathInAssets(assetsList, SE_DIRECTORY_PATH))
                {
                    CreateSEPath();
                }
            };
        }

        //���͂��ꂽassets�̃p�X�̒��ɁA�w�肵���p�X���܂܂����̂���ł����邩
        private static bool ExistsPathInAssets(List<string[]> assetPathsList, string targetPath)
        {
            return assetPathsList
                .Any(assetPaths => assetPaths
                    .Any(assetPath => assetPath
                        .Contains(targetPath)));
        }

#endif

        /*�X�N���v�g�쐬*/
        //BGM��SE�t�@�C���ւ̃p�X��萔�ŊǗ�����N���X���쐬
        [MenuItem("Tools/AudioController/Create BGM&SE Path")]
        private static void CreateAudioPath()
        {
            CreateBGMPath();
            CreateSEPath();
        }

        //BGM�t�@�C���ւ̃p�X��萔�ŊǗ�����N���X���쐬
        [MenuItem("Tools/AudioController/Create BGM Path")]
        private static void CreateBGMPath()
        {
            Create(BGM_DIRECTORY_PATH);
        }

        //SE�t�@�C���ւ̃p�X��萔�ŊǗ�����N���X���쐬
        [MenuItem("Tools/AudioController/Create SE Path")]
        private static void CreateSEPath()
        {
            Create(SE_DIRECTORY_PATH);
        }

        //�I�[�f�B�I�t�@�C���ւ̃p�X��萔�ŊǗ�����N���X���쐬
        private static void Create(string directoryPath)
        {
            //�I�[�f�B�I�t�@�C���ւ̃p�X�𒊏o
            string directoryName = Path.GetFileName(directoryPath);
            var audioPathDict = new Dictionary<string, string>();

            foreach (var audioClip in Resources.LoadAll<AudioClip>(directoryName))
            {
                //�A�Z�b�g�ւ̃p�X���擾
                var assetPath = AssetDatabase.GetAssetPath(audioClip);

                //Resources�ȉ��̃p�X(�g���q�Ȃ�)�ɕϊ�
                var targetIndex = assetPath.LastIndexOf("Resources", StringComparison.Ordinal) + "Resources".Length + 1;
                var resourcesPath = assetPath.Substring(targetIndex);
                resourcesPath = resourcesPath.Replace(Path.GetExtension(resourcesPath), "");

                //�I�[�f�B�I���̏d���`�F�b�N
                var audioName = audioClip.name;
                if (audioPathDict.ContainsKey(audioName))
                {
                    Debug.LogError(audioName + " is duplicate!\n1 : " + resourcesPath + "\n2 : " + audioPathDict[audioName]);
                }
                audioPathDict[audioName] = resourcesPath;
            }

            //���̃X�N���v�g�����鏊�ւ̃p�X�擾���A�萔�N���X�������o���ꏊ������
            string selfFileName = "AudioPathCreator.cs";
            string selfPath = Directory.GetFiles("Assets", "*", System.IO.SearchOption.AllDirectories)
                .FirstOrDefault(path => System.IO.Path.GetFileName(path) == selfFileName);

            string exportPath = selfPath.Replace(selfFileName, "").Replace("Editor", "Scripts");

            //�萔�N���X�쐬
            ConstantsClassCreator.Create(directoryName + "Path", directoryName + "�t�@�C���ւ̃p�X��萔�ŊǗ�����N���X", audioPathDict, exportPath, "AudioController");
        }
    }
}
