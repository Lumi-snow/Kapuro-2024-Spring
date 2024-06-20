using Cysharp.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;

public class ShishiGawara : AbstractBoss
{
    public override BossType bossType => BossType.SHISHIGAWARA; // bossType プロパティをオーバーライド
    
    /*共通のメンバ変数*/
    [SerializeField] private PrefabController prefabController; //PrefabController
    [SerializeField] private PrefabList prefabList; //PrefabList
    [SerializeField] private Slider ShishiGawaraHpSlider;
    [SerializeField] private const int hpMax = 1000; // 最大HP
    [SerializeField] private int shishiGawaraHp = 1000; //HP
    [SerializeField] private bool isShishiGawaraHpHalf = false; //HPが半分かどうか
    [SerializeField] private bool isLocationSet = false; //位置が設定されているかどうか
    
    /*共通のプロパティ*/
    public override float HpSlider { get => ShishiGawaraHpSlider.value; set => ShishiGawaraHpSlider.value = value; }
    
    public override int HpMax { get => hpMax;}
    
    public override int Hp
    {
        get => shishiGawaraHp;
        set => shishiGawaraHp = value;
    }
    
    public override bool IsBossHpHalf
    {
        get => isShishiGawaraHpHalf;
        set => isShishiGawaraHpHalf = value;
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
        HpSlider = HpMax;
        AwakingPointSlider.value = 0;
    }

    public override async void Initialize()
    {
        prefabController = this.AddComponent<PrefabController>();
        prefabController.prefabList = prefabList;
        InitializeMessagePanel();
        await OnGenerateMyself();
    }
    
    /*固有のメンバ変数*/
    [SerializeField] private Slider awakingPointSlider; //覚醒ポイントのスライダー
    [SerializeField] private int shishiGawaraMaxAwakingPoint = 300; //最大覚醒ポイント
    [SerializeField] private float shishiGawaraAwakingPoint = 0; //覚醒ポイント
    [SerializeField] private int allShishiGawaraWaterRoofTileNum = 30; //水の音が鳴る瓦の総数
    [SerializeField] private int allshishiGawaraWhistleNum = 100; //笛の音が鳴る瓦の数
    [SerializeField] private bool isShishiGawaraAwaking = false; //覚醒しているかどうか
    [SerializeField] private bool isGenerateShishiGawaraWaterRoofTile = false; //特殊瓦を生成するかどうか
    
    [SerializeField] private GameObject panel; //パネル
    [SerializeField] private TextMeshProUGUI messageText; //メッセージテキスト
    
    /*固有のプロパティ*/
    public override Slider AwakingPointSlider { get => awakingPointSlider; set => awakingPointSlider = value; }
    public override int MaxAwakingPoint { get => shishiGawaraMaxAwakingPoint; }
    public override float AwakingPoint { get => shishiGawaraAwakingPoint; set => shishiGawaraAwakingPoint = value; }
    public override int AllShishiGawaraWhistleNum { get => allshishiGawaraWhistleNum; set => allshishiGawaraWhistleNum = value; }
    public override int AllShishiGawaraWaterRoofTileNum { get => allShishiGawaraWaterRoofTileNum; set => allShishiGawaraWaterRoofTileNum = value; }
    public override bool IsAwaking { get => isShishiGawaraAwaking; set => isShishiGawaraAwaking = value; }
    public override bool IsGenerateShishiGawaraWaterRoofTile { get => isGenerateShishiGawaraWaterRoofTile; set => isGenerateShishiGawaraWaterRoofTile = value; }
    
    /*固有メンバ関数*/
    public override void AddAwakingPoint(float awakingPoint)
    {
        AwakingPoint += awakingPoint;
        AwakingPointSlider.value += awakingPoint;
    }

    public override void AttackShishiGawara(int attackPower)
    {
        Hp -= attackPower;
        HpSlider -= attackPower;
    }

    public override void GenerateShishiGawaraWaterRoofTile(RoofTileController roofTileController, GameObject roofTile)
    {
        int randomValue01 = UnityEngine.Random.Range(3, (roofTileController.roofTiles.Count - 1) / 2); //どのタイミングで出現させるかをランダムに決定
                        
        prefabController.InstantiatePrefab("ShishiGawaraWaterRoofTile", Vector3.zero, Quaternion.identity, roofTile); //PrefabからshishiGawaraWaterRoofTileを複製
        GameObject shishiGawaraWaterRoofTile = prefabController.clonePrefab;
        shishiGawaraWaterRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.NOT_EVALUATED; //shishiGawaraWaterRoofTileの評価をNOT_EVALUATEDに設定
        roofTileController.roofTiles.Insert(randomValue01, shishiGawaraWaterRoofTile); //複製したshishiGawaraWaterRoofTileをリストに追加
    }
    
    public override void GenerateShishiGawaraWhistle(RoofTileController roofTileController, GameObject roofTile)
    {
        int randomValue01 = UnityEngine.Random.Range(3, (roofTileController.roofTiles.Count - 1) / 2); //どのタイミングで出現させるかをランダムに決定
                        
        prefabController.InstantiatePrefab("ShishiGawaraWhistle", Vector3.zero, Quaternion.identity, roofTile); //PrefabからshishiGawaraWaterRoofTileを複製
        GameObject shishiGawaraWhistle = prefabController.clonePrefab;
        shishiGawaraWhistle.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.NOT_EVALUATED; //shishiGawaraWaterRoofTileの評価をNOT_EVALUATEDに設定
        roofTileController.roofTiles.Insert(randomValue01, shishiGawaraWhistle); //複製したshishiGawaraWaterRoofTileをリストに追加
    }
    
    public override void GenerateShishiGawaraEventRoofTile(RoofTileController roofTileController, GameObject roofTile) //特殊イベントを発生させる瓦を生成
    {
        int randomValue02 = UnityEngine.Random.Range(3, (roofTileController.roofTiles.Count - 1) / 2);
                    
        prefabController.InstantiatePrefab("ShishiGawaraEventRoofTile", Vector3.zero, Quaternion.identity, roofTile); //PrefabからshishiGawaraWaterRoofTileを複製
        GameObject shishiGawaraEventRoofTile = prefabController.clonePrefab;
        shishiGawaraEventRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.NOT_EVALUATED; //shishiGawaraWaterRoofTileの評価をNOT_EVALUATEDに設定
        shishiGawaraEventRoofTile.GetComponent<RoofTile>().InitializeShishiGawaraMessageEvent(); //イベント用の瓦を初期化
        roofTileController.roofTiles.Insert(randomValue02, shishiGawaraEventRoofTile); //複製したshishiGawaraWaterRoofTileをリストに追加
    }
    
    private void InitializeMessagePanel()
    {
        Transform panelTransform = gameObject.transform.Find("ExpressMessagePanel");
        panel = GameObject.Find("ExpressMessagePanel");
        Transform messageTextTransform = panelTransform.Find("ExpressMessageText");
        messageText = messageTextTransform.GetComponent<TextMeshProUGUI>();
        messageText.text = "獅子瓦の倒し方\n(下矢印キー押下で次のメッセージに進みます)";
    }
    
    private async UniTask OnGenerateMyself()
    {
        messageText.text = "獅子瓦は水の音が鳴ると\n目覚めてしまいます\n(下矢印キー押下で次のメッセージに進みます)";
        await UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.DownArrow), cancellationToken: this.GetCancellationTokenOnDestroy());
        messageText.text = "水の音が鳴る瓦を置いたり\n捨てたりしてはいけません\n(下矢印キー押下で次のメッセージに進みます)";
        await UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.DownArrow), cancellationToken: this.GetCancellationTokenOnDestroy());
        messageText.text = "Spaceキーで笛を使って\n獅子瓦を沈めましょう\n(下矢印キー押下でメッセージを閉じる)";
        await UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.DownArrow), cancellationToken: this.GetCancellationTokenOnDestroy());
        panel.gameObject.SetActive(false);
    }
}
