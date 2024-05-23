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
        /*�X�V�֌W*/
        //�C���X�y�N�^���X�V
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            ShowGUIAtSkin(ShowInspectorGUI);
            serializedObject.ApplyModifiedProperties();
        }

        //�C���X�y�N�^���GUI��\��
        private void ShowInspectorGUI()
        {
            ShowGUIAtSkin(() => {
                ShowPropertyField("isAutoUpdateAudioPath", "Is Auto Update Audio Path", "BGMPath��SEPath�������X�V����");
            });

            ShowGUIAtSkin(() => {
                ShowPropertyField("bgmAudioPlayerNum", "BGM Audio Player Num", "BGM�̓����Đ��\��");
                ShowPropertyField("seAudioPlayerNum", "SE Audio Player Num", "SE�̓����Đ��\��");
            });

            ShowGUIAtSkin(() => {
                ShowPropertyField("bgmBaseVolume", "BGM Base Volume", "BGM�̊�{�����[��");
                ShowPropertyField("seBaseVolume", "SE Base Volume", "SE�̊�{�����[��");

                EditorGUILayout.Space();

                ShowPropertyField("shouldAdjustSEVolumeRate", "Should Adjust SE Volume Rate", "SE�̃{�����[���{������������");
            });

            ShowGUIAtSkin(() => {
                ShowPropertyField("isAutoGenerateBGMController", "Is Auto Generate BGM Controller", "BGMController��������������");
                ShowPropertyField("isAutoGenerateSEController", "Is Auto Generate SE Controller", "SEController��������������");

                EditorGUILayout.Space();

                ShowPropertyField("isDestroyBGMController", "Is Destroy BGM Controller", "BGMController���V�[���J�ڎ��ɔj������");
                ShowPropertyField("isDestroySEController", "Is Destroy SE Controller", "SEController���V�[���J�ڎ��ɔj������");
            });

            ShowGUIAtSkin(() => {
                EditorGUILayout.LabelField("�L���b�V���̎��");
                EditorGUI.indentLevel++;
                EditorGUILayout.LabelField("None : ��؃L���b�V�����Ȃ�");
                EditorGUILayout.LabelField("All : �N�����ɑS�ăL���b�V��");
                EditorGUILayout.LabelField("Used : �Q�[�����Ɏg�������̂��L���b�V��");
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

        //�I�[�f�B�I�̎����ݒ����̐ݒ������GUI��\��
        private void ShowAutoSettingGUI(string targetTypeName)
        {
            var isAutoUpdateProperty = ShowPropertyField("isAutoUpdate" + targetTypeName + "Setting", "Is Auto Update" + targetTypeName + " Setting", targetTypeName + "�t�@�C���̐ݒ�������ł���");
            if (isAutoUpdateProperty.boolValue == false) 
            {
                return;
            }

            if(GUILayout.Button("�S" + targetTypeName + "�t�@�C���X�V"))
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

            ShowPropertyField("forceToMonoFor" + targetTypeName, "Force To Mono For" + targetTypeName, targetTypeName + "�t�@�C�����X�e���I���狭���I�Ƀ��m�����ɂ���");
            ShowPropertyField("normalizeFor" + targetTypeName, "Normalize For " + targetTypeName, targetTypeName + "�t�@�C���̉��ʂ𕽋ω�����");
            ShowPropertyField("ambisonicFor" + targetTypeName, "Ambisonic For " + targetTypeName, targetTypeName + "�t�@�C�����A���r�\�j�b�N(?)�ɂ���");
            ShowPropertyField("loadInBackgroundFor" + targetTypeName, "Load In Background For " + targetTypeName, targetTypeName + "�t�@�C�����o�b�N�O���E���h�Ń��[�h����");
            ShowPropertyField("loadTypeFor" + targetTypeName, "Load Type For " + targetTypeName, targetTypeName + "�t�@�C���̃��[�h�̎��");
            ShowPropertyField("qualityFor" + targetTypeName, "Quality For " + targetTypeName, targetTypeName + "�t�@�C���̕i��");
            ShowPropertyField("compressionFormatFor" + targetTypeName, "Compression Format For " + targetTypeName, targetTypeName + "�t�@�C���̈��k�t�H�[�}�b�g");
            ShowPropertyField("sampleRateSettingFor" + targetTypeName, "Sample Rate Setting For " + targetTypeName, targetTypeName + "�t�@�C���̃T���v�����O���[�g");
        }

        //�L���b�V���̐ݒ������GUI��\��
        private void ShowCacheGUI(string targetTypeName)
        {
            var typeProperty = ShowPropertyField(targetTypeName.ToLower() + "CacheType", targetTypeName + " Cache Type", targetTypeName + "�̃L���b�V���̎��");
            var cacheType = (AudioCacheType)Enum.ToObject(typeof(AudioCacheType), typeProperty.enumValueIndex);

            if(cacheType == AudioCacheType.USED)
            {
                ShowPropertyField("isRelease" + targetTypeName + "Cache", "Is Release " + targetTypeName + " Cache", "�V�[���J�ڎ���" + targetTypeName + "�̃L���b�V����j������");
            }
        }

        //�v���p�e�B��ύX����GUI��\��
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

        //GUI���X�L���ŋ���ŕ\��
        private static void ShowGUIAtSkin(Action showGUIAction)
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);
            showGUIAction();
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();
        }

        //GUI���X�L���ŋ���ŕ\��(����1��)
        public static void ShowGUIAtSkin<T1>(Action<T1> showGUIAction, T1 t1)
        {
            ShowGUIAtSkin(() => { showGUIAction(t1); });
        }
    }
}
