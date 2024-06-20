using Cysharp.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;

public class KawaraBouzu : AbstractBoss
{
    public override BossType bossType => BossType.KAWARA_BOUZU; // bossType プロパティをオーバーライド
    
    /*共通のメンバ変数*/
    [SerializeField] private PrefabController prefabController; //PrefabController
    [SerializeField] private PrefabList prefabList; //PrefabList
    [SerializeField] private Slider kawaraBouzuHpSlider;
    [SerializeField] private const int kawaraBouzuMaxHp = 1000; // 最大HP
    [SerializeField] private int kawaraBouzuHp = 1000; //HP
    [SerializeField] private bool isKawaraBouzuHpHalf = false; //HPが半分かどうか
    [SerializeField] private bool isLocationSet = false; //位置が設定されているかどうか
    
    /*共通のプロパティ*/
    public override float HpSlider { get => kawaraBouzuHpSlider.value; set => kawaraBouzuHpSlider.value = value; }
    
    public override int HpMax { get => kawaraBouzuMaxHp;}
    
    public override int Hp
    {
        get => kawaraBouzuHp;
        set => kawaraBouzuHp = value;
    }
    
    public override bool IsBossHpHalf
    {
        get => isKawaraBouzuHpHalf;
        set => isKawaraBouzuHpHalf = value;
    }
    
    public override bool IsLocationSet
    {
        get => isLocationSet;
        set => isLocationSet = value;
    }
    
    /*共通のメンバ関数*/
    public override void SetMyself()
    {
        transform.localPosition = new Vector3(0, 300, 0);
        transform.localScale = new Vector3(50, 50, 1);
        HpSlider = kawaraBouzuMaxHp;
        bouzuHpSlider.value = bouzuMaxHp;
    }

    public override async void Initialize()
    {
        prefabController = this.AddComponent<PrefabController>();
        prefabController.prefabList = prefabList;
        InitializeMessagePanel();
        await OnGenerateMyself();
    }
    
    /*固有のメンバ変数*/
    [SerializeField] private Slider bouzuHpSlider;
    [SerializeField] private int bouzuMaxHp = 1000; // 最大HP
    [SerializeField] private int bouzuHp = 1000; //HP
    [SerializeField] private bool isBouzuHpHalf = false; //HPが半分かどうか
    [SerializeField] private int allKyouNum = 50; //お経の数
    [SerializeField] private int allAburaNum = 50; //油の数
    [SerializeField] private int allMameNum = 50; //豆の数
    [SerializeField] private bool isGenerateKawaraBouzuRoofTile = false; //覚醒しているかどうか
    [SerializeField] private bool isCorrectAnswer = false; //正解かどうか
    [SerializeField] private bool isFinishEvent = false; //イベントが終了したかどうか

    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI messageText;
    
    /*固有のプロパティ*/
    public override Slider BouzuHpSlider { get => bouzuHpSlider; set => bouzuHpSlider = value; }
    public override int BouzuHp { get => bouzuHp; set => bouzuHp = value; }
    public override int AllKyouNum { get => allKyouNum; set => allKyouNum = value; }
    public override int AllAburaNum { get => allAburaNum; set => allAburaNum = value; }
    public override int AllMameNum { get => allMameNum; set => allMameNum = value; }
    public override bool IsGenerateKawaraBouzuRoofTile { get => isGenerateKawaraBouzuRoofTile; set => isGenerateKawaraBouzuRoofTile = value; }
    public override bool IsCorrectAnswerKawaraBouzu { get => isCorrectAnswer; set => isCorrectAnswer = value; }
    public override bool IsFinishEventKawaraBouzu { get => isFinishEvent; set => isFinishEvent = value; }
    
    /*固有メンバ関数*/
    public override void AttackKawaraBouzu(int attackPower)
    {
        Hp -= attackPower;
        HpSlider -= attackPower;
    }

    public override void AttackBouzu(int attackPower)
    {
        BouzuHp -= attackPower;
        bouzuHpSlider.value -= attackPower;
    }

    public override void GenerateKawaraBouzuAburaRoofTile(RoofTileController roofTileController, GameObject roofTile)
    {
        int randomValue01 = UnityEngine.Random.Range(3, (roofTileController.roofTiles.Count - 1) / 2); //どのタイミングで出現させるかをランダムに決定
                        
        prefabController.InstantiatePrefab("KawaraBouzuAburaRoofTile", Vector3.zero, Quaternion.identity, roofTile); //PrefabからshishiGawaraWaterRoofTileを複製
        GameObject kawaraBouzuAburaRoofTile = prefabController.clonePrefab;
        kawaraBouzuAburaRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.NOT_EVALUATED; //shishiGawaraWaterRoofTileの評価をNOT_EVALUATEDに設定
        roofTileController.roofTiles.Insert(randomValue01, kawaraBouzuAburaRoofTile); //複製したshishiGawaraWaterRoofTileをリストに追加
    }
    
    public override void GenerateKawaraBouzuKyouRoofTile(RoofTileController roofTileController, GameObject roofTile)
    {
        int randomValue01 = UnityEngine.Random.Range(3, (roofTileController.roofTiles.Count - 1) / 2); //どのタイミングで出現させるかをランダムに決定
                        
        prefabController.InstantiatePrefab("KawaraBouzuKyouRoofTile", Vector3.zero, Quaternion.identity, roofTile); //PrefabからshishiGawaraWaterRoofTileを複製
        GameObject kawaraBouzuKyouRoofTile = prefabController.clonePrefab;
        kawaraBouzuKyouRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.NOT_EVALUATED; //shishiGawaraWaterRoofTileの評価をNOT_EVALUATEDに設定
        roofTileController.roofTiles.Insert(randomValue01, kawaraBouzuKyouRoofTile); //複製したshishiGawaraWaterRoofTileをリストに追加
    }

    public override void GenerateKawaraBouzuMameRoofTile(RoofTileController roofTileController, GameObject roofTile)
    {
        int randomValue01 = UnityEngine.Random.Range(3, (roofTileController.roofTiles.Count - 1) / 2); //どのタイミングで出現させるかをランダムに決定
                        
        prefabController.InstantiatePrefab("KawaraBouzuMameRoofTile", Vector3.zero, Quaternion.identity, roofTile); //PrefabからshishiGawaraWaterRoofTileを複製
        GameObject kawaraBouzuMameRoofTile = prefabController.clonePrefab;
        kawaraBouzuMameRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.NOT_EVALUATED; //shishiGawaraWaterRoofTileの評価をNOT_EVALUATEDに設定
        roofTileController.roofTiles.Insert(randomValue01, kawaraBouzuMameRoofTile); //複製したshishiGawaraWaterRoofTileをリストに追加
    }
    
    public override void GenerateKawaraBouzuEventRoofTile(RoofTileController roofTileController, GameObject roofTile) //特殊イベントを発生させる瓦を生成
    {
        int randomValue02 = UnityEngine.Random.Range(3, (roofTileController.roofTiles.Count - 1) / 2);
                    
        prefabController.InstantiatePrefab("KawaraBouzuEventRoofTile", Vector3.zero, Quaternion.identity, roofTile); //PrefabからshishiGawaraWaterRoofTileを複製
        GameObject kawaraBouzuEventRoofTile = prefabController.clonePrefab;
        kawaraBouzuEventRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.NOT_EVALUATED; //shishiGawaraWaterRoofTileの評価をNOT_EVALUATEDに設定
        kawaraBouzuEventRoofTile.GetComponent<RoofTile>().InitializeKawaraBouzuEvent(); //イベント用の瓦を初期化
        roofTileController.roofTiles.Insert(randomValue02, kawaraBouzuEventRoofTile); //複製したshishiGawaraWaterRoofTileをリストに追加
    }
    
    private void InitializeMessagePanel()
    {
        Transform panelTransform = gameObject.transform.Find("ExpressMessagePanel");
        panel = GameObject.Find("ExpressMessagePanel");
        Transform messageTextTransform = panelTransform.Find("ExpressMessageText");
        messageText = messageTextTransform.GetComponent<TextMeshProUGUI>();
        messageText.text = "瓦坊主の倒し方\n(下矢印キー押下で次のメッセージに進みます)";
    }
    
    private async UniTask OnGenerateMyself()
    {
        Debug.Log("koko");
        messageText.text = "瓦坊主と坊主がいます\n(下矢印キー押下で次のメッセージに進みます)";
        await UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.DownArrow), cancellationToken: this.GetCancellationTokenOnDestroy());
        messageText.text = "瓦坊主には\n油と豆をSpaceキーで投げてください\n(下矢印キー押下で次のメッセージに進みます)";
        await UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.DownArrow), cancellationToken: this.GetCancellationTokenOnDestroy());
        messageText.text = "坊主には\nお経を右矢印キーで受け取ってください\n(下矢印キー押下でメッセージを閉じる)";
        await UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.DownArrow), cancellationToken: this.GetCancellationTokenOnDestroy());
        panel.gameObject.SetActive(false);
    }
}