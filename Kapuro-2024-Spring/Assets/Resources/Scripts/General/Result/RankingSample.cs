using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RankingSample : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rankingText; //ランキング情報を表示するTextMeshProUGUI
    
    /*ユーザ名*/
    
    //ユーザ名を更新
    public void UpdateUserName(TMP_InputField nameText)
    {
        //ユーザ名を指定して、UpdateUserTitleDisplayNameRequestのインスタンスを作成
        var request = new UpdateUserTitleDisplayNameRequest { DisplayName = nameText.text };
        
        //ユーザ名の更新
        Debug.Log($"ユーザ名の更新開始");
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnUpdateUserNameSuccess, OnUpdateUserNameFailure);
    }
    
    //ユーザ名の更新成功
    private void OnUpdateUserNameSuccess(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log($"ユーザ名の更新に成功しました: {result.DisplayName}");
    }

    //ユーザ名の更新失敗
    private void OnUpdateUserNameFailure(PlayFabError error)
    {
        Debug.LogError($"ユーザ名の更新に失敗しました\n{error.GenerateErrorReport()}");
    }
    
    /*スコア送信*/
    
    //スコアの更新をする
    public void UpdatePlayerStatistics(TMP_InputField scoreText)
    {
        //UpdatePlayerStatisticsRequestのインスタンスを作成
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "ランキングサンプル",
                    Value = int.Parse(scoreText.text),
                }
            }
        };
        
        Debug.Log($"スコア(統計情報)の更新開始");
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnUpdatePlayerStatisticsSuccess, OnUpdatePlayerStatisticsFailure);
    }
    
    //スコアの更新成功
    private void OnUpdatePlayerStatisticsSuccess(UpdatePlayerStatisticsResult result)
    {
        Debug.Log($"スコア(統計情報)の更新に成功しました");
    }
    
    //スコアの更新失敗
    private void OnUpdatePlayerStatisticsFailure(PlayFabError error)
    {
        Debug.LogError($"スコア(統計情報)の更新に失敗しました\n{error.GenerateErrorReport()}");
    }
    
    /*スコアの取得*/
    
    //ランキング(リーダボード)を取得
    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "ランキングサンプル", //ランキング名
            StartPosition = 0, //何位以降のランキングを取得するか
            MaxResultsCount = 3, //ランキングデータを何件取得するか(Max 100)
        };
        
        Debug.Log($"ランキング(リーダーボード)の取得開始");
        PlayFabClientAPI.GetLeaderboard(request, OnGetLeaderboardSuccess, OnGetLeaderboardFailure);
    }

    //自分の順位周辺のランキング(リーダーボード)を取得
    public void GetLeaderboardAroundPlayer()
    {
        var request = new GetLeaderboardAroundPlayerRequest()
        {
            StatisticName = "ランキングサンプル", //ランキング名(統計情報名)
            MaxResultsCount = 3, //自分を含め前後何件取得するか
        };
        
        Debug.Log($"自分の順位周辺のランキング(リーダーボード)の取得開始");
        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnGetLeaderboardAroundPlayerSuccess, OnGetLeaderboardAroundPlayerFailure);
    }
    
    //ランキング(リーダーボード)の取得成功
    private void OnGetLeaderboardSuccess(GetLeaderboardResult result)
    {
        Debug.Log($"ランキング(リーダーボード)の取得に成功しました");
        
        //result.Leaderboardに各順位の情報(PlayerLeaderboardEntry)が入っている
        rankingText.text = "";
        foreach (var entry in result.Leaderboard)
        {
            rankingText.text += $"\n順位: {entry.Position}, スコア: {entry.StatValue}, ユーザ名: {entry.DisplayName}, ID: {entry.PlayFabId}";
        }
    }

    //ランキング(リーダーボード)の取得失敗
    private void OnGetLeaderboardFailure(PlayFabError error)
    {
        Debug.LogError($"ランキング(リーダーボード)の取得に失敗しました\n{error.GenerateErrorReport()}");
    }
    
    //自分の順位周辺のランキング(リーダーボード)の取得成功
    private void OnGetLeaderboardAroundPlayerSuccess(GetLeaderboardAroundPlayerResult result)
    {
        Debug.Log($"自分の順位周辺のランキング(リーダーボード)の取得に成功しました");
        
        //result.Leaderboardに各順位の情報(PlayerLeaderboardEntry)が入っている
        rankingText.text = "";
        foreach (var entry in result.Leaderboard)
        {
            rankingText.text += $"\n順位: {entry.Position + 1}, スコア: {entry.StatValue}, ユーザ名: {entry.DisplayName}, ID: {entry.PlayFabId}";
        }
    }
    
    //自分の順位周辺のランキング(リーダーボード)の取得失敗
    private void OnGetLeaderboardAroundPlayerFailure(PlayFabError error)
    {
        Debug.LogError($"自分の順位周辺のランキング(リーダーボード)の取得に失敗しました\n{error.GenerateErrorReport()}");
    }
}
