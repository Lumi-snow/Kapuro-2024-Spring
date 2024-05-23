using System;
using UnityEngine;
using UnityEngine.Assertions;

//ジェネリックであるTYpeはMonoBehaviourを継承したクラスを指定
//IDisposableはinterface
public abstract class SingletonWithMonoBehaviour<TYpe> : MonoBehaviour, IDisposable where TYpe : MonoBehaviour
{
    private static TYpe instance;

    public static TYpe Instance
    {
        get //instanceがnull出ないことをアサートし、インスタンスを返す
        {
            //アサート(Assert)は、プログラムの実行時に特定の条件が成り立っていることを検証するための仕組み
            //今回はnullの場合、エラーを表示しプログラムの実行を停止する。
            Assert.IsNotNull(instance, "There is no object attached" + typeof(TYpe).Name);
            return instance;
        }
    }

    //一番最初に自動で実行される
    private void Awake()
    {
        if (instance != null && instance.gameObject != null)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this as TYpe;
        onAwakeProcess();
    }

    //Singletonが存在するか
    public static bool isExist()
    {
        return instance != null ? true : false;
    }

    //Destroy処理時に実行する
    private void onDestroy()
    {
        if (instance != (this as TYpe)) return;
        onDestroyProcess();
        Dispose();
    }

    //自身を破棄する
    public virtual void Dispose()
    {
        if(isExist() == true)
        {
            instance = null;
        }
    }

    /*この関数をいじって追加処理を加える*/
    //Destroy処理時の追加処理を実行
    protected virtual void onDestroyProcess() { }
    //派生先での初期化処理を実行
    protected virtual void onAwakeProcess() { }
}

/* reference -> https://qiita.com/Cova8bitdot/items/29b7064c7472a6f34972 */

/*実装例
 *using System;
 *using UnityEngine;
 *using UnityEngine.Assertions;
 *
 *public class SoundManager : SingletonMonoBehaviour<SoundManager>
 *{
 *    (中略)
 *}
 */

/*
 * IDisposableは、リソースの開放やクリーンアップを行うためのインターフェイス。
 * GC(ガベージコレクタ)がオブジェクトを解放するまでの間、アンマネージドリソース(ネットワーク接続など)が解放されることを保証する
 */