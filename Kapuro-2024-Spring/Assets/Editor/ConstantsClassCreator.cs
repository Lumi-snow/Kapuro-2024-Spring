#if UNITY_EDITOR

namespace AudioController {

	using System;
	using System.IO;
	using System.Linq;
	using System.Text;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEditor;

	//�萔���Ǘ�����N���X�𐶐�����N���X
	public static class ConstantsClassCreator{

		//�^��
		private const string STRING_NAME = "string";
		private const string INT_NAME    = "int";
		private const string FLOAT_NAME  = "float";
	
		//�g���q
		private const string SCRIPT_EXTENSION = ".cs";
	
		/*����*/

		//�萔���Ǘ�����N���X��������������
		public static void Create<T> (string className, string summary, Dictionary<string, T> valueDict, string exportDirectoryPath, string nameSpace = ""){
			//���͂��ꂽ�^�̔���
			string typeName = null;

			if(typeof(T) == typeof(string)){
				typeName = STRING_NAME;
			}
			else if(typeof(T) == typeof(int)){
				typeName = INT_NAME;
			}
			else if(typeof(T) == typeof(float)){
				typeName = FLOAT_NAME;
			}
			else{
				Debug.Log (className + SCRIPT_EXTENSION +"�̍쐬�Ɏ��s���܂���.�z��O�̌^" + typeof(T).Name  + "�����͂���܂���");
				return;
			}

			//�f�B�N�V���i���[���\�[�g�������̂�
			SortedDictionary<string, T> sortDict = new SortedDictionary<string, T> (valueDict);

			//���͂��ꂽ������key���疳���ȕ�������폜���āA�啶����_��ݒ肵���萔���Ɠ������̂ɕύX���V���Ȏ����ɓo�^
			//���̒萔�̍ő咷���߂�Ƃ���ŁA_���܂߂����̂��擾�������̂ŁA��Ɏ��s
			Dictionary<string, T> newValueDict = new Dictionary<string, T> ();

			foreach (KeyValuePair<string, T> valuePair in sortDict) {
				string newKey = RemoveInvalidChars(valuePair.Key);
				newKey = SetDelimiterBeforeUppercase(newKey);
				newValueDict [newKey] = valuePair.Value;
			}

			//�萔���̍ő咷���擾���A�󔒐�������
			int keyLengthMax = 0;
			if(newValueDict.Count > 0){
				keyLengthMax = 1 + newValueDict.Keys.Select (key => key.Length).Max ();
			}

			//�R�[�h�S��
			StringBuilder builder = new StringBuilder ();
		
			//�l�[���X�y�[�X������ΐݒ�
			if (!string.IsNullOrEmpty(nameSpace)) {
				builder.AppendLine ("namespace " + nameSpace + "{\n");
			}
		
			//�R�����g���ƃN���X�������
			builder.AppendLine ("/// <summary>");
			builder.AppendFormat ("/// {0}", summary).AppendLine ();
			builder.AppendLine ("/// </summary>");
			builder.AppendFormat ("public static class {0}", className).AppendLine ("{").AppendLine ();

			//���͂��ꂽ�萔�Ƃ��̒l�̃y�A�������o���Ă���
			string[] keyArray = newValueDict.Keys.ToArray();
			foreach (string key in keyArray) {

				if (string.IsNullOrEmpty (key)) {
					continue;
				}
				//����������key��������X���[
				else if (System.Text.RegularExpressions.Regex.IsMatch(key ,@"^[0-9]+$")){
					continue;
				}
				//key�ɔ��p������_�ȊO���܂܂�Ă�����X���[
				else if (!System.Text.RegularExpressions.Regex.IsMatch(key, @"^[_a-zA-Z0-9]+$")){
					continue;
				}

				//�C�R�[�������ԗp�ɋ󔒂𒲐�����
				string EqualStr = String.Format("{0, " + (keyLengthMax - key.Length).ToString() + "}", "=");

				//��L�Ŕ��肵���^�ƒ萔�������
				builder.Append ("\t").AppendFormat (@"public const {0} {1} {2} ", typeName, key, EqualStr);

				//T��string�̏ꍇ�͒l�̑O���"��t����
				if (typeName == STRING_NAME) {
					builder.AppendFormat (@"""{0}"";", newValueDict[key]).AppendLine ();
				} 

				//T��float�̏ꍇ�͒l�̌��f��t����
				else if (typeName == FLOAT_NAME) {
					builder.AppendFormat (@"{0}f;", newValueDict[key]).AppendLine ();
				}

				else {
					builder.AppendFormat (@"{0};", newValueDict[key]).AppendLine ();
				}

			}

			builder.AppendLine().AppendLine ("}");
		
			//�l�[���X�y�[�X������΍Ō�ɃJ�b�R�ǉ�
			if (!string.IsNullOrEmpty(nameSpace)) {
				builder.AppendLine().AppendLine ("}");
			}

			//�����o���A�t�@�C�����̓N���X��.cs
			string exportPath = Path.Combine(exportDirectoryPath, className + SCRIPT_EXTENSION);
			string exportText = builder.ToString();

			//�����o����̃f�B���N�g����������΍쐬
			string directoryName = Path.GetDirectoryName (exportPath);
			if (!Directory.Exists (directoryName)) {
				Directory.CreateDirectory(directoryName);
			}
		
			//�����o����̃t�@�C�������邩�`�F�b�N
			if (File.Exists(exportPath)) {
				//�����t�@�C���̒��g���`�F�b�N�A�S�������������珑���o���Ȃ�
				StreamReader sr = new StreamReader(exportPath, Encoding.UTF8);
				bool isSame = sr.ReadToEnd() == exportText;
				sr.Close();

				if (isSame) {
					return;;
				}
			}
		
			//�����o��
			File.WriteAllText (exportPath, exportText, Encoding.UTF8);
			AssetDatabase.Refresh (ImportAssetOptions.ImportRecursive);

			Debug.Log (className + SCRIPT_EXTENSION + "�̍쐬���������܂���");
		}

		/*�����ȕ����̍폜*/

		//�����ȕ������Ǘ�����z��
		private static readonly string[] INVALID_CHARS = {
			" ", "!", "\"", "#", "$",
			"%", "&", "\'", "(", ")",
			"-", "=", "^",  "~", "\\",
			"|", "[", "{",  "@", "`",
			"]", "}", ":",  "*", ";",
			"+", "/", "?",  ".", ">",
			",", "<"
		};
	
		//�����ȕ������폜���܂�
		private static string RemoveInvalidChars(string str){
			Array.ForEach(INVALID_CHARS, c => str = str.Replace(c, string.Empty));
			return str;
		}
	
		/*��؂蕶���̐ݒ�*/

		//�萔�̋�؂蕶��
		private const char DELIMITER = '_';

		//��؂蕶����啶���̑O�ɐݒ肷��
		private static string SetDelimiterBeforeUppercase(string str){
			string conversionStr = "";

			for(int strNo = 0; strNo < str.Length; strNo++){

				bool isSetDelimiter = true;

				//�ŏ��ɂ͐ݒ肵�Ȃ�
				if(strNo == 0){
					isSetDelimiter = false;
				}
				//�������������Ȃ�ݒ肵�Ȃ�
				else if(char.IsLower(str[strNo]) || char.IsNumber(str[strNo])){
					isSetDelimiter = false;
				}
				//���肵�Ă�̑O���啶���Ȃ�ݒ肵�Ȃ�(�A���啶���̎�)
				else if(char.IsUpper(str[strNo - 1]) && !char.IsNumber(str[strNo])){
					isSetDelimiter = false;
				}
				//���肵�Ă镶�������̕����̑O����؂蕶���Ȃ�ݒ肵�Ȃ�
				else if(str[strNo] == DELIMITER || str[strNo - 1] == DELIMITER){
					isSetDelimiter = false;
				}

				//�����ݒ�
				if(isSetDelimiter){
					conversionStr += DELIMITER.ToString();
				}
				conversionStr += str.ToUpper() [strNo];

			}

			return conversionStr;
		}
	}
}

#endif
