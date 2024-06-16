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
            case ConstantNumberKoto.ConstantNumberKoto.EVENT_TYPE.BOSS_YOKAI01:
                bossGenerater.GenerateBoss(eventType);
                BGMSwitcher.CrossFade(BGMPath.BossBGM02, 3);
                break;
            case ConstantNumberKoto.ConstantNumberKoto.EVENT_TYPE.BOSS_YOKAI02:
                bossGenerater.GenerateBoss(eventType);
                break;
            default:
                break;
        }
    }
    
    public void SetEvent() //イベントの種類を決定する
    {
        int randomValue = UnityEngine.Random.Range(1, 2);
        
        switch (randomValue)
        {
            case 1:
                eventType = ConstantNumberKoto.ConstantNumberKoto.EVENT_TYPE.BOSS_YOKAI01;
                break;
            case 2:
                eventType = ConstantNumberKoto.ConstantNumberKoto.EVENT_TYPE.BOSS_YOKAI02;
                break;
        }
    }
}
