namespace AudioController
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(AudioControllerSetting))]
    public class AudioControllerSettingEditor : Editor
    {
        /*更新関係*/
        //インスペクタを更新
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            ShowGUIAtSkin(ShowInspectorGUI);
            serializedObject.ApplyModifiedProperties();
        }

        //インスペクタ上のGUIを表示
        private void ShowInspectorGUI()
        {
            ShowGUIAtSkin(() => {
                ShowPropertyField("isAutoUpdateAudioPath", "Is Auto Update Audio Path", "BGMPathとSEPathを自動更新する");
            });

            ShowGUIAtSkin(() => {
                ShowPropertyField("bgmAudioPlayerNum", "BGM Audio Player Num", "BGMの同時再生可能数");
                ShowPropertyField("seAudioPlayerNum", "SE Audio Player Num", "SEの同時再生可能数");
            });

            ShowGUIAtSkin(() => {
                ShowPropertyField("bgmBaseVolume", "BGM Base Volume", "BGMの基準ボリューム");
                ShowPropertyField("seBaseVolume", "SE Base Volume", "SEの基準ボリューム");

                EditorGUILayout.Space();

                ShowPropertyField("shouldAdjustSEVolumeRate", "Should Adjust SE Volume Rate", "SEのボリューム倍率調整をする");
            });

            ShowGUIAtSkin(() => {
                ShowPropertyField("isAutoGenerateBGMController", "Is Auto Generate BGM Controller", "BGMControllerを自動生成する");
                ShowPropertyField("isAutoGenerateSEController", "Is Auto Generate SE Controller", "SEControllerを自動生成する");

                EditorGUILayout.Space();

                ShowPropertyField("isDestroyBGMController", "Is Destroy BGM Controller", "BGMControllerをシーン遷移時に破棄する");
                ShowPropertyField("isDestroySEController", "Is Destroy SE Controller", "SEControllerをシーン遷移時に破棄する");
            });

            ShowGUIAtSkin(() => {
                EditorGUILayout.LabelField("キャッシュの種類");
                EditorGUI.indentLevel++;
                EditorGUILayout.LabelField("None : 一切キャッシュしない");
                EditorGUILayout.LabelField("All : 起動時に全てキャッシュ");
                EditorGUILayout.LabelField("Used : ゲーム中に使ったものをキャッシュ");
                EditorGUI.indentLevel--;
                EditorGUILayout.Space();

                ShowGUIAtSkin(ShowCacheGUI, "BGM");
                ShowGUIAtSkin(ShowCacheGUI, "SE");
            });

            ShowGUIAtSkin(() => {
                ShowGUIAtSkin(ShowAutoSettingGUI, "BGM");
                ShowGUIAtSkin(ShowAutoSettingGUI, "SE");
            });
        }

        //オーディオの自動設定周りの設定をするGUIを表示
        private void ShowAutoSettingGUI(string targetTypeName)
        {
            var isAutoUpdateProperty = ShowPropertyField("isAutoUpdate" + targetTypeName + "Setting", "Is Auto Update" + targetTypeName + " Setting", targetTypeName + "ファイルの設定を自動でする");
            if (isAutoUpdateProperty.boolValue == false) 
            {
                return;
            }

            if(GUILayout.Button("全" + targetTypeName + "ファイル更新"))
            {
                if(targetTypeName == "BGM")
                {
                    AudioPostProcessor.UpdateBGMSetting();
                }
                else
                {
                    AudioPostProcessor.UpdateSESetting();
                }
            }

            ShowPropertyField("forceToMonoFor" + targetTypeName, "Force To Mono For" + targetTypeName, targetTypeName + "ファイルをステレオから強制的にモノラルにする");
            ShowPropertyField("normalizeFor" + targetTypeName, "Normalize For " + targetTypeName, targetTypeName + "ファイルの音量を平均化する");
            ShowPropertyField("ambisonicFor" + targetTypeName, "Ambisonic For " + targetTypeName, targetTypeName + "ファイルをアンビソニック(?)にする");
            ShowPropertyField("loadInBackgroundFor" + targetTypeName, "Load In Background For " + targetTypeName, targetTypeName + "ファイルをバックグラウンドでロードする");
            ShowPropertyField("loadTypeFor" + targetTypeName, "Load Type For " + targetTypeName, targetTypeName + "ファイルのロードの種類");
            ShowPropertyField("qualityFor" + targetTypeName, "Quality For " + targetTypeName, targetTypeName + "ファイルの品質");
            ShowPropertyField("compressionFormatFor" + targetTypeName, "Compression Format For " + targetTypeName, targetTypeName + "ファイルの圧縮フォーマット");
            ShowPropertyField("sampleRateSettingFor" + targetTypeName, "Sample Rate Setting For " + targetTypeName, targetTypeName + "ファイルのサンプリングレート");
        }

        //キャッシュの設定をするGUIを表示
        private void ShowCacheGUI(string targetTypeName)
        {
            var typeProperty = ShowPropertyField(targetTypeName.ToLower() + "CacheType", targetTypeName + " Cache Type", targetTypeName + "のキャッシュの種類");
            var cacheType = (AudioCacheType)Enum.ToObject(typeof(AudioCacheType), typeProperty.enumValueIndex);

            if(cacheType == AudioCacheType.USED)
            {
                ShowPropertyField("isRelease" + targetTypeName + "Cache", "Is Release " + targetTypeName + " Cache", "シーン遷移時に" + targetTypeName + "のキャッシュを破棄する");
            }
        }

        //プロパティを変更するGUIを表示
        private SerializedProperty ShowPropertyField(string propertyName, string propertyDisplayName, string summary)
        {
            var property = serializedObject.FindProperty(propertyName);
            if (property != null)
            {
                EditorGUILayout.PropertyField(property, new GUIContent(propertyDisplayName));
                EditorGUI.indentLevel++;
                EditorGUILayout.LabelField(summary);
                EditorGUI.indentLevel--;
                return property;
            }
            else
            {
                Debug.LogError("Property not found: " + propertyName);
                return property;
            }      
        }

        //GUIをスキンで挟んで表示
        private static void ShowGUIAtSkin(Action showGUIAction)
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);
            showGUIAction();
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();
        }

        //GUIをスキンで挟んで表示(引数1つ)
        public static void ShowGUIAtSkin<T1>(Action<T1> showGUIAction, T1 t1)
        {
            ShowGUIAtSkin(() => { showGUIAction(t1); });
        }
    }
}
