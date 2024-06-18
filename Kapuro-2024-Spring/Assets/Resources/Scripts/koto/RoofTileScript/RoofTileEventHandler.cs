using AudioController;
using UnityEngine;

public class RoofTileEventHandler : MonoBehaviour
{
    [SerializeField] private ConstantNumberKoto.ConstantNumberKoto.EVENT_TYPE eventType; //イベントの種類
    [SerializeField] private BossGenerater bossGenerater;
    
    public void EventHandler()
    {
        switch(eventType)
        {
            case ConstantNumberKoto.ConstantNumberKoto.EVENT_TYPE.KAWARAYOKAI:
                bossGenerater.GenerateBoss(eventType);
                BGMSwitcher.CrossFade(BGMPath.BossBGM02, 3);
                break;
            case ConstantNumberKoto.ConstantNumberKoto.EVENT_TYPE.SHISHIGAWARA:
                bossGenerater.GenerateBoss(eventType);
                BGMSwitcher.CrossFade(BGMPath.BossBGM02, 3);
                break;
            case ConstantNumberKoto.ConstantNumberKoto.EVENT_TYPE.SHISHIGAWARA_EVENT:
                //獅子瓦固有のイベント
                break;
            default:
                break;
        }
    }
    
    public void SetEvent() //イベントの種類を決定する
    {
        int randomValue = UnityEngine.Random.Range(0, 2);
        Debug.Log(randomValue);
        
        switch (randomValue)
        {
            case 0:
                eventType = ConstantNumberKoto.ConstantNumberKoto.EVENT_TYPE.KAWARAYOKAI;
                break;
            case 1:
                eventType = ConstantNumberKoto.ConstantNumberKoto.EVENT_TYPE.SHISHIGAWARA;
                break;
        }
    }
}
