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

        //変更の監視
        #if !UNITY_CLOUD_BUILD

        //オーディオファイルが入ってるディレクトリが変更されたら、自動で各スクリプトを作成
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

        //入力されたassetsのパスの中に、指定したパスが含まれるものが一つでもあるか
        private static bool ExistsPathInAssets(List<string[]> assetPathsList, string targetPath)
        {
            return assetPathsList
                .Any(assetPaths => assetPaths
                    .Any(assetPath => assetPath
                        .Contains(targetPath)));
        }

#endif

        /*スクリプト作成*/
        //BGMとSEファイルへのパスを定数で管理するクラスを作成
        [MenuItem("Tools/AudioController/Create BGM&SE Path")]
        private static void CreateAudioPath()
        {
            CreateBGMPath();
            CreateSEPath();
        }

        //BGMファイルへのパスを定数で管理するクラスを作成
        [MenuItem("Tools/AudioController/Create BGM Path")]
        private static void CreateBGMPath()
        {
            Create(BGM_DIRECTORY_PATH);
        }

        //SEファイルへのパスを定数で管理するクラスを作成
        [MenuItem("Tools/AudioController/Create SE Path")]
        private static void CreateSEPath()
        {
            Create(SE_DIRECTORY_PATH);
        }

        //オーディオファイルへのパスを定数で管理するクラスを作成
        private static void Create(string directoryPath)
        {
            //オーディオファイルへのパスを抽出
            string directoryName = Path.GetFileName(directoryPath);
            var audioPathDict = new Dictionary<string, string>();

            foreach (var audioClip in Resources.LoadAll<AudioClip>(directoryName))
            {
                //アセットへのパスを取得
                var assetPath = AssetDatabase.GetAssetPath(audioClip);

                //Resources以下のパス(拡張子なし)に変換
                var targetIndex = assetPath.LastIndexOf("Resources", StringComparison.Ordinal) + "Resources".Length + 1;
                var resourcesPath = assetPath.Substring(targetIndex);
                resourcesPath = resourcesPath.Replace(Path.GetExtension(resourcesPath), "");

                //オーディオ名の重複チェック
                var audioName = audioClip.name;
                if (audioPathDict.ContainsKey(audioName))
                {
                    Debug.LogError(audioName + " is duplicate!\n1 : " + resourcesPath + "\n2 : " + audioPathDict[audioName]);
                }
                audioPathDict[audioName] = resourcesPath;
            }

            //このスクリプトがある所へのパス取得し、定数クラスを書き出す場所を決定
            string selfFileName = "AudioPathCreator.cs";
            string selfPath = Directory.GetFiles("Assets", "*", System.IO.SearchOption.AllDirectories)
                .FirstOrDefault(path => System.IO.Path.GetFileName(path) == selfFileName);

            string exportPath = selfPath.Replace(selfFileName, "").Replace("Editor", "Scripts");

            //定数クラス作成
            ConstantsClassCreator.Create(directoryName + "Path", directoryName + "ファイルへのパスを定数で管理するクラス", audioPathDict, exportPath, "AudioController");
        }
    }
}
