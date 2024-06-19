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
            case ConstantNumberKoto.ConstantNumberKoto.EVENT_TYPE.KAWARA_BOUZU:
                bossGenerater.GenerateBoss(eventType);
                BGMSwitcher.CrossFade(BGMPath.BossBGM02, 3);
                break;
            case ConstantNumberKoto.ConstantNumberKoto.EVENT_TYPE.KAWARA_BOUZU_EVENT:
                //瓦坊主固有のイベント
                break;
        }
    }
    
    public void SetEvent() //イベントの種類を決定する
    {
        int randomValue = UnityEngine.Random.Range(2, 3);
        Debug.Log(randomValue);
        
        switch (randomValue)
        {
            case 0:
                eventType = ConstantNumberKoto.ConstantNumberKoto.EVENT_TYPE.KAWARAYOKAI;
                break;
            case 1:
                eventType = ConstantNumberKoto.ConstantNumberKoto.EVENT_TYPE.SHISHIGAWARA;
                break;
            case 2:
                eventType = ConstantNumberKoto.ConstantNumberKoto.EVENT_TYPE.KAWARA_BOUZU;
                break;
        }
    }
}
