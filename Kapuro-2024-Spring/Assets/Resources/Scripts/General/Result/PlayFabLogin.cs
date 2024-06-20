using System.Text;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;
using UnityEditor.PackageManager;
using UnityEngine;

public class PlayFabLogin : MonoBehaviour
{
    private bool isCreateAccount; //アカウントを作るかどうか
    private string customID; //カスタムID
    
    /*ログイン処理*/
    public void Start()
    {
        Login();
    }

    //ログイン実行時の処理
    private void Login()
    {
        customID = LoadCustomID();
        var request = new LoginWithCustomIDRequest { CustomId = customID, CreateAccount = isCreateAccount };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }

    //ログイン成功時の処理
    private void OnLoginSuccess(LoginResult result)
    {
        //IDが被っている場合
        if (isCreateAccount == true && result.NewlyCreated == false)
        {
            Debug.LogWarning($"CumstomId: {customID} は既に使われています");
            Login(); //ログインしなおす
            return;
        }

        //アカウント作成時にIDを保存
        if (result.NewlyCreated == true)
        {
            SaveCustomID();
        }
    
        Debug.Log($"PlayFabのログインに成功\nPlayFabId: {result.PlayFabId}, CustomId: {customID}\nアカウントを作成したか: {result.NewlyCreated}");
    }
    
    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogError($"PlayFabのログインに失敗\n{error.GenerateErrorReport()}");
    }
    
    /*カスタムIDの取得*/
    
    //IDを保存するときのKey
    private static readonly string CUSTOM_ID_SAVE_KEY = "CUSTOM_ID_SAVE_KEY";
    
    //IDを取得
    private string LoadCustomID()
    {
        //ここでIDを取得
        string id = PlayerPrefs.GetString(CUSTOM_ID_SAVE_KEY);
        
        //保存されていなければ新規作成
        isCreateAccount = string.IsNullOrEmpty(id);
        return isCreateAccount ? GenerateCustomID() : id;
    }
    
    //IDを保存
    private void SaveCustomID()
    {
        PlayerPrefs.SetString(CUSTOM_ID_SAVE_KEY, customID);
    }
    
    /*カスタムIDの生成*/
    
    //IDに使用する文字
    private static readonly string ID_CHARACTERS = "0123456789abcdefghijklmnopqrstuvwxyz";
    
    //IDを生成する
    private string GenerateCustomID()
    {
        int idLength = 32; //IDの長さ
        StringBuilder stringBuilder = new StringBuilder(idLength);
        var random = new System.Random();
        
        //ランダムにIDを生成
        for (int i = 0; i < idLength; i++)
        {
            stringBuilder.Append(ID_CHARACTERS[random.Next(ID_CHARACTERS.Length)]);
        }
        
        return stringBuilder.ToString();
    }
}