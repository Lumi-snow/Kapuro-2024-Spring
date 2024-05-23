using System;
using System.Dynamic;

public abstract class PureSingleton<T> : IDisposable where T : class, new()
{
    private static T instance = null;

    //Instanceのシンタックスシュガー(糖衣構文) 要するに、シンプルでわかりやすく書き換えること
    //Iを介してインスタンスを取得することができる
    public static T I => Instance;
    public static T Instance
    {
        get
        {
            createInstance();
            return instance;
        }
    }

    //インスタンスを生成する
    public static void createInstance()
    {
        if(instance == null)
        {
            instance = new T();
        }
    }

    //Singletonが存在するか
    public static bool isExists()
    {
        return instance != null;
    }

    //Singletonを破棄する
    public virtual void Dispose()
    {
        instance = null;
    }
}

/* reference -> https://qiita.com/Cova8bitdot/items/29b7064c7472a6f34972 */

/*
 * Tにクラス名を入れて継承する
 */